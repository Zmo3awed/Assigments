using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    internal class Book
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int NumOfPages { get; set; }
        public int PublishYear { get; set; }
        public bool InStock { get; set; }





    }
}
