using MyTraining1121AngularDemo.EntityFrameworkCore;
using MyTraining1121AngularDemo.Phonebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.Migrations.Seed.Host;

public class InitialPeopleAndPhoneCreator
{
    private readonly MyTraining1121AngularDemoDbContext _context;

    public InitialPeopleAndPhoneCreator(MyTraining1121AngularDemoDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        var gurav = _context.Persons.FirstOrDefault(p => p.EmailAddress == "pooja.gurav@waiin.com");
        if (gurav == null)
        {
            _context.Persons.Add(
                new Person
                {
                    Name = "Pooja",
                    Surname = "Gurav",
                    EmailAddress = "pooja.gurav@waiin.com",
                    Phones = new List<Phone>
                                {
                                    new Phone {Type = PhoneType.Home, Number = "1112242"},
                                    new Phone {Type = PhoneType.Mobile, Number = "2223342"}
                                }
                });
        }

        var kulkarni = _context.Persons.FirstOrDefault(p => p.EmailAddress == "bhagyashree.kulkarni@waiin.com");
        if (kulkarni == null)
        {
            _context.Persons.Add(
                new Person
                {
                    Name = "Bhagyashree",
                    Surname = "Kulkarni",
                    EmailAddress = "bhagyashree.kulkarni@waiin.com",
                    Phones = new List<Phone>
                                {
                                    new Phone {Type = PhoneType.Home, Number = "1112242"},
                                    new Phone {Type = PhoneType.Mobile, Number = "2223342"}
                                }
                });
        }
      
    }
}
