using Abp.Application.Services.Dto;
using Microsoft.Graph;

namespace MyTraining1121AngularDemo.PhoneBook
{
    public class PhoneInPersonListDto : CreationAuditedEntityDto<long>
    {
        public PhoneType Type { get; set; }

        public string Number { get; set; }
    }

}