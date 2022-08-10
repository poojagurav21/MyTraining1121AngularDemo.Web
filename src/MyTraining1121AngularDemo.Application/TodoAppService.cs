using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using MyTraining1121AngularDemo.Authorization;
using MyTraining1121AngularDemo.TodoApplication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Todo)]

    public class TodoAppService : ApplicationService, ITodoAppService
    {
        private readonly IRepository<TodoItem> _todoItemRepository;

        public TodoAppService(IRepository<TodoItem> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Todo_CreateTodo)]

        public async Task<TodoItemDto> CreateAsync(string text)
        {
            var todoItem = await _todoItemRepository.InsertAsync(
                new TodoItem { Text = text }
            );

            return new TodoItemDto
            {
                Id = todoItem.Id,
                Text = todoItem.Text
            };
        }
        public async Task DeleteMultipleTodoAsync(List<int> Id)
        {
            foreach (var id in Id)
            {
                await _todoItemRepository.DeleteAsync(id);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Todo_DeleteTodo)]

        public async Task DeleteAsync(int id)
        {
            await _todoItemRepository.DeleteAsync(id);
        }

        public async Task<List<TodoItemDto>> GetListAsync()
        {
            var items = await _todoItemRepository.GetAllListAsync();
            return items
                .OrderBy(c => c.Text)
                .Select(item => new TodoItemDto
                {
                    Id = item.Id,
                    Text = item.Text
                }).ToList();
        }
    }
}
