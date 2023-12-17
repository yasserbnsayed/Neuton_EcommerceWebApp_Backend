using Domain;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<UserProfileDTO> getUserByIdAsync(string id);
        Task<bool> UpdateAsync(string id ,UserProfileDTO user);

    }
}
