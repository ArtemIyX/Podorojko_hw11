using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hw11
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                //Удалить все перед работой
                context.ClearAll();
                //Добавил стандартные значения
                context.Teachers.AddRange(context.DefaultTeachers());
                context.Subjects.Include(s => s.Teachers);
                context.Subjects.AddRange(context.DefaultSubjects());

                context.SaveChanges();
                //Дообавить всем предметам учителей
                List<Teacher> teachers = context.Teachers.ToList();
                List<Subject> subjects = context.Subjects.ToList();
                subjects.ForEach(s =>
                {
                    Helper.RandomTeachers(teachers, 5).ForEach(t =>
                    {
                        if (s.Teachers is null)
                            s.Teachers = new HashSet<Teacher>();
                        s.Teachers.Add(t);
                    });
                }); 
                context.SaveChanges();
                subjects.ForEach(s => {
                    Console.WriteLine($"{s} {s.Teachers.ToList().ToMyString()}");
                });
                Subject s = subjects.ElementAt(new Random().Next(subjects.Count - 1));
                s.Teachers.Clear();
                s.Title = "Edited element";
                context.SaveChanges();
                Console.WriteLine("===================================================================");
                subjects.ForEach(s => {
                    Console.WriteLine($"{s} {s.Teachers.ToList().ToMyString()}");
                });
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
    public static class Helper
    {
        public static List<Teacher> RandomTeachers(List<Teacher> teachers, int max)
        {
            max = Math.Min(teachers.Count, max);
            HashSet<Teacher> res = new HashSet<Teacher>();
            for (int i = 0; i < max; ++i)
            {
                res.Add(teachers.ElementAt(new Random().Next(teachers.Count)));
            }
            return res.ToList();
        }
        public static List<Teacher> DefaultTeachers(this ApplicationContext context)
        {
            return Enumerable.Range(1, 25).Select(i => new Teacher() { Name = $"Teacher{i}" }).ToList();
        }
        public static List<Subject> DefaultSubjects(this ApplicationContext context)
        {
            return Enumerable.Range(1, 10).Select(i => new Subject { Title = $"Subject{i}" }).ToList();
        }
        public static void ClearAll(this ApplicationContext context)
        {
            foreach (var item in context.Teachers)
                context.Teachers.Remove(item);
            foreach (var item in context.Subjects)
                context.Subjects.Remove(item);
            context.SaveChanges();
        }
        public static void DisplaySubjects(this ApplicationContext context)
        {

        }
        public static string ToMyString<T>(this List<T> l)
        {
            StringBuilder sb = new StringBuilder("{ ");
            for (int i = 0; i < l.Count; ++i)
            {
                sb.Append(l[i].ToString());
                if (i < l.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(" }");
            return sb.ToString();
        }
    }

}
