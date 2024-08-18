using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAPI.IHelper;
using ToDoAPI.Model;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDo _toDo;

        public ToDoController(IToDo toDo)
        {
            _toDo = toDo;
        }

        [HttpPost]
        [ActionName("CreateToDo")]
        public async Task<ActionResult> CreateToDo(AddToDoModel addToDo)
        {
            try
            {
                if (addToDo != null)
                {
                   var data =  await _toDo.AddNewToDo(addToDo);
                    if (data != null)
                    {
                        return StatusCode(StatusCodes.Status201Created,""+addToDo.TaskName.ToString()+ " is created Successfully");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent,"Please Check Input");
                    }

                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent,"Cannot Add ToDo");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        [HttpGet]
        [ActionName("GetAllToDoData")]
        public async Task<IActionResult> GetAllToDo()
        {
            try
            {
                var output = await _toDo.GetAllTodoList();

                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return NoContent();
                }
                    
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }


    }
}
