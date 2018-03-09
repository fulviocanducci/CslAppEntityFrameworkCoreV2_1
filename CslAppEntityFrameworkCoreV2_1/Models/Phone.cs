using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Phone
    {
        protected ILazyLoader LazyLoader { get; }

        public Phone(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        private User _user;
        public User User
        {
            get => LazyLoader.Load(this, ref _user);
            set => _user = value;
        }

        public string Ddd { get; set; }
        public string Number { get; set; }
        
    }
}
