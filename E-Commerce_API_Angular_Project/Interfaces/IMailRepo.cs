namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IMailRepo
    {
        public void SendEmail(string toAddress, string subject, string body);
    }
}
