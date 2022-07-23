using System.ComponentModel.DataAnnotations;

namespace MyTraining1121AngularDemo.PhoneBook
{
    public class GetPersonForEditOutput
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

       
        public string Surname { get; set; }

       
        public string EmailAddress { get; set; }
    }
}