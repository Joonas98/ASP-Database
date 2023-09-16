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

				using SqlConnection connection = new(connectionString);
				connection.Open();
				String sql = "SELECT * FROM clients";

				using SqlCommand command = new(sql, connection);
				using SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					ClientInfo clientInfo = new()
					{
						id = reader.GetInt32(0).ToString(),
						name = reader.GetString(1),
						email = reader.GetString(2),
						phone = reader.GetString(3),
						address = reader.GetString(4),
						created_at = reader.GetDateTime(5).ToString()
					};

					clientsList.Add(clientInfo);
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
