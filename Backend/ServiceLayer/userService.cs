using System;
using System.Collections.Generic;
using System.Reflection;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;

public class UserService
{
    public UserController userController;
    private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    public UserService(ServiceFactory serviceFactory)
    {
        userController = serviceFactory.UserController;
    }

    /// <summary>
    ///  This method logs in an existing user.
    /// </summary>
    /// <param name="email">The email address of the user to login</param>
    /// <param name="password">The password of the user to login</param>
    /// <returns>Json formatted string, where ErrorMessage = "ok" , unless an error occurs</returns>
    public string login(String email, String password)
    {
        try
        {
            email = email.ToLower();
            User user = userController.login(email, password);
            log.Info("user with email: " + email + " has logged in successfully");
            
            return JsonController<string>.toJson(new Response<string>(null, email));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    /// <summary>
    /// This method creates a new account. 
    /// </summary>
    /// <param name="email">The email of the new user</param>
    /// <param name="password">The password of the new user</param>
    /// <returns>Json formatted string, where ErrorMessage = "ok" , unless an error occurs</returns>
    public string createUser(String email, String password)
    {
        try
        {
            email = email.ToLower();
            User user = userController.createUser(email, password);
            log.Info("user with email " + email + " was created successfully");
            // return new Response<string>(null, null);
            return JsonController<string>.toJson(new Response<string>(null, null));

        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }

    /// <summary>
    /// This method logs out a logged in user. 
    /// </summary>
    /// <returns>Json formatted string, where ErrorMessage = "ok" , unless an error occurs</returns>
    public string logout(string email)
    {
        try
        {
            email = email.ToLower();
            userController.logout(email);
            log.Info("user with email: " + email + "has logged out successfully");
            return JsonController<string>.toJson(new Response<string>(null, null));
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<string>.toJson(new Response<string>(e.Message, null));
        }
    }


    public string GetUserBoards(string email)
    {
        try
        {
            email = email.ToLower();
            List<int> userBoards = userController.getUserBoards(email);
            log.Info("Returned list of board ids of user " + email);
            return JsonController<List<int>>.toJson(new Response<List<int>>(null, userBoards)); //todo: serialize
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<List<int>>.toJson(new Response<string>(e.Message, null));
        }
    }

    public string GetUserBoardsNames(string email)
    {
        try
        {
            email = email.ToLower();
            List<string> userBoards = userController.getUserBoardsNames(email);
            log.Info("Returned list of board ids of user " + email);
            return JsonController<List<string>>.toJson(new Response<List<string>>(null, userBoards)); //todo: serialize
        }
        catch (Exception e)
        {
            log.Debug(e.Message);
            return JsonController<List<string>>.toJson(new Response<string>(e.Message, null));
        }
    }
}
