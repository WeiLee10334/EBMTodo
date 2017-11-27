using Newtonsoft.Json;
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
            //Random rnd = new Random();
            //var projects = db.TodoEBMProject.ToList();

            //var lineUsers = db.Line_User.ToList();
            //var id = db.AspNetUsers.First().Id;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var pid = projects[rnd.Next(0, projects.Count)].PID;
            //    var uid = lineUsers[rnd.Next(0, lineUsers.Count)].UID;
            //    DateTime dt = DateTime.Now.AddDays(rnd.Next(-30, 30));
            //    var obj = new TodoEBMProjectWorking()
            //    {
            //        CreateDateTime = dt,
            //        Description = "Description" + rnd.Next(0, 9999),
            //        RecordDateTime = dt,
            //        workingType = i % 4 + 1,
            //        WokingHour = rnd.Next(0, 10),
            //        Target = "target" + rnd.Next(0, 9999),
            //        PID = pid,
            //        LineUID = uid,
            //        Id = id
            //    };
            //    db.TodoEBMProjectWorking.Add(obj);
            //    db.SaveChanges();
            //    Console.WriteLine(JsonConvert.SerializeObject(obj));
            //}

            WorkingLib lib = new WorkingLib();
            lib.getData(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(30), db.Line_User.Take(1).Select(x => x.UID).ToList());
            Console.ReadLine();
        }
    }
}