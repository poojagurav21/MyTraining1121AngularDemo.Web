﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.CustomerMgt
{
    public interface ICustomerAppService : IApplicationService
    {
        ListResultDto<CustomerListDto> GetCustomer(GetCustomerInput input);
        Task CreateCustomer(CreateCustomerInput input);
        Task DeleteCustomer(EntityDto input);

    }
}
