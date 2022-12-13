using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserView> GetUserByEmail(string email)
        {
            if (await _userManager.FindByEmailAsync(email.Trim().Normalize()) is not { } user)
            {
                throw new NotFoundException("Wrong email or user doesn't exist.");
            }

            var userView = _mapper.Map<UserView>(user);
            return userView;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                RegistrationDate = DateTime.UtcNow,
                NormalizedUserName = request.UserName                
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new InvalidRequestException(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description).ToArray()));
            }
        }
    }
}
