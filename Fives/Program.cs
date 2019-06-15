using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Fives
{
    class Program
    {
        static char sep = Path.DirectorySeparatorChar;

        static void Main(string[] args)
        {
            // loading files

            DirectoryInfo dir = new DirectoryInfo($"..{sep}..{sep}kingdomsFives");
            FileInfo[] files = dir.GetFiles("kingdom_*.txt");

            var file = files.Skip(2).First();
            var name = file.Name
                    .Remove(file.Name.Length - 4)
                    .Substring(20).TrimStart(new char[] { '7', '_' });

            Console.WriteLine("blbosti");

            using (var reader = new StreamReader(file.OpenRead()))
            {
                var agenda = new AgendaDb();
                agenda.Cards = name;

                reader.ReadLine();
                agenda.Provinces = int.Parse(reader.ReadLine());
                agenda.Duchies = int.Parse(reader.ReadLine());
                agenda.Estates = int.Parse(reader.ReadLine());
                agenda.BuyMenu = new List<BuyMenuItem>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split();
                    Enum.TryParse(line[0], out CardType type);
                    agenda.BuyMenu.Add(new BuyMenuItem { CardId = (int)type, Number = int.Parse(line[1]) });
                }

                Console.WriteLine("blbosti 2");

                // adding to database
                using (var db = new DominionDbContext())
                {
                    db.Database.Migrate();
                    try
                    {
                        db.Fives.Add(agenda);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            // searching in database
            using (var db = new DominionDbContext())
            {
                var agenda = db.Fives.Include(a => a.BuyMenu).Where(a => a.Cards == name).Single();

                Console.WriteLine(agenda.Cards);
                Console.WriteLine(agenda.Provinces);
                Console.WriteLine(agenda.Duchies);
                Console.WriteLine(agenda.Estates);
                agenda.BuyMenu.ForEach(item => Console.WriteLine($"{(CardType)item.CardId} {item.Number}"));
            }

            Console.ReadLine();

            //const int set = 25;
            //const int subset = 5;

            //long c = 2 << set - 1;
            //int allFives = 0;

            //using (var writer = new StreamWriter("fives.txt"))
            //{
            //    for (long i = 1; i < c + 1; i++)
            //    {
            //        List<int> numbers = new List<int>();
            //        long x = i;
            //        int a = 8;
            //        while (x > 0)
            //        {
            //            if (x % 2 == 1)
            //                numbers.Add(a);
            //            x /= 2;
            //            a++;
            //        }

            //        if (numbers.Count == subset)
            //        {
            //            //    Console.WriteLine(i);
            //            writer.WriteLine(numbers.Aggregate("", (e, f) => e + " " + f.ToString()));
            //            allFives++;
            //        }
            //    }
            //}
            //Console.WriteLine(allFives);
            //Console.ReadLine();
        }
    }
}
