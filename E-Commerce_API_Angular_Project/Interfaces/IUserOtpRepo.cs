using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IUserOtpRepo
    {
        public void Add(UserOtp userOtp);
        public void Update(UserOtp userOtp);
        public void Delete(UserOtp userOtp);
        public int GetOTP(int UserId);
        public void save();

        public bool isValid(int UserId , int otp);
        public bool isCorrect(int UserId, int otp);


    }
}
