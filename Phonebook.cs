using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Phonebook
    {
        /* Write a program that receives some info from the console about people and their phone numbers. 
         * You are free to choose the manner in which the data is entered; each entry should have just one
         * name and one number (both of them strings). After filling this simple phonebook, upon receiving 
         * the command "search", your program should be able to perform a search of a contact by name and 
         * print her details in format "{name} -> {number}". In case the contact isn't found, print "Contact
         * {name} does not exist."
         */

        private Dictionary<string,string> phonebook = new Dictionary<string,String>();
        public Phonebook()
        {

        }

        public void AddContact(string name, string phone)
        {
            if (this.phonebook.ContainsKey(name))
            {
                Console.WriteLine("Contact with that name already exists!");
                return;
            }
            this.phonebook.Add(name, phone);             
        }

        public void EditContact(string name, string phone)
        {
            if (!this.phonebook.ContainsKey(name))
            {
                Console.WriteLine("There is no contact with that name!");
                return;
            }
            this.phonebook[name] = phone;
        }

        public void SearchByName(string name)
        {
            if (this.phonebook.ContainsKey(name))
            {
                Console.WriteLine("The phone number of {0} is {1}", name, this.phonebook[name]);
            }
            else
            {
                Console.WriteLine("There is no contact with that name!");
            }
        }
        public void SearchByPhone(string phone)
        {
            if (this.phonebook.ContainsValue(phone))
            {
                foreach (var item in this.phonebook)
                {
                    if(item.Value == phone) 
                    {
                        Console.WriteLine("The person who uses the phone with this phone number is {0}!", item.Key);
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no such phone number!");
            }
        }
    }
}
