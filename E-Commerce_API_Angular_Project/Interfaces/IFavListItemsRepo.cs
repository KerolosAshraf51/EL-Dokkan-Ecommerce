namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IFavListItemsRepo
    {
        Task AddProductToFavList(int userId, int productId);
        Task RemoveProductFromFavList(int userId, int productId);
    }
}
