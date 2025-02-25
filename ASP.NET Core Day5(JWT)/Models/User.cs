namespace JwtAuthApi.Models
{

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UserManager
    {
        private List<User> _users = new List<User>();

        public List<User> GetUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public User UpdateUser(User User)
        {
            var user = _users.FirstOrDefault(u => u.Username == User.Username);
            if (user != null)
            {
                user.Email = User.Email;
                user.Age = User.Age;
                user.Role = User.Role;
                return user;
            }
            return user;
        }
    }

}
