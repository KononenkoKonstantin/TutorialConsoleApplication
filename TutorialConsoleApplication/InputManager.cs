using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialConsoleApplication
{
    public class InputManager
    {        
        public string TableName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RangeValue { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public InputManager()
        {
                
        }

        public void InputTableName()
        {
            Console.Write("Введите название таблицы: ");
            TableName = Console.ReadLine();            
        }

        public void InputLastName()
        {
            Console.Write("Введите фамилию: ");
            LastName = Console.ReadLine();
        }

        public void InputFirstName()
        {
            Console.Write("Введите имя: ");
            FirstName = Console.ReadLine();
        }

        public void InputPhone()
        {
            Console.Write("Введите номер телефона: ");
            Phone = Console.ReadLine();
        }

        public void InputEmail()
        {
            Console.Write("Введите Email: ");
            Email = Console.ReadLine();
        }

        public void SuccessTableCreating()
        {
            Console.WriteLine("Таблица успешно создана");
        }

        public void Success()
        {
            Console.WriteLine("Данные успешно добавлены");
        }

        public void SuccessDeleted()
        {
            Console.WriteLine("Данные успешно удалены");
        }

        public void TableNotFound()
        {
            Console.WriteLine("Таблица с таким названием не найдена");
        }

        public bool AllowContinue()
        {
            string choice = "y";
            Console.WriteLine("Желаете добавить еще (y / n) ?");
            choice = Console.ReadLine();                      
           do
           {
           if (choice != "y" || choice != "n")
                break;
                Console.WriteLine("Ваш выбор не определен");
           } while (true);               
            
            if (choice == "y")
                return true;
            else
                return false;

        }                   
          

        public void InputRangeValue()
        {
            Console.WriteLine("Введите верхнюю границу диапазона: ");
            RangeValue = Console.ReadLine();
        }
    }
}
