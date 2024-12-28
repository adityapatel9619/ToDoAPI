using Microsoft.AspNetCore.Mvc;
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

        //Add New ToDo List
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
            catch (Exception)
            {

                throw;
            }
        }

        //Delete ToDo Data By ID
        public async Task DeleteToDo(int ToDoId)
        {
            try
            {
                var data =  await _appDbContext.ToDoDetails.FirstOrDefaultAsync(t=> t.Id == ToDoId);

                if (data!=null)
                {
                    _appDbContext.ToDoDetails.Remove(data);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Fetch All ToDo List
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
            catch (Exception)
            {

                throw;
            }
        }

        //Update ToDo List By ID
        public async Task<AddToDoModel> UpdateToDo(AddToDoModel updateToDo)
        {
            var result = await _appDbContext.ToDoDetails.FirstOrDefaultAsync(e => e.Id == updateToDo.Id);

            if (result != null)
            {
                result.TaskName = (updateToDo.TaskName == null? result.TaskName.Trim():updateToDo.TaskName.Trim());
                result.TaskDescription = (updateToDo.TaskDescription == null? result.TaskDescription:updateToDo.TaskDescription);
                result.TaskStartDateTime = (updateToDo.TaskStartDateTime == null ? result.TaskStartDateTime : updateToDo.TaskStartDateTime);
                result.TaskEndDateTime = (updateToDo.TaskEndDateTime == null ? result.TaskEndDateTime : updateToDo.TaskEndDateTime);
                result.IsPriorityLow = (updateToDo.IsPriorityLow == null ? result.IsPriorityLow : updateToDo.IsPriorityLow);
                result.IsPriorityMedium = (updateToDo.IsPriorityMedium == null ? result.IsPriorityMedium : updateToDo.IsPriorityMedium);
                result.IsPriorityHigh = (updateToDo.IsPriorityHigh == null ? result.IsPriorityHigh : updateToDo.IsPriorityHigh);
                result.IsCompleted = (updateToDo.IsCompleted == null ? result.IsCompleted : updateToDo.IsCompleted);
                result.UserId = (updateToDo.UserId == 0 ? result.UserId : updateToDo.UserId);

                await _appDbContext.SaveChangesAsync();
                return result;
            }
            else
            {
                return null;
            }
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


        //Fetch Single ToDo List By ID
        public async Task<AddToDoModel> GetToDoById(int ToDoID)
        {
            try
            {
                var data = _appDbContext.ToDoDetails.FirstOrDefaultAsync(e => e.Id == ToDoID);

                if (data != null)
                {
                    return await data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
