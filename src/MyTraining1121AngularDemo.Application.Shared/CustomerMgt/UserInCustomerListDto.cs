using Abp.Application.Services.Dto;
using MyTraining1121AngularDemo.Authorization.Users;

public class UserInCustomerListDto : CreationAuditedEntityDto<long>
{
    public long UserRefId { get; set; }
    public decimal TotalBillingAmount{ get; set; }
}
