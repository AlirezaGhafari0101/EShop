using EShop.Application.Convertors;
using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Application.ViewModels.Account;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;

namespace EShop.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> ActiveAccountServiceAsync(string activeCode)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(activeCode);

            if (user == null || user.IsActive)
            {
                return null;
            }

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUnipNDigitCode(6);

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangeAsync();

            var userViewModel = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return userViewModel;

        }

        public async Task ChangeUserPasswordAsync(ChangePasswordViewModel viewMode)
        {
            User user = await _userRepository.GetUserByEmailAsync(viewMode.Email);

            string hashedPassword = PasswordHelper.EncodePasswordMd5(viewMode.Password);

            user.Password = hashedPassword;

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangeAsync();

        }

        public async Task<UserViewModel> CheckForgotPasswordAsync(string code)
        {
            User user = await _userRepository.GetUserByActiveCodeAsync(code);

            user.ActiveCode = NameGenerator.GenerateUnipNDigitCode(6);
            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangeAsync();

            var userViewModel = new UserViewModel()
            {
                Email = user.Email
            };

            return userViewModel;
        }

        public async Task<UserViewModel> ForgotPasswordServiceAsync(string email)
        {
            string Fixedemail = FixedText.FixEmail(email);
            User user = await _userRepository.ForgotPasswordAsync(Fixedemail);
            UserViewModel userViewModel = new UserViewModel() {
                Email = user.Email,
                ActiveCode = user.ActiveCode,
            };

            return userViewModel;
        }

        public async Task<bool> IsExistUserEmailServiceAsync(string email)
        {
            return await _userRepository.IsExistUserEmailAsync(email);
        }

        public async Task<UserViewModel> LoginUserServiceAsync(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);
            string email = FixedText.FixEmail(loginViewModel.Email);

            var user = await _userRepository.LoginUserAsync(email, password);

            UserViewModel userViewModel = new UserViewModel()
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Email = user.Email,
            };
            return userViewModel;
        }

        public async Task<User> UserLoginAsync(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);

            return await _userRepository.LoginAsync(loginViewModel.Email, password);
        }

        public async Task<UserViewModel> UserRegisterAsync(RegisterViewModel registerViewModel)
        {
            var hashedPassword = PasswordHelper.EncodePasswordMd5(registerViewModel.Password);
            User user = new User()

            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = FixedText.FixEmail(registerViewModel.Email),
                Password = hashedPassword,
                ActiveCode = NameGenerator.GenerateUnipNDigitCode(6),
                CreateDate = DateTime.Now,
                Avatar = "Defult.jpg"




            };

         


            await _userRepository.RegisterAsync(user);
            await _userRepository.SaveChangeAsync();

            var userViewModel = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ActiveCode = user.ActiveCode,
            };

            return userViewModel;
        }
    }
}
