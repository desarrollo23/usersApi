
using User.Common.Response;
using User.Model.DTOs;

namespace User.Model.Interfaces.Engine
{
    public interface IUserEngine
    {
        EntityResponse Create(UserEntityDTO userDTO);

        EntityResponse GetUsers();

        EntityResponse GetUserById(int id);

        EntityResponse UpdateUser(UserEntityDTO userDTO, int id);
    }
}
