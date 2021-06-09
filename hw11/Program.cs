using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace hw11
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                try
                {
                    Console.WriteLine("Subjects: ");
                    context.Subjects.ToList().ForEach(s =>
                    {
                        Console.WriteLine($"[{s.Id}]\t{s.Title})");
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
                Console.WriteLine("Teachers: ");
                try
                {
                    Console.WriteLine("Teachers: ");
                    context.Teachers.ToList().ForEach(s =>
                    {
                        Console.WriteLine($"[{s.Id}]\t{s.Name})");
                    });
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    Console.ReadLine();
                }
            }
            Console.ReadLine();
        }
    }
}
