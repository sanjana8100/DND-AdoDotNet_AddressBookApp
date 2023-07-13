using System;

namespace AdoDotNet_AddressBookApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source = SANJANA; Database = AddressBookApplication; Integrated Security = true";
            AddressBook addressBook = new AddressBook(connectionString);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("******************MENU:******************");
                Console.WriteLine("=> To Add Contact: PRESS 1");
                Console.WriteLine("=> To Update an Existing Contact: PRESS 2");
                Console.WriteLine("=> To Delete a Contact: PRESS 3");
                Console.WriteLine("=> To Display a specific Contact: PRESS 4");
                Console.WriteLine("=> To Display all Contacts in the Address Book: PRESS 5");
                Console.WriteLine("=> To Add Contact Using Stored Procedure: PRESS 6");
                Console.WriteLine("=> To Add Contact Using Stored Procedure And Transaction: PRESS 7");
                Console.WriteLine("=> To EXIT: PRESS 8");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter the details to add a contact: ");

                            Console.Write("\nEnter name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter email: ");
                            string email = Console.ReadLine();

                            Console.Write("Enter phone number: ");
                            string phone = Console.ReadLine();

                            Console.Write("Enter state: ");
                            string state = Console.ReadLine();

                            Console.Write("Enter city: ");
                            string city = Console.ReadLine();

                            Console.Write("Enter zip: ");
                            string zip = Console.ReadLine();

                            Contact contact = new Contact(name, email, phone, state, city, zip);
                            addressBook.AddContact(contact);

                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter the ID of the Contact who's phone number you want to edit: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter the new phone number: ");
                            string newPhone = Console.ReadLine();

                            Contact contact = new Contact(newPhone);
                            addressBook.UpdateContact(id, contact);

                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter the ID of the Contact you want to Delete: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            //addressBook.DisplayContact(id);

                            Console.WriteLine("Are you sure you want to DELETE the Contact?");
                            Console.WriteLine("1. YES \t 2. NO");
                            int option = Convert.ToInt32(Console.ReadLine());
                            switch (option)
                            {
                                case 1:
                                    addressBook.DeleteContact(id);
                                    break;
                                case 2:
                                    Console.WriteLine("Contact is NOT deleted!!!");
                                    break;
                                default:
                                    Console.WriteLine("Select a valid option!!!");
                                    break;
                            }

                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter the ID of the Contact you want to Display: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Contact contact = addressBook.DisplayContact(id);

                            break;
                        }
                    case 8:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice!!! Please make a valid choice.");
                            break;
                        }
                }
            }
        }
    }
}