using ConsoleApplication;
using ConsoleApplication.Contracts;
using ConsoleApplication.Contracts.Requests;

class Program
{
    private const string ApiUrl = "http://localhost:5027/api/";
    private static readonly UserService UserService = new UserService(ApiUrl);
    private static readonly UserInterface UserInterface = new UserInterface();
    private static async Task Main(string[] args)
    {

        while (true)
        {
            var isHasAccount = UserInterface.IsHasAccount();
            if (isHasAccount)
            {
                var loggedIn = await HandleLogin();
                if (loggedIn) break;
            }
            else
                await HandleRegistration();
        }

        Console.ReadKey();
    }

    private static async Task<bool> HandleLogin()
    {
        try
        {
            var loginData = UserInterface.ReadLoginCredentials();
            var user = new UserCredential
            {
                UserName = loginData.UserName,
                Password = loginData.Password
            };
            var loginResponse = await UserService.LoginUser(user);

            UserInterface.HandleSuccessfulLogin(loginResponse);

            return true;
        }
        catch (Exception e)
        {
            UserInterface.HandleUnsuccessfulLogin(e);
            return false;
        }
    }

    private static async Task<bool> HandleRegistration()
    {
        try
        {
            var registerData = UserInterface.ReadRegistrationCredentials();
            var registerUserRequest = new RegisterRequest()
            {
                Password = registerData.Password,
                UserName = registerData.UserName,
                Email = registerData.Email
            };
            await UserService.RegisterUser(registerUserRequest);

            UserInterface.HandleSuccessfulRegistration();

            return true;
        }
        catch (Exception e)
        {
            UserInterface.HandleUnsuccessfulRegistration(e);
            return false;
        }
    }
}
