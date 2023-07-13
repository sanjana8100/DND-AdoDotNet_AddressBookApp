using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdoDotNet_AddressBookApplication
{
    internal class AddressBook
    {
        SqlConnection sqlConnection;

        public AddressBook(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public bool AddContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();

                string insertQuery1 = $"Insert into Contact Values (@Name, @Phone, @State, @City, @Zipcode)";
                SqlCommand insertCommand1 = new SqlCommand(insertQuery1, sqlConnection);

                insertCommand1.Parameters.AddWithValue("@Name", contact.Name);
                insertCommand1.Parameters.AddWithValue("@Phone", contact.Phone);
                insertCommand1.Parameters.AddWithValue("@State", contact.State);
                insertCommand1.Parameters.AddWithValue("@City", contact.City);
                insertCommand1.Parameters.AddWithValue("@Zipcode", contact.Zipcode);

                int result1 = insertCommand1.ExecuteNonQuery();

                string insertQuery2 = $"Insert into ContactEmail Values (@ContactId, @Email)";
                SqlCommand insertCommand2 = new SqlCommand(insertQuery2, sqlConnection);

                insertCommand2.Parameters.AddWithValue("@ContactId", contact.ContactId);
                insertCommand2.Parameters.AddWithValue("@Email", contact.Email);

                int result2 = insertCommand2.ExecuteNonQuery();

                if (result1 > 0 && result2 > 0)
                {
                    Console.WriteLine($"{result1} number of rows affected in Contact Table.");
                    Console.WriteLine($"{result2} number of rows affected in ContactEmail Table.");
                }
                else
                {
                    Console.WriteLine("Error!!! Invalid Query.");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateContact(int id, Contact contact)
        {
            try
            {
                sqlConnection.Open();

                string query = $"UPDATE Contacts SET Phone = @Phone WHERE Id= @Id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Phone", contact.Phone);
                sqlCommand.Parameters.AddWithValue("@Id", contact.ContactId);

                int result = sqlCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine($"{result} number of rows affected");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteContact(int id)
        {
            try
            {
                sqlConnection.Open();

                string query1 = $"Delete FROM ContactEmail WHERE ContactId = {id}";
                string query2 = $"Delete FROM Contact WHERE ContactId = {id}";

                SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection);
                SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);

                sqlCommand1.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Contact DisplayContact(int id)
        {
            try
            {
                sqlConnection.Open();

                string query = $"SELECT * From Contact JOIN ContactEmail ON Contact.ContactId = ContactEmail.ContactId where Contact.ContactId = {id}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                List<Contact> list = new List<Contact>();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = new Contact()
                    {
                        ContactId = (int)reader["ContactId"],
                        Name = (string)reader["Name"],
                        Phone = (string)reader["Phone"],
                        Email = (string)reader["Email"],
                        City = (string)reader["City"],
                        State = (string)reader["State"],
                        Zipcode = (string)reader["Zipcode"]
                    };
                    list.Add(contact);
                }
                foreach (Contact contact in list)
                {
                    if (contact.ContactId == id)
                    {
                        Console.WriteLine($"ContactId: {contact.ContactId}\t Name:- {contact.Name}\t Email:- {contact.Email}\t Phone:- {contact.Phone} \tCity:- {contact.City} \tState:- {contact.State} \tZIPCode:- {contact.Zipcode}");
                        return contact;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Contact> DisplayAllContacts()
        {
            sqlConnection.Open();

            List<Contact> list = new List<Contact>();

            string query = $"SELECT * From Contact JOIN ContactEmail ON Contact.ContactId = ContactEmail.ContactId";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Contact contact = new Contact()
                {
                    ContactId = (int)reader["ContactId"],
                    Name = (string)reader["Name"],
                    Phone = (string)reader["Phone"],
                    Email = (string)reader["Email"],
                    City = (string)reader["City"],
                    State = (string)reader["State"],
                    Zipcode = (string)reader["Zipcode"]
                };
                list.Add(contact);

            }

            foreach (Contact contact in list)
            {
                Console.WriteLine($"ContactId: {contact.ContactId}\t Name:- {contact.Name}\t Email:- {contact.Email}\t Phone:- {contact.Phone} \tCity:- {contact.City} \tState:- {contact.State} \tZIPCode:- {contact.Zipcode}");
            }
            sqlConnection.Close();
            return list;
        }
    }
}
