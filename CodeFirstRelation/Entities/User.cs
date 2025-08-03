namespace CodeFirstRelation.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}