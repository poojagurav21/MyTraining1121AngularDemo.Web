using System;
using System.ComponentModel.DataAnnotations;

namespace MyTraining1121AngularDemo.CustomerMgt
{
    public class CreateCustomerInput
    {
        [Required]
        [MaxLength(CustomerConsts.MaxNameLength)]
        public string CustomerName { get; set; }

        [EmailAddress]
        [MaxLength(CustomerConsts.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string Address { get; set; }
        public long UserRefId { get; set; }
    }
}
