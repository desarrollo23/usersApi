using User.Infraestructure.Base.Context;
using User.Infraestructure.Base.Repository;
using User.Model.Interfaces.Repos;
using User.Model.Models;

namespace User.Infraestructure.Repos.Engine
{
    public class UserRepository: Repository<UserEntity>, IUserRepository
    {
        public UserRepository(UserContext userContext): base(userContext)
        {

        }
    }
}
