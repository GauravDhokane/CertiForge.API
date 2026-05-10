using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.Interfaces.ManageUser;
using CertiForge.Domain.Entities;

namespace CertiForge.Application.Services.ManageUser
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task UpdateUserProfilePicture(int userId, string pictureUrl)
        {
            await userProfileRepository.UpdateUserProfilePicture(userId, pictureUrl);
        }

        public Task<UserProfile?> GetUserInfoAsync(int userId)
        {
            return userProfileRepository.GetUserInfoAsync(userId);
        }
    }
}