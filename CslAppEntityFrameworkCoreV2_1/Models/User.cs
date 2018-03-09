using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        protected ILazyLoader LazyLoader { get; }

        public User(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private ICollection<Phone> _phones;
        public ICollection<Phone> Phones
        {
            get => LazyLoader.Load(this, ref _phones);
            set => _phones = value;
        }        
    }
}
