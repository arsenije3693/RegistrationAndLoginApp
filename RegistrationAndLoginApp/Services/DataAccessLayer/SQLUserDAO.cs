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
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                // 1) Insert user and get the new Id
                const string insertUserSql =
                    "INSERT INTO Users (Username, PasswordHash) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Username, @PasswordHash);";

                int newId;
                using (var cmd = new SqlCommand(insertUserSql, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.Password); // assuming already hashed
                    newId = (int)cmd.ExecuteScalar();
                }

                // 2) Insert group memberships via helper
                if (!AddGroupMemberships(connection, transaction, newId, user.Groups))
                {
                    transaction.Rollback();
                    return -1;
                }

                // 3) All good → commit
                transaction.Commit();
                return newId;
            }
            catch
            {
                try { transaction.Rollback(); } catch { /* ignore */ }
                return -1;
            }
        }// end of method


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
            if (groups == null || groups.Count == 0)
                return true; // nothing to insert = success

            string query =
                "INSERT INTO GroupMemberships (UserId, GroupId) " +
                "VALUES (@UserId, @GroupId);";

            foreach (GroupDomainModel group in groups)
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@GroupId", group.Id); // or group.GroupId
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {
                    // any insert failed; let caller decide to rollback
                    return false;
                }
            }

            // all inserts succeeded
            return true;
        }


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
