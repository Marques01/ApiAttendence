namespace Domain.Entities
{
    public class Roles
    {
        public Guid RoleId { get; set; }

        public string Name { get; set; } = string.Empty;

        public Roles()
        {
            RoleId = Guid.NewGuid();
        }
    }
}
