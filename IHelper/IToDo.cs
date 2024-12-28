using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Model;

namespace ToDoAPI.IHelper
{
    public interface IToDo
    {
        Task<AddToDoModel> AddNewToDo(AddToDoModel addToDo);
        Task<IEnumerable<AddToDoModel>> GetAllTodoList();
        Task<AddToDoModel> UpdateToDo(AddToDoModel updateToDo);
        Task DeleteToDo(int ToDoId);
        Task <AddToDoModel> GetToDoById(int ToDoId);
    }
}