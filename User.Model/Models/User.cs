using User.Model.Base.Entity;

namespace User.Model.Models
{
    public class UserEntity: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
