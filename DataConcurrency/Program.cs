// See https://aka.ms/new-console-template for more information


using DataConcurrency;
using Microsoft.EntityFrameworkCore;

AppDbContext appDbContext = new AppDbContext();

#region Pesimistic Lock

var transaction = appDbContext.Database.BeginTransaction();

var sonuc = await appDbContext.Persons.FromSql($"SELECT * FROM Persons WITH (XLOCK) Where PersonID=3").ToListAsync();

Console.WriteLine();
transaction.Commit();
#endregion


#region Optimistic Lock
//burada işlem yapılcak kolona [ConcurencyCheck] attribute eklenir ya da fluentapiden eklenir
var person=appDbContext.Persons.Find(3);
person.PersonName = "efrun";
appDbContext.SaveChanges();
#endregion


