using System.ComponentModel.Design;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
namespace hmwrk
{
    internal class Program
    {
        static StringBuilder sb = new StringBuilder();
        static void Main(string[] args)
        {
        here:
            Console.Write("Expence name: ");
            string exp = Console.ReadLine();
            Console.Write("Amount: ");
            bool check = double.TryParse(Console.ReadLine(), out double amount);
            if (!check)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter amount in correct format.");
                Console.ResetColor();
                Thread.Sleep(1500);
                Console.Clear();
                goto here;
            }
            Console.Write("Date {2024,12,31}: ");
            bool timeCheck = DateTime.TryParse(Console.ReadLine(), out var time);
            if (!timeCheck)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter time in correct format.");
                Console.ResetColor();
                Thread.Sleep(1500);
                Console.Clear();
                goto here;
            }

            AddToFile(exp, amount, time);

            string path = "notes.text";

            Console.WriteLine("\nHit enter to add expence ... \n");
            //ConsoleKeyInfo key = Console.ReadKey();
            var stop = Console.ReadLine();


            if (string.IsNullOrEmpty(stop))
            {
                Console.Clear();
                goto here;
            }
            else if (Regex.IsMatch(stop, "stop", RegexOptions.IgnoreCase))
            {
                Console.Clear();

                if (!File.Exists(path))
                {
                    File.Create(path).Close();

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(sb.ToString());
                        Console.WriteLine("Saved");
                    }
                }
                else
                {
                    var text = File.ReadAllText(path);

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(sb.ToString());
                        Console.WriteLine("Saved");
                    }
                }
            }
        }

        static void AddToFile(string name, double amount, DateTime time)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var t = time.ToString("d", CultureInfo.InvariantCulture);
                //var expence = new StringBuilder();
                int length = $"| {name,+25} | {amount,+13} | {t,+13} |".Length - 2;

                string line = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    line += "-";
                }

                if (sb.Length > 0)
                {
                    //sb.AppendFormat($" {line}\n");
                    sb.AppendFormat($"| {name,+25} | {amount,+13} | {t,+13} |\n");
                    sb.AppendFormat($" {line}\n");
                }
                else
                {
                    sb.AppendFormat($" {line}");
                    sb.AppendFormat($"\n| {"Expense",+25} | {"Amount",+13} | {"Date",+13} |\n");
                    sb.AppendFormat($" {line} \n");
                    sb.AppendFormat($"| {name,+25} | {amount,+13} | {t,+13} |\n");
                    sb.AppendFormat($" {line}\n");
                }

                Console.WriteLine(sb);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter full info.");
                Console.ResetColor();
                Thread.Sleep(1500);
                Console.Clear();
            }
        }
    }
}
