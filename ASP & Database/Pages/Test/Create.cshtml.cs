using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ASP___Database.Pages.Test
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {

        }

        public void OnPost() 
        {
        clientInfo.name = Request.Form["name"];
        clientInfo.email = Request.Form["email"];
        clientInfo.phone = Request.Form["phone"];
        clientInfo.address = Request.Form["address"];

            // Make sure that all the fields are filled
			if (string.IsNullOrEmpty(clientInfo.name) || string.IsNullOrEmpty(clientInfo.email) || 
                string.IsNullOrEmpty(clientInfo.phone) || string.IsNullOrEmpty(clientInfo.address))
            {
                errorMessage = "All the fields are required";
                return;
            }

            // Save the new client into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ASP_DB_TEST;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " + "(name, email, phone, address) VALUES " + "(@name, @email, @phone, @address);";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            successMessage = "The new client was added successfully into the database";

            Response.Redirect("/Test/Index");
		}
    }
}
