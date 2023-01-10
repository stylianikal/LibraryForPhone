using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;




namespace LibraryForPhone
{
    public class PhoneBookLibrary
    {
        public class PhoneBook
        {
            private List<PhoneBookEntry> entries;

            public PhoneBook()
            {
                entries = new List<PhoneBookEntry>();
            }
            //method to add entry
            public void Add(PhoneBookEntry entry)
            {
                entries.Add(entry);
            }
            //method to delete entry
            public void Delete(PhoneBookEntry entry)
            {
                entries.Remove(entry);

            }
            //method to edit entry
            public void Edit(PhoneBookEntry entry, string firstName, string lastName, string type, string number)
            {
                entry.FirstName = firstName;
                entry.LastName = lastName;
                entry.Type = type;
                entry.Number = number;
            }
            //method to get entries to order by last and then first name alphametically
            public IEnumerable<PhoneBookEntry> GetEntriesByName()
            {
                return entries.OrderBy(entry => entry.LastName).ThenBy(entry => entry.FirstName);
            }
            //save data to file using BinaryWriter
            public void Save(string fileName)
            {
                using (var stream = new FileStream(fileName, FileMode.Create))
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(entries.Count);
                    foreach (var entry in entries)
                    {
                        writer.Write(entry.FirstName);
                        writer.Write(entry.LastName);
                        writer.Write(entry.Type);
                        writer.Write(entry.Number);
                    }
                }
            }
            //load data from file with BinaryReader
            public void Load(string fileName)
            {
                entries.Clear();
                using (var stream = new FileStream(fileName, FileMode.Open))
                using (var reader = new BinaryReader(stream))
                {
                    var count = reader.ReadInt32();
                    for (var i = 0; i < count; i++)
                    {
                        var firstName = reader.ReadString();
                        var lastName = reader.ReadString();
                        var type = reader.ReadString();
                        var number = reader.ReadString();
                        entries.Add(new PhoneBookEntry(firstName, lastName, type, number));
                    }
                }
            }

        }

    }

    
public class PhoneBookEntry
{
     //constractror
    public PhoneBookEntry(string firstName, string lastName, string type, string number)
    {
        FirstName = firstName;
        LastName = lastName;
        Type = type;
        Number = number;
    }
    //getters and setters for fields
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Type { get; set; }
    public string Number { get; set; }
}
}
    

