using Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CslAppEntityFrameworkCoreV2_1
{
    class Program
    {
        static void Main(string[] args)
        {
                        
            ILoggerFactory loggerFactory = new LoggerFactory();
            DiagnosticsLogger<DbLoggerCategory.Infrastructure> logger =
                new DiagnosticsLogger<DbLoggerCategory.Infrastructure>(loggerFactory, null, null);

            using (DatabasecoreContext db = new DatabasecoreContext())
            {

                #region a
                //var a = db.Credit.ToArray();


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
                //var b = db.VNoticeCredit.Where(x => x.Id == 1).FirstOrDefault();

                //db.SaveChanges();

                //People p = new People(0, "Mary", false);
                //db.People.Add(p);
                //db.SaveChanges();

                //var peoples = db.People.ToList();

                //var v1 = db.VNoticeCredit.Where(x => x.Id == 1).FirstOrDefault();
                //var v2 = db.VNoticeCredit.Where(x => x.Status == "Active").ToList();                
                #endregion

                var itens = db.VNoticeCredit.ToList();
                var usr1 = db.User.Find(1);
                //var usr = new User(new LazyLoader(new CurrentDbContext(db),logger));

            }
            Console.WriteLine("Hello World!");
        }
    }
}
