using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Notice
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Credit Credit { get; set; }
    }
}
