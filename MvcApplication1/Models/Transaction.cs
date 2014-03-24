using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Transaction : BaseModel
    {
        public Category Category { get; set; }
        public DateTime TransactionDate { get; set;}
        public decimal Amount { get; set; }
        public string Location { get; set; }
        public string Memo { get; set; }
    }
}