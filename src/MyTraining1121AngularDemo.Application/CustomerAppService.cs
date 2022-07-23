using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using MyTraining1121AngularDemo.Authorization;
using MyTraining1121AngularDemo.CustomerMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Customer)]
    public class CustomerAppService : MyTraining1121AngularDemoAppServiceBase, ICustomerAppService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerAppService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ListResultDto<CustomerListDto> GetCustomer(GetCustomerInput input)
        {
            var customer = _customerRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.CustomerName.Contains(input.Filter) ||
                        p.EmailAddress.Contains(input.Filter)||
                        p.Address.Contains(input.Filter)
                )
                .OrderBy(p => p.CustomerName)
                
                .ToList();

            return new ListResultDto<CustomerListDto>(ObjectMapper.Map<List<CustomerListDto>>(customer));
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_CreateCustomer)]
        public async Task CreateCustomer(CreateCustomerInput input)
        {
            var customer = ObjectMapper.Map<Customer>(input);
            await _customerRepository.InsertAsync(customer);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_DeleteCustomer)]
        public async Task DeleteCustomer(EntityDto input)
        {
            await _customerRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]
        public async Task<GetCustomerForEditOutput> GetCustomerForEdit(GetCustomerForEditInput input)
        {
            var customer = await _customerRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetCustomerForEditOutput>(customer);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]
        public async Task EditCustomer(EditCustomerInput input)
        {
            var customer = await _customerRepository.GetAsync(input.Id);
            customer.CustomerName = input.CustomerName;
            customer.EmailAddress = input.EmailAddress;
            customer.RegistrationDate=input.RegistrationDate;
            customer.Address=input.Address;
            await _customerRepository.UpdateAsync(customer);
        }

    }

}
