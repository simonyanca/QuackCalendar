using MySql.Data.MySqlClient;
using QuackCalendar.Model;

namespace QuackCalendar.Service.Manager.Gateway
{
    internal sealed class MySqlGateway : SqlGatewayBase
    {
        protected override QCGetEventsResponse GetEventsCore(QCGetEventsRequest qcGetEventsRequest)
        {
            //var connectionString = "server=192.168.1.103;port=3306;user=admin;password=sqlsql";
            //var connection = new MySqlConnection(connectionString);
            //connection.Open();

            //var sqlQueryString = "SELECT * FROM quackcalendar.events;";
            //var command = new MySqlCommand(sqlQueryString, connection);
            //var returnValue = command.ExecuteReader();
            //while (returnValue.Read())
            //{
            //    string a = string.Empty;

            //    for (int i = 0; i < returnValue.FieldCount; i++)
            //    {
            //        a += returnValue[i] + " ";
            //    }

            //    a = a;
            //}

            //connection.Close();

            var results = ExecuteQuery("SELECT * FROM quackcalendar.events;", 5);
            var response = new QCGetEventsResponse();

            return response;
        }

        private MySqlDataReader ExecuteQuery(string queryString, int expectedColumns)
        {
            using (var connection = new MySqlConnection("server=192.168.1.103;port=3306;user=admin;password=sqlsql"))
            {
                connection.Open();
                var command = new MySqlCommand(queryString, connection);
                var reader = command.ExecuteReader();
                connection.Close();

                if (reader.FieldCount != expectedColumns)
                {
                    throw new System.Exception($"wrong number of expected columns:\nexpected{expectedColumns} but got {reader.FieldCount}");
                }

                return reader;
            }
        }
    }
}