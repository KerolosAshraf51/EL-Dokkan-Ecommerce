using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{

    public class UserOtpRepo : IUserOtpRepo
    {
        EcommContext context;
        public UserOtpRepo(EcommContext _context)
        {
            this.context = _context;
        }
        public void Add(UserOtp userOtp)
        {
            UserOtp? temp = context.userOtps
                                   .FirstOrDefault(o => o.userID == userOtp.userID);
            if (temp == null)
            {
                context.Add(userOtp);
            }

            else
            {
                temp.userID = userOtp.userID;
                temp.OTP = userOtp.OTP;
                temp.CreatedAt = userOtp.CreatedAt;
                Update(temp);
            }

        }
        public void Update(UserOtp userOtp)
        {
            context.Update(userOtp);
        }
        public void Delete(UserOtp userOtp)
        {
            context.Remove(userOtp);
        }
        public int GetOTP(int UserId)
        {
            return (context.userOtps.FirstOrDefault(o => o.userID == UserId)).OTP;
        }

        public void save()
        {
            context.SaveChanges();
        }

        public bool isValid(int UserId, int otp) //expired or still valid
        {
            UserOtp userotp = context.userOtps.FirstOrDefault(o => o.userID == UserId);

            if ((DateTime.Now - userotp.CreatedAt).TotalMinutes <= 1)
            {
                return true;
            }


            return false;
        }

        public bool isCorrect(int UserId, int otp)
        {
            UserOtp userotp = context.userOtps.OrderBy(n => n.CreatedAt)
                                             .LastOrDefault(o => o.userID == UserId);

            if (userotp != null && userotp.OTP == otp)
            {
                return true;
            }

            return false;
        }

    
    }
}
