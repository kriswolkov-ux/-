using System;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dataSet = new DataSet("Univer");

            DataTable groups = new DataTable("Groups");
            groups.Columns.Add("Id", typeof(int));
            groups.Columns.Add("Name", typeof(string));
            groups.PrimaryKey = new[] { groups.Columns["Id"] };

            DataTable students = new DataTable("Students");
            students.Columns.Add("Id", typeof(int));
            students.Columns.Add("Name", typeof(string));
            students.Columns.Add("Age", typeof(int));
            students.Columns.Add("GroupId", typeof(int));
            students.PrimaryKey = new[] { students.Columns["Id"] };

            dataSet.Tables.Add(groups);
            dataSet.Tables.Add(students);

            DataRelation relation = dataSet.Relations.Add(
                "GroupStudents",
                groups.Columns["Id"],
                students.Columns["GroupId"]
            );

            groups.Rows.Add(1, "ИВТ-21");
            groups.Rows.Add(2, "ИВТ-22");
            groups.Rows.Add(3, "ПИ-21");

            students.Rows.Add(1, "Эпштейн Витя", 20, 1);
            students.Rows.Add(2, "Матьювели Андрей", 19, 1);
            students.Rows.Add(3, "Сидр Матвей", 21, 2);
            students.Rows.Add(4, "Сокол Eyes", 20, 2);
            students.Rows.Add(5, "Андерграунд Сергей", 22, 3);
            students.Rows.Add(6, "Ямакаси Максим", 19, 3);
            students.Rows.Add(7, "Книга Нига", 18, 1);
            students.Rows.Add(8, "Петренько Петронько", 20, 2);
            students.Rows.Add(9, "Шаман Рахман", 21, 3);
            students.Rows.Add(10, "Артемий Лебедев", 19, 1);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" Доступные группы ");
                foreach (DataRow group in groups.Rows)
                {
                    Console.WriteLine($"[{group["Id"]}] - {group["Name"]}");
                }
                Console.WriteLine("[0] - Выход");
                Console.WriteLine();
                Console.Write("Выберите номер группы: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int selectedId))
                {
                    Console.WriteLine("Ошибка: введите число");
                    Console.ReadKey();
                    continue;
                }

                if (selectedId == 0)
                {
                    break;
                }

                DataRow[] foundGroups = groups.Select($"Id = {selectedId}");
                if (foundGroups.Length == 0)
                {
                    Console.WriteLine("Группа с таким номером не найдена");
                    Console.ReadKey();
                    continue;
                }

                DataRow selectedGroup = foundGroups[0];
                Console.WriteLine($"\n Студенты группы: {selectedGroup["Name"]} ");

                DataRow[] groupStudents = selectedGroup.GetChildRows(relation);

                if (groupStudents.Length == 0)
                {
                    Console.WriteLine("В этой группе нет студентов.");
                }
                else
                {
                    foreach (DataRow student in groupStudents)
                    {
                        Console.WriteLine($"ID: {student["Id"],-4} | Имя: {student["Name"],-25} | Возраст: {student["Age"]}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
        }
    }
}
