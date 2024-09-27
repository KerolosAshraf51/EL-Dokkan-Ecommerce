namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IMailRepo
    {
        public Task<string> SendEmail(string toAddress, string subject, string body);
    }
}
