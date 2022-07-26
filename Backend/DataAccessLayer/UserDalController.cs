using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDalController : DalController
    {
        private const string UsersTableName = "Users";

        public UserDalController() : base(UsersTableName)
        {

        }

        public List<UserDTO> SelectAllUsers()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();

            return result;
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

            return result;
        }

        public bool Insert(UserDTO user)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {UsersTableName} ( {UserDTO.UsersEmailColumnName},{UserDTO.UsersPasswordColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"passwordVal", user.Password);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Fatal("Couldn't write to " + UsersTableName);

                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }
    }
}
