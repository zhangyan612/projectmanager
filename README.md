# projectmanager
A Project Management application for agile teams

This app is build on ASP.NET MVC on backend and use Gantt chart js framework as UI

Data Models:
Project
Tasks
Links
GanttRequest
Teams

To do:


//Generic read
public static string Read(string query, params object[] args)
        {
            var dataTable = new DataTable();

            //string qry = "Select [EMAIL_ADDR] FROM[Op_Intel].[pnr].[WebUsers] " +
            //             "where [FirstName] + ' ' + [LastName] = @name ";
            //string email = string.Empty;

            using (AdoHelper db = new AdoHelper("14a_Op_Intel", true))
            using (SqlDataReader rdr = db.ExecDataReader(query, args))
            {
                while (rdr.Read())
                {
                    dataTable.Load(rdr);

                }
            }
            string json = JsonConvert.SerializeObject(dataTable);

            return json;
        }
