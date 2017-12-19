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
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            List<TodoEBMProjectOnline> list = new List<TodoEBMProjectOnline>();
            while (!parser.EndOfData)
            {
                //Processing row
                string[] fields = parser.ReadFields();
                foreach (string field in fields)
                {
                    //TODO: Process field

                    Console.WriteLine(field);
                }
                try
                {
                    TodoEBMProjectOnline online = new TodoEBMProjectOnline()
                    {
                        CreateDateTime = DateTime.Now,
                        ApplyDateTime = Convert.ToDateTime(fields[1].Trim()),
                        ApplyDepartment = fields[2].Trim(),
                        ApplyName = fields[3].Trim(),
                        Description = fields[4].Trim(),
                        HandleDateTime = string.IsNullOrEmpty(fields[5].Trim()) ? null : (DateTime?)Convert.ToDateTime(fields[5].Trim()),
                        HandleName = fields[6].Trim(),
                        ResolveDateTime = string.IsNullOrEmpty(fields[7].Trim()) ? null : (DateTime?)Convert.ToDateTime(fields[7].Trim()),
                        ResponseName = fields[8].Trim(),
                        Memo = fields[9].Trim(),
                        CompleteRate = 0,
                        title = null,
                        OnlineCategories = 0
                    };

                    list.Add(online);
                    Console.WriteLine(JsonConvert.SerializeObject(online));
                }
                catch (Exception e)
                {

                }


            }
            parser.Close();
            db.TodoEBMProjectOnline.AddRange(list);
            db.SaveChanges();
            //foreach (string s in parser.ReadFields())
            //{
            //    Console.WriteLine(s);
            //}


            //string line;
            //System.IO.StreamReader file = new System.IO.StreamReader(@"D:\IIS\EBM\國軍電子病歷系統整合案-線上報修紀錄表 - 線上報修紀錄表.csv");

            //while ((line = file.ReadLine()) != null)
            //{


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