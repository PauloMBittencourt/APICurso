using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Repos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IUserRepository : IRepositoryGenerico<User>
    {
        int VerifyRegister();
        Task LoginUser(User usuario, bool lembrar);
        Task<IdentityResult> CreateUser(User usuario, string senha);
        Task IncludeUserInFunction(User usuario, string funcao);
    }
}
