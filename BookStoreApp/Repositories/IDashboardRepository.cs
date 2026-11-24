namespace BookStoreApp.Repositories
{
    public interface IDashboardRepository
    {
        int GetTotalBooksCount();
        int GetTotalAuthorsCount();
    }
}