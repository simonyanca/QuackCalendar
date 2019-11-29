using System;
using System.Data;
using MySql.Data.MySqlClient;
using QuackCalendar.Model;
using QuackCalendar.Service.Exception;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal sealed class MySqlGateway : SqlGatewayBase
    {
        protected override QCAddEventResponse AddEventCore(QCAddEventRequest qcAddEventRequest)
        {
            // INSERT INTO `quackcalendar`.`events` (`userid`, `startdatetime`, `enddatetime`, `name`, `description`)
            // VALUES ('1', '2019-1-1', '2019-1-2', 'nyname', 'nydesc');

            var query = $"INSERT INTO quackcalendar.events (`userid`, `startdatetime`, `enddatetime`, `name`, `description`) VALUES (" +
                $"'{qcAddEventRequest.UserId}', " +
                $"'{qcAddEventRequest.Event.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"'{qcAddEventRequest.Event.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"'{qcAddEventRequest.Event.Name}', " +
                $"'{qcAddEventRequest.Event.Description}');";

            ExecuteCommand(query, 1);

            return new QCAddEventResponse();
        }

        protected override QCGetEventsResponse GetEventsCore(QCGetEventsRequest qcGetEventsRequest)
        {
            var query = $"SELECT edb.name, edb.description, edb.startdatetime, edb.enddatetime " +
                $"FROM quackcalendar.events AS edb " +
                $"WHERE edb.userid = {qcGetEventsRequest.UserId} " +
                $"AND edb.startdatetime >= '{qcGetEventsRequest.StartDate.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                $"AND edb.enddatetime <= '{qcGetEventsRequest.EndDate.ToString("yyyy-MM-dd HH:mm:ss")}';";
            

            var dataSetQuery = ExecuteQuery(query, 4);
            var response = new QCGetEventsResponse();

            foreach (DataRow row in dataSetQuery.Tables[0].Rows)
            {
                response.Events.Add(new QCEvent
                {
                    Description = (string)row.ItemArray[1],
                    EndDateTime = (DateTime)row.ItemArray[3],
                    Name = (string)row.ItemArray[0],
                    StartDateTime = (DateTime)row.ItemArray[2]
                });
            }

            return response;
        }

        private void ExecuteCommand(string query, int expectedRowsAffected)
        {
            try
            {
                using (var connection = new MySqlConnection(GetConnectionString()))
                {
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Connection.Open();
                        var rowsAffected = command.ExecuteNonQuery();
                        command.Connection.Close();
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

        private DataSet ExecuteQuery(string query, params int[] expectedColumnsPerTable)
        {
            try
            {
                using (var connection = new MySqlConnection(GetConnectionString()))
                {
                    using (var dataAdapter = new MySqlDataAdapter(query, connection))
                    {
                        var dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
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

        private void ValidateQueryResults(DataSet dataSet, params int[] expectedColumnsPerTable)
        {
            var actualTableCount = dataSet.Tables.Count;
            var expectedTableCount = expectedColumnsPerTable.Length;            

            if (actualTableCount != expectedTableCount)
            {
                throw new QCServiceSqlException($"The SQL query returned {actualTableCount} table(s) but expected {expectedTableCount} table(s) to be returned.");
            }

            for (int table = 0; table < actualTableCount; table++)
            {
                var actualColumns = dataSet.Tables[table].Columns.Count;
                var expectedColumns = expectedColumnsPerTable[table];

                if (actualColumns != expectedColumns)
                {
                    throw new QCServiceSqlException($"The SQL query returned {actualColumns} columns for a table expected to return {expectedColumns} columns.");
                }
            }
        }
    }
}