using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardsMembersDalController : DalController
    {
        private const string BoardsMembersTableName = "BoardsMembers";

        public BoardsMembersDalController() : base(BoardsMembersTableName)
        {

        }

        public List<BoardsMembersDTO> SelectAllBoardsMembersDtos()
        {
            List<BoardsMembersDTO> result = Select().Cast<BoardsMembersDTO>().ToList();

            return result;
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            BoardsMembersDTO result = new BoardsMembersDTO((int)(long)reader.GetValue(0), (int)(long)reader.GetValue(1));

            return result;
        }
        public bool Insert(BoardsMembersDTO boardsMembersDTO)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {BoardsMembersTableName} ( {BoardsMembersDTO.boardIdColumn},{BoardsMembersDTO.memberIdColumn}) " +
                                          $"VALUES (@boardIdVal,@memberIdVal);";

                    SQLiteParameter memberIdParam = new SQLiteParameter(@"memberIdVal", boardsMembersDTO.MemberID);
                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", boardsMembersDTO.BoardId);

                    command.Parameters.Add(memberIdParam);
                    command.Parameters.Add(boardIdParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Fatal("Couldn't write to " + BoardsMembersTableName);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }

        public bool Delete(BoardsMembersDTO boardsMembersDto)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {BoardsMembersTableName} where board_id={boardsMembersDto.BoardId} and member_id={boardsMembersDto.MemberID}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool DeleteBoard(int boardId)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {BoardsMembersTableName} where board_id={boardId}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }
    }
}
