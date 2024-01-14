using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Category { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
