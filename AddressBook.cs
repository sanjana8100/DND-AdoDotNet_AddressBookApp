using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                insertCommand2.Parameters.AddWithValue("@ContactId", contact.Id);
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
                sqlCommand.Parameters.AddWithValue("@Id", contact.Id);

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

    }
}
