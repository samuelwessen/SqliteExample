using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string firstname, string lastname, DateTime dateofbirth)
        {
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = dateofbirth;
        }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public string Summary => $"{DisplayName}, {DateOfBirth:d}";       
    }

    public class PersonViewModel
    {
        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>();

        public PersonViewModel()
        {
            People.Add(new Person("Samuel", "Wessén", new DateTime(1986, 6, 6)));
            People.Add(new Person("Mikael", "Wessén", new DateTime(1986, 6, 6)));
            People.Add(new Person("Daniel", "Wessén", new DateTime(1986, 6, 6)));
        }

        public void RemoveItem(Person person)
        {
            People.Remove(person);
        }
    }
}
