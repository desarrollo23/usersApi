
using System.Threading.Tasks;
using User.Common.Response.Security;
using User.Model.Requests;

namespace User.Model.Interfaces.Engine
{
    public interface ISecurityEngine
    {
        Task<AuthenticationResponse> RegisterUser(UserCredentials userCredentials);
        Task<AuthenticationResponse> Login(UserCredentials userCredentials);
    }
}
