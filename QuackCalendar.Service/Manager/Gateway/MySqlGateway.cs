using System;
using MySql.Data.MySqlClient;
using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal sealed class MySqlGateway : SqlGatewayBase
    {
        protected override QCGetEventsResponse GetEventsCore(QCGetEventsRequest qcGetEventsRequest)
        {
            (var connection, var reader) = CreateReaderForQuery("SELECT * FROM quackcalendar.events;");
            var response = new QCGetEventsResponse();

            while (reader.Read())
            {
                var newEvent = new QCEvent
                {
                    Description = (string)reader["description"],
                    EndDateTime = (DateTime)reader["enddatetime"],
                    Name = (string)reader["name"],
                    StartDateTime = (DateTime)reader["startdatetime"]
                };

                response.Events.Add(newEvent);
            }

            reader.Close();
            connection.Close();

            return response;
        }

        private (MySqlConnection, MySqlDataReader) CreateReaderForQuery(string query)
        {
            var connectionString = "server=192.168.1.103;port=3306;user=admin;password=sqlsql";
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = new MySqlCommand(query, connection);
            var reader = command.ExecuteReader();
            return (connection, reader);
        }
    }
}