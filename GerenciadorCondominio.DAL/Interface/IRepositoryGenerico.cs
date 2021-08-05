using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interface
{
    public interface IRepositoryGenerico<TEntity> where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Exclude(TEntity entity);
        Task Exclude(int id);
        Task Exclude(string id);
    }
}
