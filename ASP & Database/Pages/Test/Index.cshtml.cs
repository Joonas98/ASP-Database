using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ASP___Database.Pages.Test
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> clientsList = new List<ClientInfo>();

        public void OnGet()
        {
            try { 
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ASP_DB_TEST;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    String sql = "SELECT * FROM clients";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientInfo clientInfo = new ClientInfo();

                            clientInfo.id = reader.GetInt32(0).ToString();
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.phone = reader.GetString(3);
                            clientInfo.address = reader.GetString(4);
                            clientInfo.created_at = reader.GetDateTime(5).ToString();

                            clientsList.Add(clientInfo);
                        }
                    }
                }
                }
			}
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " +  ex.Message);
            }

		}
    }

    public class ClientInfo
    {
        public string id, name, email, phone, address, created_at;
    }
}
