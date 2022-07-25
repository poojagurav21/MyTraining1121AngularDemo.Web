using Abp.Application.Services.Dto;
using MyTraining1121AngularDemo.Authorization.Users;

public class UserInCustomerListDto : CreationAuditedEntityDto<long>
{
    public decimal TotalBillingAmount{ get; set; }
}
