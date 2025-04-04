namespace MVC.Services
{
    public interface IUserService
    {
        bool IsUserValid(string username, string password);
    }

    public class UserService: IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public bool IsUserValid(string username, string password)
        {
            // We're so secure, we only have one admin
            var isCorrectUser = username == "AzureDiamond";
            var isCorrectPass = password == "hunter2";

            return isCorrectUser && isCorrectPass;
        }
    }
}