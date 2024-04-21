using One_Time_Password.Data;
using System.Text;

namespace One_Time_Password.Service
{
    public class OneTimePasswordService
    {
        private readonly OneTimePasswordRepository repository;
        private const int Otp_Length = 4; 

        public OneTimePasswordService(OneTimePasswordRepository repository)
        {
            this.repository = repository;
        }

        public string GenerateOtpForUser(string userEmail)
        {
            var user = new User()
            {
                Email = userEmail,
                Password = GenerateOTP(),
                CreatedTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()
            };
            var users = repository.Deserialize<User>();

            var existingUser = users.Find(u => u.Email == userEmail);
            if (existingUser != null) 
            {
                users.Remove(existingUser);
            }

            users.Add(user);
            repository.Serialize(users);

            return $"OTP successfully generated: {user.Password}";
        }

        public string CheckOtpForUser(string userEmail, string password)
        {
            var users = repository.Deserialize<User>();
            var user = users.Find(u => u.Email == userEmail);

            if (user is null)
            {
                return "Does not exist OTP generated for this user";
            }

            if (password != user.Password)
            {
                return "Invalid password";
            }

            return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() - user.CreatedTime > 300  // if password is not expired(5 min = 300 sec)
                ? "Your password is valid just for 5 minutes and it has been expired"
                : "Valid password";
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            StringBuilder otp = new StringBuilder();

            for (int i = 0; i < Otp_Length; i++)
            {
                otp.Append(random.Next(0, 10)); 
            }

            return otp.ToString();
        }
    }
}
