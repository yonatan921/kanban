using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;

public class BoardService
{
    private BoardController boardController;
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public BoardService(ServiceFactory serviceFactory)
    {
        boardController = serviceFactory.BoardController;
    }

    /// <summary>
    /// This method creates a new board. 
    /// </summary>
    /// <param name="boardName">The name of the new board</param>
    /// <returns>The string "{}", unless an error occurs</returns>
    public string createBoard(string boardName, string email)
    {
        try
        {
            email = email.ToLower();
            Board board = boardController.addBoard(boardName, email);
            log.Info("Board: " + boardName + "was add by " + email);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }


    /// <summary>
    /// This method removes an existing board. 
    /// </summary>
    /// <param name="boardName">The name of the board to remove</param>
    /// <param name="email">The user that owns the board</param>
    /// <returns>The string "{}", unless an error occurs</returns>
    public string remove(string boardName, string email)
    {
        try
        {
            email = email.ToLower();
            boardController.removeBoard(boardName, email);
            log.Info("Board: " + boardName + "was removed by " + email);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    /// <summary>
    /// This method limits a column in a given board. 
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The id of the requested column</param>
    /// <param name="limit">The new limit of the column</param>
    /// <returns>The string "{}" and the column, unless an error occurs</returns>
    public string limitColumn(string email, string boardName, int columnOrdinal, int limit)
    {
        try
        {
            email = email.ToLower();
            boardController.setColumnLimit(email, boardName, columnOrdinal, limit);
            log.Info("The limit of column " + boardController.getColumnName(email, boardName, columnOrdinal) +
                     " in board " + boardName + " was set to " + limit);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    /// <summary>
    /// This method returns the limit of a requested column from a given board. 
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The id of the requested column</param>
    /// <returns>The string "{}" and the limit of the column, unless an error occurs</returns>
    public string getColumnLimit(string email, string boardName, int columnOrdinal)
    {
        try
        {
            email = email.ToLower();
            int columnLimit = boardController.getColumnLimit(email, boardName, columnOrdinal);
            log.Info("The limit of column " + boardController.getColumnName(email, boardName, columnOrdinal) +
                     " in board " + boardName + " has been accessed");
            return JsonController<Object>.toJson(new Response<Object>(null, columnLimit)); //todo change
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<Object>.toJson(new Response<Object>(e.Message, null));
        }
    }

    /// <summary>
    /// This method returns the name of a requested column from a given board. 
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The id of the requested column</param>
    /// <returns>The string "{}" and the column name, unless an error occurs</returns>
    public string getColumnName(string email, string boardName, int columnOrdinal)
    {
        try
        {
            email = email.ToLower();
            string columnName = boardController.getColumnName(email, boardName, columnOrdinal);
            log.Info("The name of column " + boardController.getColumnName(email, boardName, columnOrdinal) +
                     " in board " + boardName + " has been accessed");
            return JsonController<string>.toJson(new Response<string>(null, columnName)); //todo: change
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    /// <summary>
    /// This method returns a requested column from a given board. 
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="boardName">The name of the board</param>
    /// <param name="columnOrdinal">The id of the requested column</param>
    /// <returns>The string "{}" and the column, unless an error occurs</returns>
    public string getColumn(string email, string boardName, int columnOrdinal)
    {
        try
        {
            email = email.ToLower();
            List<Task> column = boardController.getColumn(email, boardName, columnOrdinal);
            log.Info("The name of column " + boardController.getColumnName(email, boardName, columnOrdinal) +
                     " in board " + boardName + " has been accessed");
            return JsonController<List<Task>>.toJson(new Response<List<Task>>(null, column)); //todo
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<List<Task>>.toJson(new Response<List<Task>>(e.Message, null));
        }
    }



    public string joinBoard(string email, int id)
    {
        try
        {
            email = email.ToLower();
            boardController.joinBoard(email, id);
            log.Info("user: " + email + " joined board: " + id);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    public string transferOwnerShip(string currentOwnerEmail, string newOwnerEmail, string boardName)
    {
        try
        {
            currentOwnerEmail = currentOwnerEmail.ToLower();
            newOwnerEmail = newOwnerEmail.ToLower();
            boardController.transferOwnerShip(currentOwnerEmail, newOwnerEmail, boardName);
            log.Info("user: " + currentOwnerEmail + " transfered: " + boardName + " to user: " + newOwnerEmail);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    public string leaveBoard(string email, int boardId)
    {
        try
        {
            email = email.ToLower();
            boardController.leaveBoard(email, boardId);
            log.Info("user: " + email + " left board: " + boardId);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    public string removeBoard(string email, string name)
    {
        try
        {
            email = email.ToLower();
            boardController.removeBoard(name, email);
            log.Info("user: " + email + " deleted board: " + name);
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    public string getBoardName(int boardId)
    {
        try
        {
            string boardName = boardController.getBoardName(boardId);
            log.Info("returned board name = " + boardName);
            return JsonController<string>.toJson(new Response<string>(null, boardName)); //todo
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }
}