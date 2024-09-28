
using System.Net;
using System.Security.Authentication;
using ConsoleApplication.Contracts;
using ConsoleApplication.Contracts.Requests;
using ConsoleApplication.Contracts.Responses;

namespace ConsoleApplication
{
    public class UserInterface
    {
        public bool IsHasAccount()
        {
            while (true)
            {
                var userAnswer = GetInput("Do you have an account? (y/n) ").ToLower();

                switch (userAnswer)
                {
                    case "y":
                        return true;
                    case "n":
                        return false;
                    default:
                        Console.WriteLine("Invalid input. Please answer with (y/n).");
                        break;
                }
            }
        }


        public UserCredential ReadLoginCredentials()
        {
            string username, password;
            var isValidLoginInput = false;

            do
            {
                username = GetInput("Enter your username:");
                password = GetInput("Enter your password:");

                if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Enter your name and password");
                    isValidLoginInput = false;
                }
                else
                {
                    isValidLoginInput = true;
                }
            } while (!isValidLoginInput);

            return new UserCredential
            {
                UserName = username,
                Password = password
            };
        }

        public RegisterRequest ReadRegistrationCredentials()
        {
            string email, username, password;
            bool isValidInput = false;

            do
            {
                email = GetInput("Enter your email:");
                username = GetInput("Enter your username:");
                password = GetInput("Enter your password:");

                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                {
                    Console.WriteLine("Password must be at least 6 characters long and cannot be empty.");
                    isValidInput = false;
                }
                else if (!password.Any(char.IsUpper))
                {
                    Console.WriteLine("Password must contain at least one uppercase letter.");
                    isValidInput = false;
                }
                else if (!password.Any(char.IsDigit))
                {
                    Console.WriteLine("Password must contain at least one number.");
                    isValidInput = false;
                }
                else if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    Console.WriteLine("Password must contain at least one symbol.");
                    isValidInput = false;
                }
                else
                {
                    isValidInput = true;
                }
            } while (!isValidInput);


            return new RegisterRequest
            {
                UserName = username,
                Password = password,
                Email = email
            };
        }

        public void HandleSuccessfulLogin(LoginResponse loginResponse)
        {
            
            Console.WriteLine("Successfully logged in. ");
            Console.WriteLine($"Username: {loginResponse.UserName}. ");
            Console.WriteLine($"Email: {loginResponse.Email}. ");
        }

        public void HandleSuccessfulRegistration()
        {
            Console.WriteLine("Registration successful! You can now log in.");
        }

        public void HandleUnsuccessfulRegistration(Exception exception)
        {
            switch (exception)
            {
                case HttpRequestException:
                    Console.WriteLine("Registration error: Unable to connect to the server. Please try again later. Details: " + exception.Message);
                    break;
                case TaskCanceledException:
                    Console.WriteLine("Registration error: Request timed out. Please try again later: " + exception.Message);
                    break;
            }

            Console.WriteLine("An unexpected error occurred during registration. Details: " + exception.Message);
        }

        public void HandleUnsuccessfulLogin(Exception exception)
        {
            switch (exception)
            {
                case HttpRequestException:
                    Console.WriteLine("Login error: Unable to connect to the server. Please try again later. Details: " + exception.Message);
                    break;
                case TaskCanceledException:
                    Console.WriteLine("Login error: Request timed out. Please try again later: " + exception.Message);
                    break;
                case AuthenticationException:
                    Console.WriteLine("Invalid credentials. Details: " + exception.Message);
                    break;
            }

            Console.WriteLine("An unexpected error occurred during registration. Details: " + exception.Message);
        }

        private string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
    }
}
