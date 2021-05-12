using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    class Person
    {
        /* CLASS: Person
         * PURPOSE: a person entry in the address list
         */
        string name, address, telephone, email;
        public string GetName()
        {
            return name;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public string GetAddress()
        {
            return address;
        }
        public void SetAddress(string address)
        {
            this.address = address;
        }

        public string GetTelephone()
        {
            return telephone;
        }
        public void SetTelephone(string telephone)
        {
            this.telephone = telephone;
        }

        public string GetEmail()
        {
            return email;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public Person(string N, string A, string T, string E)
        {
            name = N; address = A; telephone = T; email = E;
        }
        public void Print()
        {
            Console.WriteLine("{0 },{1},{2},{3}",name,address,telephone,email );
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AddressList();
        }

        private static void AddressList()
        {
            /* METHOD: AddressList (static)
             * PURPOSE: Construct a addes list with menu. 
             */

            string command;
            List<Person> Dict = new List<Person>();
            // PROGRAM INITIALIZATION:
            AddressListInit(Dict);
            DisplayMenu();
            // COMMAND PROMPT:
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    NewEntryToList(Dict);
                }
                else if (command == "ta bort")
                {
                    RemoveEntry(Dict);
                }
                else if (command == "visa")
                {
                    DisplayEntries(Dict);
                }
                else if (command == "andra")
                {
                    EditEntry(Dict);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        private static void DisplayMenu()
        {
            /* METHOD: DisplayMenu
            * PURPOSE: prints a  menu to Console.
            */
            // COMMAND PROMPT:
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'ny' för att lägga till ett nytt kontakt!");
            Console.WriteLine("Skriv 'ta bort' för radera kontakt!");
            Console.WriteLine("Skriv 'visa' för att visa alla kontakter!");
            Console.WriteLine("Skriv 'andra' för att ändra i en kontakt!");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
        }

        private static void EditEntry(List<Person> Dict)
        {
            /* METHOD: EditEntry (static)
             * PURPOSE: Edits entries in list of persons.
             * PARAMETERS: list of type Person.
             */

            string EntryNameToBeUpdated;
            int found;//index of found name in list. found = -1 if not found in list
            string fieldToBeUpdated;
            string newValueOfField;

            Console.Write("Vem vill du ändra (ange namn): ");
            EntryNameToBeUpdated = Console.ReadLine();
            found = -1;
            for (int atIndex = 0; atIndex < Dict.Count(); atIndex++)
            {
                if (Dict[atIndex].GetName() == EntryNameToBeUpdated) found = atIndex;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", EntryNameToBeUpdated);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                fieldToBeUpdated = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", fieldToBeUpdated, EntryNameToBeUpdated);
                newValueOfField = Console.ReadLine();
                switch (fieldToBeUpdated)
                {
                    case "namn": Dict[found].SetName(newValueOfField); break;
                    case "adress": Dict[found].SetAddress(newValueOfField); break;
                    case "telefon": Dict[found].SetTelephone(newValueOfField); break;
                    case "email": Dict[found].SetEmail(newValueOfField); break;
                    default: break;
                }
            }
        }

        private static void DisplayEntries(List<Person> Dict)
        {
            /* METHOD: DisplayEntries.(static)
             * PURPOSE: Prints all entris in list to Console. 
             * PARAMETERS: list of type Person.
             */

            for (int i = 0; i < Dict.Count(); i++)
            {
                Person P = Dict[i];
                P.Print();
            }
        }

        private static void RemoveEntry(List<Person> Dict)
        {
            /* METHOD: RemoveEntry. (static)
             * PURPOSE: Removes an entry in the address list .
             * PARAMETERS: list of type Person.
             */

            string strToRemove;
            int found; //index of found string

            Console.Write("Vem vill du ta bort (ange namn): ");
            strToRemove = Console.ReadLine();
            found = -1;
            for (int atIndex = 0; atIndex < Dict.Count(); atIndex++)
            {
                if (Dict[atIndex].GetName() == strToRemove) 
                    found = atIndex;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", strToRemove);
            }
            else
            {
                Dict.RemoveAt(found);
                
            }
        }

        private static void NewEntryToList(List<Person> Dict)
        {
            /* METHOD: NewEntryToList (static)
             * PURPOSE:Adds anew entry to the address list .
             * PARAMETERS: list of type Person.
             */

            string name;
            string address;
            string telephone;
            string email;

            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            telephone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
            Dict.Add(new Person(name, address, telephone, email));
            
        }

        private static void AddressListInit(List<Person> Dict)
        {
            /* METHOD: AddressListInit (static)
             * PURPOSE: Initialize list of type Person.
             * PARAMETERS: list of type Person.
             */

            string line;
            string[] word;
            Person P;

            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    line = fileStream.ReadLine();
                    // Console.WriteLine(line);
                    word = line.Split('#');
                    P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
    }
}
