using MD.TODO.Data.Repository;
using MD.TODO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.TODO.Service
{
    public interface ITodoItemService
    {
        IQueryable<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(int id);
        void InsertTodoItem(TodoItem todo);
        void UpdateTodoItem(TodoItem todo);
        void DeleteTodoItem(TodoItem todo);
    }

    public class TodoItemService : ITodoItemService
    {
        private readonly IRepository<TodoItem> _todoItemRepository;

        public TodoItemService(IRepository<TodoItem> todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public IQueryable<TodoItem> GetTodoItems()
        {
            return _todoItemRepository.Table;
        }

        public TodoItem GetTodoItem(int id)
        {
            return _todoItemRepository.GetById(id);
        }

        public void InsertTodoItem(TodoItem todo)
        {
            _todoItemRepository.Insert(todo);
        }

        public void UpdateTodoItem(TodoItem todo)
        {
            _todoItemRepository.Update(todo);
        }

        public void DeleteTodoItem(TodoItem todo)
        {
            _todoItemRepository.Delete(todo);
        }
    }
}
