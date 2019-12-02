using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using QuackCalendar.Model;
using QuackCalendar.Model.Constant;
using QuackCalendar.Service.Exception;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal sealed class MySqlGateway : SqlGatewayBase
    {
        protected override async Task<QCAddEventResponse> AddEventCoreAsync(QCAddEventRequest qcAddEventRequest)
        {
            //var query = $"INSERT INTO quackcalendar.events (`userid`, `startdatetime`, `enddatetime`, `name`, `description`) VALUES (" +
            //    $"'{qcAddEventRequest.UserId}', " +
            //    $"'{qcAddEventRequest.Event.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
            //    $"'{qcAddEventRequest.Event.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
            //    $"'{qcAddEventRequest.Event.Name}', " +
            //    $"'{qcAddEventRequest.Event.Description}');";
            var query = $"INSERT INTO quackcalendar.events (`userid`, `startdatetime`, `enddatetime`, `name`, `description`) VALUES (" +
                $"'{qcAddEventRequest.UserId}', " +
                $"'{qcAddEventRequest.Event.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"'{qcAddEventRequest.Event.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"@param1, " +
                $"@param2);";

            await ExecuteCommandAsync(
                query,
                1,
                qcAddEventRequest.Event.Name,
                qcAddEventRequest.Event.Description);

            //query = $"SELECT edb.eventid FROM quackcalendar.events AS edb " +
            //    $"WHERE edb.userid = '{qcAddEventRequest.UserId}' AND " +
            //    $"edb.name = '{qcAddEventRequest.Event.Name}' AND " +
            //    $"edb.description = '{qcAddEventRequest.Event.Description}' AND " +
            //    $"edb.startdatetime = '{qcAddEventRequest.Event.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}';";
            query = $"SELECT edb.eventid FROM quackcalendar.events AS edb " +
                $"WHERE edb.userid = '{qcAddEventRequest.UserId}' AND " +
                $"edb.name=@param1 AND " +
                $"edb.description=@param2 AND " +
                $"edb.startdatetime = '{qcAddEventRequest.Event.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}';";

            var dataSetQuery = await ExecuteQueryAsync(
                query,
                1,
                qcAddEventRequest.Event.Name,
                qcAddEventRequest.Event.Description);
            var eventId = (int)dataSetQuery.Tables[0].Rows[0].ItemArray[0];

            var response = new QCAddEventResponse
            {
                Event = new QCEvent { Id = eventId },
                StatusCode = QCStatusCodes.SuccessfulStatusCode,
                StatusMessage = QCStatusMessages.SuccessfulStatusMessage
            };

            return response;
        }

        protected override async Task<QCDeleteEventResponse> DeleteEventCoreAsync(QCDeleteEventRequest qcDeleteEventRequest)
        {
            var query = $"DELETE FROM quackcalendar.events WHERE quackcalendar.events.eventid = {qcDeleteEventRequest.EventId};";

            await ExecuteCommandAsync(query, 1);

            var response = new QCDeleteEventResponse
            {
                StatusCode = QCStatusCodes.SuccessfulStatusCode,
                StatusMessage = QCStatusMessages.SuccessfulStatusMessage
            };

            return response;
        }

        protected override async Task<QCGetEventResponse> GetEventCoreAsync(QCGetEventRequest qcGetEventRequest)
        {
            var query = $"SELECT edb.name, edb.description, edb.startdatetime, edb.enddatetime " +
                $"FROM quackcalendar.events AS edb " +
                $"WHERE edb.eventid = {qcGetEventRequest.EventId};";

            var dataSetQuery = await ExecuteQueryAsync(query, 4);
            var response = new QCGetEventResponse();

            foreach (DataRow row in dataSetQuery.Tables[0].Rows)
            {
                response.Event.Description = (string)row.ItemArray[1];
                response.Event.EndDateTime = (DateTime)row.ItemArray[3];
                response.Event.Id = qcGetEventRequest.EventId;
                response.Event.Name = (string)row.ItemArray[0];
                response.Event.StartDateTime = (DateTime)row.ItemArray[2];
            }

            return response;
        }

        protected override async Task<QCGetEventsResponse> GetEventsCoreAsync(QCGetEventsRequest qcGetEventsRequest)
        {
            var query = $"SELECT edb.name, edb.description, edb.startdatetime, edb.enddatetime, edb.eventid " +
                $"FROM quackcalendar.events AS edb " +
                $"WHERE edb.userid = {qcGetEventsRequest.UserId} " +
                $"AND edb.startdatetime >= '{qcGetEventsRequest.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                $"AND edb.enddatetime <= '{qcGetEventsRequest.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}';";
            

            var dataSetQuery = await ExecuteQueryAsync(query, 5);
            var response = new QCGetEventsResponse();

            foreach (DataRow row in dataSetQuery.Tables[0].Rows)
            {
                response.Events.Add(new QCEvent
                {
                    Description = (string)row.ItemArray[1],
                    EndDateTime = (DateTime)row.ItemArray[3],
                    Id = (int)row.ItemArray[4],
                    Name = (string)row.ItemArray[0],
                    StartDateTime = (DateTime)row.ItemArray[2]
                });
            }

            response.StatusCode = QCStatusCodes.SuccessfulStatusCode;
            response.StatusMessage = QCStatusMessages.SuccessfulStatusMessage;

            return response;
        }

        protected override async Task<QCUpdateEventResponse> UpdateEventCoreAsync(QCUpdateEventRequest qcUpdateEventRequest)
        {
            var qce = qcUpdateEventRequest.Event;

            //var query = $"UPDATE quackcalendar.events SET " +
            //    $"quackcalendar.events.startdatetime = '{qce.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
            //    $"quackcalendar.events.enddatetime = '{qce.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
            //    $"quackcalendar.events.name = '{qce.Name}', " +
            //    $"quackcalendar.events.description = '{qce.Description}' " +
            //    $"WHERE quackcalendar.events.eventid = {qce.Id};";
            var query = $"UPDATE quackcalendar.events SET " +
                $"quackcalendar.events.startdatetime = '{qce.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"quackcalendar.events.enddatetime = '{qce.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"quackcalendar.events.name=@param1, " +
                $"quackcalendar.events.description=@param2 " +
                $"WHERE quackcalendar.events.eventid = {qce.Id};";

            await ExecuteCommandAsync(
                query,
                1,
                qce.Name,
                qce.Description);

            var response = new QCUpdateEventResponse
            {
                Event = qce,
                StatusCode = QCStatusCodes.SuccessfulStatusCode,
                StatusMessage = QCStatusMessages.SuccessfulStatusMessage
            };

            return response;
        }

        private async Task ExecuteCommandAsync(string query, int expectedRowsAffected, params string[] queryParameters)
        {
            try
            {
                using (var connection = new MySqlConnection(GetConnectionString()))
                {
                    using (var command = new MySqlCommand(query, connection))
                    {
                        for (int i = 0; i < queryParameters.Length; i++)
                        {
                            var tokenString = $"@param{i + 1}";
                            command.Parameters.AddWithValue(tokenString, queryParameters[i]);
                        }

                        await command.Connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        await command.Connection.CloseAsync();
                        ValidateCommandResults(rowsAffected, expectedRowsAffected);
                    }
                }
            }
            catch (QCServiceBaseException)
            {
                throw;
            }
            catch (MySqlException exception)
            {
                throw new QCServiceSqlException($"There was an error when executing the SQL command:\n{exception.Message}");
            }
        }

        private async Task<DataSet> ExecuteQueryAsync(string query, int expectedColumnsPerTable, params string[] queryParameters)
        {
            try
            {
                using (var connection = new MySqlConnection(GetConnectionString()))
                {
                    using (var dataAdapter = new MySqlDataAdapter(query, connection))
                    {
                        for (int i = 0; i < queryParameters.Length; i++)
                        {
                            var tokenString = $"@param{i + 1}";
                            dataAdapter.SelectCommand.Parameters.AddWithValue(tokenString, queryParameters[i]);
                        }

                        var dataSet = new DataSet();
                        await dataAdapter.FillAsync(dataSet);
                        ValidateQueryResults(dataSet, expectedColumnsPerTable);

                        return dataSet;
                    }
                }
            }
            catch (QCServiceBaseException)
            {
                throw;
            }
            catch (MySqlException exception)
            {
                throw new QCServiceSqlException($"There was an error when executing the SQL query:\n{exception.Message}");
            }
        }

        private string GetConnectionString()
        {
            return "server=192.168.1.103;port=3306;user=admin;password=sqlsql";
        }

        private void ValidateCommandResults(int actualRowsAffected, int expectedRowsAffected)
        {
            if (actualRowsAffected != expectedRowsAffected)
            {
                throw new QCServiceSqlException($"The SQL command affected {actualRowsAffected} row(s) but was expected to affect {expectedRowsAffected} row(s).");
            }
        }

        private void ValidateQueryResults(DataSet dataSet, int expectedColumnsPerTable)
        {
            if (dataSet.Tables[0].Columns.Count != expectedColumnsPerTable)
            {
                throw new QCServiceSqlException($"The SQL query returned {dataSet.Tables[0].Columns.Count} columns(s) but expected {expectedColumnsPerTable} columns(s) to be returned.");
            }
        }
    }
}