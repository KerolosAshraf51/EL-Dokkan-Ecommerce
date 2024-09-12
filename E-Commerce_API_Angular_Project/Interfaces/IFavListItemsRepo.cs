namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IFavListItemsRepo
    {
        void AddProductToFavList(int userId, int productId);
        void RemoveProductFromFavList(int userId, int productId);
    }
}
