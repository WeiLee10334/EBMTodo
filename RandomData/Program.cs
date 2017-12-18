using GraphQL.Net;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using RandomData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RandomData
{
    class Program
    {
        static void Main(string[] args)
        {
            EBMTodo db = new EBMTodo();
            var parser = new TextFieldParser(@"D:\IIS\EBM\國軍電子病歷系統整合案-線上報修紀錄表 - 線上報修紀錄表.csv");
            parser.SetDelimiters(",");
            foreach (string s in parser.ReadFields()) Console.WriteLine(s);

            //string line;
            //System.IO.StreamReader file = new System.IO.StreamReader(@"D:\IIS\EBM\國軍電子病歷系統整合案-線上報修紀錄表 - 線上報修紀錄表.csv");
       
            //while ((line = file.ReadLine()) != null)
            //{
               
            //    Console.WriteLine(line);
            //    //TodoEBMProjectOnline online = new TodoEBMProjectOnline()
            //    //{
            //    //    ApplyDateTime = Convert.ToDateTime(arr[1].Trim()),
            //    //    ApplyDepartment = arr[2].Trim(),
            //    //    ApplyName = arr[3].Trim(),
            //    //    Description = arr[4].Trim(),
            //    //    HandleDateTime = Convert.ToDateTime(arr[5].Trim()),
            //    //    HandleName = arr[6].Trim(),
            //    //    ResolveDateTime = Convert.ToDateTime(arr[7].Trim()),
            //    //    ResponseName = arr[8].Trim(),
            //    //    Memo = arr[9].Trim()
            //    //};
            //    //db.TodoEBMProjectOnline.Add(online);
            //    //db.SaveChanges();
            //}
            Console.ReadLine();
        }
        static void addWorkings()
        {
            EBMTodo db = new EBMTodo();
            Random rnd = new Random();
            var projects = db.TodoEBMProject.ToList();
            var lineUsers = db.Line_User.ToList();
            var id = db.AspNetUsers.First().Id;
            for (int i = 0; i < 1000; i++)
            {
                var pid = projects[rnd.Next(0, projects.Count)].PID;
                var uid = lineUsers[rnd.Next(0, lineUsers.Count)].UID;
                DateTime dt = DateTime.Now.AddDays(rnd.Next(-30, 30));
                var obj = new TodoEBMProjectWorking()
                {
                    CreateDateTime = dt,
                    Description = "Description" + rnd.Next(0, 9999),
                    RecordDateTime = dt,
                    workingType = i % 4 + 1,
                    WokingHour = rnd.Next(0, 10),
                    Target = "target" + rnd.Next(0, 9999),
                    PID = pid,
                    LineUID = uid,
                    Id = id
                };
                db.TodoEBMProjectWorking.Add(obj);
                db.SaveChanges();
                Console.WriteLine(JsonConvert.SerializeObject(obj));
            }
        }
    }
}