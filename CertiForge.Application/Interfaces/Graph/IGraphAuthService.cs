using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertiForge.Application.Interfaces.Graph
{
    public interface IGraphAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}
