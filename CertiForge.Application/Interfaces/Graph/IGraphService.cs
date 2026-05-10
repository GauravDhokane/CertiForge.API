using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.DTOs;

namespace CertiForge.Application.Interfaces.Graph
{
    public interface IGraphService
    {
        public Task<List<AdB2CUserModel>> GetADB2CUsersAsync();
    }
}
