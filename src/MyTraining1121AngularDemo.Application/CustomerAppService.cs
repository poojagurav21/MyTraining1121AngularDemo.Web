using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using MyTraining1121AngularDemo.Authorization;
using MyTraining1121AngularDemo.Authorization.Users;
using MyTraining1121AngularDemo.Authorization.Users.Dto;
using MyTraining1121AngularDemo.CustomerMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Customer)]
    public class CustomerAppService : MyTraining1121AngularDemoAppServiceBase, ICustomerAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerUsers, long> _customerUserRepository;
        private readonly IRepository<User, long> _userRepository;
        public CustomerAppService(IRepository<Customer> customerRepository, IRepository<CustomerUsers, long> customerUserRepository, IRepository<User, long> userRepository)
        {
            _customerRepository = customerRepository;
            _customerUserRepository = customerUserRepository;
            _userRepository = userRepository;
        }

        public ListResultDto<CustomerListDto> GetCustomer(GetCustomerInput input)
        {
            var customer = _customerRepository
                .GetAll()
                .Include(p => p.CustomerUsers)
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.CustomerName.Contains(input.Filter) ||
                        p.EmailAddress.Contains(input.Filter) ||
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

            await _customerRepository.InsertAndGetIdAsync(customer);
            var allCustomer = await _customerRepository.GetAll().ToListAsync();
            var lastCust = allCustomer.Last();
            var u = lastCust.Id;
            var customerId = u;
            var c = input.UserRefId;
            var userId = c;
            foreach (var user in userId)
            {
                var custmerUsers = new CustomerUsers
                {
                    CustomerRefId = customerId,
                    UserRefId = user,
                    TotalBillingAmount=654.87m
                };
                await _customerUserRepository.InsertAsync(custmerUsers);
            }
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
            customer.RegistrationDate = input.RegistrationDate;
            customer.Address = input.Address;
            //customer.UserRefId = input.UserRefId;
            await _customerRepository.UpdateAsync(customer);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]
        public async Task DeleteUser(EntityDto<long> input)
        {
            await _customerUserRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_EditCustomer)]
        public async Task<UserInCustomerListDto> AddUser(AddUserInput input)
        {

            try
            {
                var customer = _customerRepository.Get(input.CustomerRefId);
                await _customerRepository.EnsureCollectionLoadedAsync(customer, p => p.CustomerUsers);

                var user = ObjectMapper.Map<CustomerUsers>(input);
                customer.CustomerUsers.Add(user);

                //Get auto increment Id of the new Phone by saving to database
                await CurrentUnitOfWork.SaveChangesAsync();

                return ObjectMapper.Map<UserInCustomerListDto>(user);
            }
            catch
            {
                return null;
            }
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Customer_GetUser)]
        public ListResultDto<User> GetUser(GetUserInput input)
        {
            var user = _userRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Name.Contains(input.Filter) ||
                        p.EmailAddress.Contains(input.Filter)
                )
                .OrderBy(p => p.Name)

                .ToList();

            return new ListResultDto<User>(ObjectMapper.Map<List<User>>(user));
        }
        public List<UserViewDto> GetUserViewAsync(GetUserCustomerIdDto input)
        {
            var customer = _customerUserRepository
                .GetAll()
                .Include(u => u.user)
                .Where(p => p.CustomerRefId == input.Id)
                .Select(l => l.user)
                .ToList();
            return new List<UserViewDto>(ObjectMapper.Map<List<UserViewDto>>(customer));

        }
    }
}