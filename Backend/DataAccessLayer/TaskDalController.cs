using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDalController : DalController
    {
        private const string TasksTableName = "Tasks";

        public TaskDalController() : base(TasksTableName)
        {

        }

        public List<TaskDTO> SelectAllTasks()
        {
            List<TaskDTO> result = Select().Cast<TaskDTO>().ToList();

            return result;
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            // TaskDTO result = new TaskDTO( reader.GetInt64(0),reader.GetString(1),reader.GetString(2), reader.GetInt32(3),reader.GetDateTime(4),reader.GetDateTime(5),reader.GetString(6),reader.GetString(7));
            TaskDTO result = new TaskDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetString(6), reader.GetString(7));
            return result;
        }

        public bool Insert(TaskDTO task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {TasksTableName} ({DTO.IDColumnName} ,{TaskDTO.TasksTitleColumnName}, {TaskDTO.TasksDescriptionColumnName}, {TaskDTO.TasksBoardIdColumnName}, {TaskDTO.TasksCreationTimeColumnName}, {TaskDTO.TasksDueDateColumnName}, {TaskDTO.TaskColumnOrdianlName}, {TaskDTO.TaskAssigneeName}) " +
                        $"VALUES (@idVal,@titleVal,@descriptionVal,@boardIdVal,@creationTimeVal,@dueDateVal,@ordinalVal,@assigneeVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", task.id);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"descriptionVal", task.Description);
                    SQLiteParameter boardIdParam = new SQLiteParameter(@"boardIdVal", task.BoardId);
                    SQLiteParameter creationTimeParam = new SQLiteParameter(@"creationTimeVal", task.CreationTime);
                    SQLiteParameter dueDateParam = new SQLiteParameter(@"dueDateVal", task.DueDate);
                    SQLiteParameter ordinalParam = new SQLiteParameter(@"ordinalVal", task.ColumnOrdinal);
                    SQLiteParameter assignieParam = new SQLiteParameter(@"assigneeVal", task.Assignee);


                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(creationTimeParam);
                    command.Parameters.Add(dueDateParam);
                    command.Parameters.Add(ordinalParam);
                    command.Parameters.Add(assignieParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Fatal("Couldn't write to " + TasksTableName);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }

        public bool Advance(int taskId, string newColumnOrdinal)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText =
                        $"UPDATE {TasksTableName} SET column_ordinal = @ordinal Where id = @taskId; ";
        
        
                    SQLiteParameter taskidParam = new SQLiteParameter(@"taskId", taskId);
                    SQLiteParameter ordinalParam = new SQLiteParameter(@"ordinal", newColumnOrdinal);
        
                    command.Parameters.Add(taskidParam);
                    command.Parameters.Add(ordinalParam);
        
        
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
        
                }
                return res > 0;
            }
        }

        // public bool Assign(int taskId, string asignee)
        // {
        //     using (var connection = new SQLiteConnection(_connectionString))
        //     {
        //         SQLiteCommand command = new SQLiteCommand(null, connection);
        //         int res = -1;
        //         try
        //         {
        //             connection.Open();
        //             command.CommandText =
        //                 $"UPDATE {TasksTableName} SET asignee = @newAsignee Where id = @taskid; ";
        //
        //
        //             SQLiteParameter taskidParam = new SQLiteParameter(@"taskid", taskId);
        //             SQLiteParameter assigneeParam = new SQLiteParameter(@"newAsignee", asignee);
        //
        //             command.Parameters.Add(taskidParam);
        //             command.Parameters.Add(assigneeParam);
        //
        //
        //             command.Prepare();
        //             res = command.ExecuteNonQuery();
        //         }
        //         catch (Exception ex)
        //         {
        //             //log error
        //         }
        //         finally
        //         {
        //             command.Dispose();
        //             connection.Close();
        //
        //         }
        //         return res > 0;
        //     }
        // }

        public bool DeleteBoard(int boardId)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {TasksTableName} where board_id={boardId}"
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
