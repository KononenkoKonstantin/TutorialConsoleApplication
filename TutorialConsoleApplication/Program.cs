using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Build.Execution;

namespace TutorialConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {               
            DataManager manager = new DataManager();
            InputManager im = new InputManager();
            CustomerEntity customer = null;
            Menu m = new Menu();

            do
            {
                m.showMenu();
                switch (m.Choice)
                {
                    case 1:
                        Console.Clear();
                        im.InputTableName();
                        manager.CreateTable(im.TableName);
                        im.SuccessTableCreating();
                        break;
                    case 2:
                        Console.Clear();
                        im.InputTableName();                        
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();
                            im.InputFirstName();
                            customer = new CustomerEntity(im.LastName, im.FirstName);
                            im.InputPhone();
                            customer.PhoneNumber = im.Phone;
                            im.InputEmail();
                            customer.Email = im.Email;
                            manager.AddEntity(customer);
                            im.Success();
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 3:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            IList<CustomerEntity> list = new List<CustomerEntity>();
                            im.InputLastName();
                            do
                            {                                
                                im.InputFirstName();
                                customer = new CustomerEntity(im.LastName, im.FirstName);
                                im.InputPhone();
                                customer.PhoneNumber = im.Phone;
                                im.InputEmail();
                                customer.Email = im.Email;
                                list.Add(customer);                                
                            } while (im.AllowContinue());
                            manager.AddEntities(list);
                            im.Success();
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 4:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();                           
                            manager.GetPartition(im.LastName);                            
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 5:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();
                            im.InputRangeValue();
                            manager.GetRangePartition(im.LastName, im.RangeValue);                            
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 6:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();
                            im.InputFirstName();
                            Console.Write("Номер телефона: ");
                            manager.GetEntityPhone(im.LastName, im.FirstName);                            
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 7:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();
                            im.InputFirstName();
                            im.InputPhone();
                            manager.ChangeEntityPhone(im.LastName, im.FirstName, im.Phone);
                            im.Success();
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 8:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();
                            im.InputFirstName();                            
                            manager.GetEntitiesProperty();                            
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 9:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {
                            im.InputLastName();
                            im.InputRangeValue();
                            manager.DeleteEntity(im.LastName, im.RangeValue);
                            im.SuccessDeleted();
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 10:
                        Console.Clear();
                        im.InputTableName();
                        if (manager.GetTableReferences(im.TableName))
                        {                            
                            manager.DeleteTable();
                            im.SuccessDeleted();
                        }
                        else
                        {
                            im.TableNotFound();
                        }
                        break;
                    case 11:
                        Console.Clear();                        
                        manager.GetDataAsync().Wait();
                        break;
                    case 12:
                        m.Finish();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Вы ввели некорректное значение");
                        break;
                }
            } while (m.allowContinue());

        }
    }
    
}
