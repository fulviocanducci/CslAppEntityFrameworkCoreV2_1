using System.Collections.Generic;
namespace Models
{

    public partial class Credit
    {
        public Credit()
        {
            Notice = new HashSet<Notice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public State Status { get; set; }

        public virtual ICollection<Notice> Notice { get; set; }
    }
}
