using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.Interfaces.ManageUser;
using CertiForge.Domain.Entities;
using CertiForge.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertiForge.Infrastructure
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly CertiForgeContext _context;

        public UserProfileRepository(CertiForgeContext context)
        {
            _context = context;
        }

        public async Task UpdateUserProfilePicture(int userId, string pictureUrl)
        {
            var user = await _context.UserProfiles.FindAsync(userId);
            if (user != null)
            {
                user.ProfileImageUrl = pictureUrl;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserProfile?> GetUserInfoAsync(int userId)
        {
            var user = await _context.UserProfiles.FirstOrDefaultAsync(f => f.UserId == userId);
            return user;
        }
    }
}