namespace Domain.Entities
{
    public class UserRoles
    {
        public int UserRolesId { get; set; }

        public int UserId { get; set; }

        public Guid RoleId { get; set; }

        public User? User { get; set; }
    }
}
