using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Repos
{
    public class UserRepository : RepositoryGenerico<User>, IUserRepository
    {
        private readonly Context _context;
        private readonly UserManager<User> _gerenciadorUser;
        private readonly SignInManager<User> _gerenciadoLogin;
        public UserRepository(Context context, UserManager<User> gerenciadorUser, SignInManager<User> gerenciadoLogin) : base(context)
        {
            _context = context;
            _gerenciadoLogin = gerenciadoLogin;
            _gerenciadorUser = gerenciadorUser;
        }

        public async Task<IdentityResult> CreateUser(User usuario, string senha)
        {
            try
            {
                return await _gerenciadorUser.CreateAsync(usuario, senha);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task IncludeUserInFunction(User usuario, string funcao)
        {
            try
            {
                await _gerenciadorUser.AddToRoleAsync(usuario, funcao);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task LoginUser(User usuario, bool lembrar)
        {
            try
            {
                await _gerenciadoLogin.SignInAsync(usuario, lembrar);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int VerifyRegister()
        {
            try
            {
                return _context.Users.Count();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
