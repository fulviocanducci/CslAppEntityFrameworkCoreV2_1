using Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
namespace CslAppEntityFrameworkCoreV2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DatabasecoreContext db = new DatabasecoreContext())
            {
                var a = db.Credit.ToArray();


                //var notice = db.Notice                    
                //    .SingleOrDefault(x => x.Id == 1);

                //var credit = db.Credit
                //    .SingleOrDefault(x => x.Id == 1);

                //credit.Status = State.Active;

                //User usr = new User
                //{
                //    Username = "Senior",
                //    Password = "abcdef123456"
                //};

                //db.Add(usr);

                //var groupUser = db.Credit
                //    .GroupBy(x => x.Status)
                //    .Select(s => new
                //    {
                //        Status = s.Key, 
                //        Count = s.LongCount()
                //    })
                //    .ToList();

                //var result = db.Query<User>();

                //db.SaveChanges();

                //db.Credit.Where(x => x.Status == State.Active).ToList();

                //var result = db.User.FromSql("SELECT * FROM [User] ORDER BY Id ASC")
                //    .ToList();

                //var a = db.LayoutView.ToList();

            }
            Console.WriteLine("Hello World!");
        }
    }
}
