using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsDemo;

namespace WinFormsDemo
{
        // Authentication class
        public class authenticate
        {
            public class User
            {
                public string Username { get; set; } // User's username property
            }

            public class DatabaseManager
            {
                
                public static User AuthenticateUser(string username, string password)
                {
                    User user = RetrieveUserInformation(username, password);
                    return user;
                }

                private static User RetrieveUserInformation(string username, string password)
                {
                    
                    using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAB109PC06\SQLEXPRESS; Initial Catalog=Intel; Integrated Security=True;"))
                    {
                        sqlCon.Open(); 

                        string Query = "SELECT COUNT(1) FROM Arsen WHERE Username=@Username AND Password=@Password";

                        SqlCommand cmd = new SqlCommand(Query, sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Username", username); 
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar()); 

                        if (count == 1)
                        {
                            
                            return new User { Username = username };
                        }
                        return null; 
                    }
                }
            }

        }
}
