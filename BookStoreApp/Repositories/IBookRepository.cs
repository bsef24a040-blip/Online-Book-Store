using System.Collections.Generic;
using BookStoreApp.Models;
using BookStoreApp.ViewModels;

namespace BookStoreApp.Repositories
{
    public interface IBookRepository
    {
        List<BookCardViewModel> GetAllBooks();
        void AddBook(Book book);
        void DeleteBook(int id);
    }
}