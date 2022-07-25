using Abp.Application.Services.Dto;

using System;
using System.Collections.ObjectModel;

public class CustomerListDto : FullAuditedEntityDto
{
    public string CustomerName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Address { get; set; }
    public Collection<UserInCustomerListDto> CustUsers { get; set; }
}
