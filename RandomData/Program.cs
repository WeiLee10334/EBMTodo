using RandomData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomData
{
    class Program
    {
        static void Main(string[] args)
        {
            EBMTodo db = new EBMTodo();
            Random rnd = new Random();
            var pid = db.TodoEBMProject.ToList()[1].PID;
            var uid = db.Line_User.ToList()[2].UID;
            var id = db.AspNetUsers.First().Id;
            for (int i = 0; i < 100; i++)
            {
                db.TodoEBMProjectWorking.Add(new TodoEBMProjectWorking()
                {
                    CreateDateTime = DateTime.Now,
                    Description = "test" + rnd.Next(0, 9999),
                    RecordDateTime = DateTime.Now,
                    workingType = i % 4 + 1,
                    WokingHour = rnd.Next(0, 10),
                    Target = "target" + rnd.Next(0, 9999),
                    PID = pid,
                    LineUID = uid,
                    Id = id
                });
                db.SaveChanges();
            }


        }
    }
}