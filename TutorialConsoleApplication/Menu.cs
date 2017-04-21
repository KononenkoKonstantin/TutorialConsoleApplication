using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialConsoleApplication
{
    public class Menu
    {
        int choice;
        string ans;
        public int Choice { get { return choice; } }

        public void showMenu()
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("1 - Создать таблицу                   ");
            Console.WriteLine("2 - Добавить сущность                 ");
            Console.WriteLine("3 - Добавить несколько сущностей      ");
            Console.WriteLine("4 - Посмотреть раздел                 ");
            Console.WriteLine("5 - Посмотреть диапазон раздела       ");
            Console.WriteLine("6 - Посмотреть телефон                ");
            Console.WriteLine("7 - Изменить номер телефона           ");
            Console.WriteLine("8 - Показать свойства сущности        ");
            Console.WriteLine("9 - Удалить сущность                  ");
            Console.WriteLine("10- Удалить таблицу                   ");
            Console.WriteLine("11- Получить данные асинхронно        ");
            Console.WriteLine("12- Выход                            ");
            Console.WriteLine("======================================");
            Console.Write("Ваш выбор: ");
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}", ex.Message);
                Console.ResetColor();
            }
        }

        public void Finish()
        {
            Console.Clear();
            Console.WriteLine("=============================================");
            Console.WriteLine("          Работа программы завершена         ");
            Console.WriteLine("=============================================");
            Console.WriteLine("\n");
        }

        public bool allowContinue()
        {
            Console.Write("Продолжить? (y/n): ");
            try
            {
                ans = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}", ex.Message);
                Console.ResetColor();
            }
            return (ans == "y");
        }
    }
}

