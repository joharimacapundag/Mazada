using Mazada.Model;
using Mazada.Services;
using System;
using System.Text.RegularExpressions;
//using System.Text.Json;
using System.Windows;

namespace Mazada.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MySQLRepository<User> _repo = new MySQLRepository<User>();

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        // --------------------------
        // Commands
        // --------------------------
        public RelayCommand RegisterCommand =>
            new RelayCommand(e => Register(), e => FieldsAreValid());


        // --------------------------
        // Registration Logic
        // --------------------------
        private async void Register()
        {
            if (!FieldsAreValid())
            {
                MessageBox.Show("Invalid input. Please check your entries.");
                return;
            }

            try
            {
                var user = new User
                {
                    Username = Username,
                    Email = Email,
                    Password = Password
                };

                await _repo.AddAsync(user);

                // Auto-login after register
                //SaveUserSession(user);

                MessageBox.Show("Registration complete. You are now logged in!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // --------------------------
        // Validation Helpers (Regex)
        // --------------------------
        private bool FieldsAreValid()
        {
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
                return false;

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;

            if (Password != ConfirmPassword)
                return false;

            // Optional: Add strong password rule
            // if (!Regex.IsMatch(Password, @"^(?=.*[A-Z])(?=.*\d).{6,}$"))
            //     return false;

            return true;
        }

        //// ----------------------------------
        //// JSON SESSION SAVE
        //// ----------------------------------
        //private string JsonPath => Path.Combine(
        //    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        //    "mazada",
        //    "session.json"
        //);

        //private void SaveUserSession(User user)
        //{
        //    Directory.CreateDirectory(Path.GetDirectoryName(JsonPath));

        //    var data = new
        //    {
        //        IsLoggedIn = true,
        //        Username = user.Username,
        //        Email = user.Email
        //    };

        //    var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

        //    File.WriteAllText(JsonPath, json);
        //}
    }
}
