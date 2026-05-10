using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Domain.Entities;

namespace CertiForge.Application.Interfaces.ManageUser
{
    public interface IUserProfileRepository
    {
        Task UpdateUserProfilePicture(int userId, string pictureUrl);

        Task<UserProfile?> GetUserInfoAsync(int userId);
    }
}
