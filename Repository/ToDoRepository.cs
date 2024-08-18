using Microsoft.EntityFrameworkCore;
using ToDoAPI.DBContext;
using ToDoAPI.IHelper;
using ToDoAPI.Model;

namespace ToDoAPI.Repository
{
    public class ToDoRepository : IToDo
    {
        private readonly AppDbContext _appDbContext;

        public ToDoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AddToDoModel> AddNewToDo(AddToDoModel addToDo)
        {
            try
            {
                if (ValidateData(addToDo))
                {
                    await _appDbContext.ToDoDetails.AddAsync(addToDo);
                    await _appDbContext.SaveChangesAsync();

                    return addToDo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task DeleteToDo(int ToDoId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddToDoModel>> GetAllTodoList()
        {
            try
            {
              var data =  _appDbContext.ToDoDetails.ToListAsync();

                if (data != null)
                {
                    return await data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<AddToDoModel> UpdateToDo(AddToDoModel updateToDo)
        {
            throw new NotImplementedException();
        }

        private bool ValidateData(AddToDoModel toDoModel)
        {
            if (toDoModel == null)
                return false;
            else if (toDoModel.TaskStartDateTime < toDoModel.TaskEndDateTime)
                return false;
            else
                return true;
        }
    }
}
