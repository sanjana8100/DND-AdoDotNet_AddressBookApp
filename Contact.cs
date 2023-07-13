using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoDotNet_AddressBookApplication
{
    internal class Contact
    {
        public int ContactId { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string State { set; get; }
        public string City { set; get; }
        public string Zipcode { set; get; }

        public Contact() { }

        public Contact(string phone)
        {
            Phone = phone;
        }

        public Contact(string name, string email, string phone, string state, string city, string zipcode)
        {
            Name = name;
            Email = email;
            Phone = phone;
            State = state;
            City = city;
            Zipcode = zipcode;
        }
    }
}
