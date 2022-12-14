using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineGameStore.Application.Auth;
using OnlineGameStore.Application.Exceptions;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Application.Models.Views;
using OnlineGameStore.Application.Services.Constants;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Infrastructure.Identity;

namespace OnlineGameStore.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IStorageService _storageService;

        public UserService(UserManager<User> userManager, IMapper mapper, ICurrentUser currentUser, 
            IStorageService storageService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _currentUser = currentUser;
            _storageService = storageService;
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

        public async Task UpdateAvatarAsync(IFormFile image)
        {
            var imageUrl = await _storageService.UploadImageAsync(image, FolderNamesConstants.AccountPictures);

            var userId = Guid.Parse(_currentUser.GetUserId()).ToString();
            var user = await _userManager.FindByIdAsync(userId) 
                ?? throw new NotFoundException("User with such id not found.");

            user.AvatarUrl = imageUrl;

            var isUpdated = await _userManager.UpdateAsync(user);

            if (!isUpdated.Succeeded)
            {
                throw new ServerErrorException("Can't update user's avatar.", null);
            }
        }
    }
}
