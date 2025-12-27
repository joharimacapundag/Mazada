using Mazada.Model;
using Mazada.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mazada.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        private MySQLRepository<User> userRepo = new MySQLRepository<User>();

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SignUpCommand => new RelayCommand(e => SignUpAsync(), e => 
        !string.IsNullOrEmpty(Username) && 
        !string.IsNullOrEmpty(Email) &&
        !string.IsNullOrEmpty(Password) &&
        !string.IsNullOrEmpty(ConfirmPassword)
        );
        public RelayCommand DeleteCommand => new RelayCommand(e => DeleteAsync());

      
        //Add user account
        public async void SignUpAsync()
        {

            User user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            await userRepo.AddAsync(user);

            await LoadUsersAsync();
            Reset();
        }
        //Delete user account
        public async void DeleteAsync()
        {
            if (SelectedUser != null) await userRepo.DeleteAsync(SelectedUser);

            await LoadUsersAsync();
        }
        public async Task LoadUsersAsync()
        {
            Users.Clear();

            var allUsers = await userRepo.GetAllAsync();

            foreach (var user in allUsers)
                Users.Add(user);
        }

        private void Reset()
        {
            Username = null;
            Email = null;
            Password = null;
            ConfirmPassword = null;
        }
    }
}
