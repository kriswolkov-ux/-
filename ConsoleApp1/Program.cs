using System;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable students = new DataTable("Students");

            students.Columns.Add("Id", typeof(int));
            students.Columns.Add("Name", typeof(string));
            students.Columns.Add("Age", typeof(int));
            students.Columns.Add("GroupId", typeof(int));
            students.Rows.Add(1, "Эпштейн Витя", 401, 101);
            students.Rows.Add(2, "Матьювели Андрей", 200, 166);
            students.Rows.Add(3, "Сидр Матвей", 176, 112);
            students.Rows.Add(4, "Соколов Артем", 188, 131);
            students.Rows.Add(5, "Андерграунд Сергей", 242, 174);
            students.Rows.Add(6, "Ямакаси Максим", 191, 142);

            Console.WriteLine(" Исходный список студентов ");
            PrintTable(students);

            int maxAge = 0;
            string old = "";
            int totalAge = 0;

            foreach (DataRow row in students.Rows)
            {
                int currentAge = (int)row["Age"];
                totalAge += currentAge;

                if (currentAge > maxAge)
                {
                    maxAge = currentAge;
                    old = (string)row["Name"];
                }
            }

            double averageAge = (double)totalAge / students.Rows.Count;

            Console.WriteLine($"\n[Аналитика]");
            Console.WriteLine($"Самый старший студент: {old} (Возраст: {maxAge})");
            Console.WriteLine($"Средний возраст группы: {averageAge:F2} года");
        }
        static void PrintTable(DataTable table)
        {
            Console.WriteLine($"{"Id",-4} | {"Name",-20} | {"Age",-4} | {"GroupId",-7}");
            Console.WriteLine(new string('-', 45));

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["Id"],-4} | {row["Name"],-20} | {row["Age"],-4} | {row["GroupId"],-7}");
            }
        }
    }
}
