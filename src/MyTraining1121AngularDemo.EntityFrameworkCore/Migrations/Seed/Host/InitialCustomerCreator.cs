using MyTraining1121AngularDemo.CustomerMgt;
using MyTraining1121AngularDemo.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.Migrations.Seed.Host;

public class InitialCustomerCreator
{
    private readonly MyTraining1121AngularDemoDbContext _context;

    public InitialCustomerCreator(MyTraining1121AngularDemoDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        var douglas = _context.Customers.FirstOrDefault(p => p.EmailAddress == "douglas.adams@fortytwo.com");
        if (douglas == null)
        {
            _context.Customers.Add(
                new Customer
                {
                    CustomerName = "Douglas Jhon",
                    EmailAddress = "douglas.adams@fortytwo.com",
                    RegistrationDate = DateTime.Now,
                    Address="Sawedi,Ahmednagar",
                    CustomerUsers = new List<CustomerUsers>
                    {
                        new CustomerUsers{UserRefId=2,TotalBillingAmount=4554.45M},

                    }

                });
        }

        var asimov = _context.Customers.FirstOrDefault(p => p.EmailAddress == "isaac.asimov@foundation.org");
        if (asimov == null)
        {
            _context.Customers.Add(
                new Customer
                {
                    CustomerName = "Asimov Scheche",
                    EmailAddress = "isaac.asimov@foundation.org",
                    RegistrationDate = DateTime.Now,
                    Address = "Sawedi,Ahmednagar",
                    CustomerUsers = new List<CustomerUsers>
                    {
                        new CustomerUsers{UserRefId=2,TotalBillingAmount=4554.45M},

                    }
                });
        }
    }
}
