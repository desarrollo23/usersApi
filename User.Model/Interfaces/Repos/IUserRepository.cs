
using User.Model.Interfaces.Base.Repository;
using User.Model.Models;

namespace User.Model.Interfaces.Repos
{
    public interface IUserRepository: IRepository<UserEntity>
    {
    }
}
