//Arsenije Brajovic
// CST - 350
// 10/3/2025
// Registration and Login Application - DAO for SQL
// ACT 3 part 3

using Microsoft.Data.SqlClient;
using RegistrationAndLoginApp.Models.DomainModels;

//Arsenije Brajovic
// CST - 350
// 10/3/2025
// Registration and Login Application - DAO for SQL
// ACT 3 part 3

using RegistrationAndLoginApp.Services.Interfaces;

namespace RegistrationAndLoginApp.Services.DataAccessLayer
{
    public class SQLUserDAO : IUserDAO
    {
        // class level variables
        private readonly string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RegisterAndLoginAppDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // <inheritdoc />
        public int AddUser(UserDomainModel user)
        {
            // declare variable
            SqlTransaction transaction;
            string query;
            int newId = -1; 

            // create a new sql connection object
            // using a statement to handle resource management
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //open the connection
                connection.Open();

                // bgin a sql transaction
                // ensures that both user and group memberships works correctly
                // if one fails, both are cacncelled
                transaction = connection.BeginTransaction();

                // Create the query for addding a new user
                query = "INSERT INTO Users (Username, PasswordHash)" +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Username, @PasswordHash);";
                // instead of hard coding values dircertly into th sql string (which can lead to sql injection), we are going to use parameter which are just placeholders


                // select a sql command which will allow a command to run
                // sql command a class in ADO.NET (part of the .NET framework /.NET Core LLibrary)
                // We use to send MSQL statements to the database
                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    // add the parameters to the command object
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", user.Password);

                    // set up try catch for execute statement
                    try
                    {
                        // ececute the query and capture the resulting int by casting result to int
                        // ExecuteScalar runs the SQL command agains the database and returns the first column of the first row in the result set.
                        newId = (int)command.ExecuteScalar();
                    }
                    catch (Exception)
                    { 
                        // Rollback the transaction if the query failed
                        transaction.Rollback();
                        return -1;
                    }
                }


            }
        }// end of method



        // <inheritdoc />
        public List<GroupDomainModel> GetAllGroups()
        {
            throw new NotImplementedException();
        }
        // <inheritdoc />
        public List<GroupDomainModel> GetGroups()
        {
            throw new NotImplementedException();
        }
        // <inheritdoc />
        public (bool wasUserFound, UserDomainModel? foundUser) GetUserFromUsername(string userUsername)
        {
            throw new NotImplementedException();
        }
    }
}
