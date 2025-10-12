//Arsenije Brajovic
// CST - 350
// 10/3/2025
// Registration and Login Application - DAO for SQL
// ACT 3 part 3

using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
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

        private string query = string.Empty;

        // <inheritdoc />
        public int AddUser(UserDomainModel user)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insert user
                query = "INSERT INTO Users (Username, PasswordHash) " +
                        "OUTPUT INSERTED.Id " +
                        "VALUES (@Username, @PasswordHash);";

                int newId;
                using (var cmd = new SqlCommand(query, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.Password);
                    newId = (int)cmd.ExecuteScalar();
                }

                // Insert memberships via helper
                if (!AddGroupMemberships(connection, transaction, newId, user.Groups))
                {
                    transaction.Rollback();
                    return -1;
                }

                transaction.Commit();
                return newId;
            }
            catch
            {
                try { transaction.Rollback(); } catch { }
                return -1;
            }
        }//end of method



        /// <summary>
        /// Inserts GroupMembership rows for a user inside the given SQL transaction.
        /// Returns true if all inserts succeed; otherwise false (no commit is performed here).
        /// </summary>
        /// <param name="connection">An open SqlConnection.</param>
        /// <param name="transaction">The active SqlTransaction to enlist the inserts in.</param>
        /// <param name="userId">The Id of the user just created.</param>
        /// <param name="groups">List of groups the user should belong to.</param>
        private bool AddGroupMemberships(SqlConnection connection, SqlTransaction transaction, int userId, List<GroupDomainModel> groups)
        {
            if (groups == null || groups.Count == 0) return true;

            query = "INSERT INTO GroupMemberships (UserId, GroupId) VALUES (@UserId, @GroupId);";

            foreach (var grp in groups)
            {
                try
                {
                    using var command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@GroupId", grp.Id);
                    command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }



        // <inheritdoc />
        public List<GroupDomainModel> GetAllGroups()
        {
            var groups = new List<GroupDomainModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection
                connection.Open();

                // Define the query
                query = "SELECT * FROM Groups;";

                // Create a new SQL command
                using (SqlCommand command = new SqlCommand(query, connection))
                // Execute the command and capture the results in a SQL data reader
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loop through the rows in the reader
                    while (reader.Read())
                    {
                        // Create and add a new Group Domain Model
                        groups.Add(new GroupDomainModel
                        {
                            // GetOrdinal -> gets the column index by name
                            // GetInt32   -> reads the value as int
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            // GetString  -> reads the value as string
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        });
                    }

                }
            }

            // Return the found list of groups
            return groups;
        }



        // <inheritdoc />
        public List<GroupDomainModel> GetGroups()
        {
            throw new NotImplementedException();
        }
        // <inheritdoc />
        /// <inheritdoc/>
        public (bool wasUserFound, UserDomainModel? foundUser) GetUserFromUsername(string userUsername)
        {
            UserDomainModel? foundUser = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                query =
           "SELECT " +
           "  Users.Id AS UserId, " +
           "  Users.Username AS Username, " +
           "  Users.Password AS Password, " +   
           "  Groups.Id AS GroupId, " +
           "  Groups.Name AS GroupName " +
           "FROM Users " +
           "LEFT JOIN GroupMemberships ON Users.Id = GroupMemberships.UserId " +
           "LEFT JOIN Groups ON GroupMemberships.GroupId = Groups.Id " +
           "WHERE Users.Username = @Username;";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", userUsername);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Set the user if the user is null
                            if (foundUser == null)
                            {
                                foundUser = new UserDomainModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Groups = new List<GroupDomainModel>()
                                };
                            }

                            // Check if the row has a GroupId
                            if (!reader.IsDBNull(reader.GetOrdinal("GroupId")))
                            {
                                foundUser.Groups.Add(new GroupDomainModel
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("GroupId")),
                                    Name = reader.GetString(reader.GetOrdinal("GroupName"))
                                });
                            }
                        }

                    }
                }
            }

            return (foundUser != null, foundUser);
        }


    }
}
