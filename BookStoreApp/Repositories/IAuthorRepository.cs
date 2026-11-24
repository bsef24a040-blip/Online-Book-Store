using System.Collections.Generic;
using BookStoreApp.Models;

namespace BookStoreApp.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> GetAllAuthors();
        void AddAuthor(Author author);
    }
}