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
                        //return StatusCode(StatusCodes.Status201Created,""+addToDo.TaskName.ToString()+ " is created Successfully");

                        //This line returns newly created ToDo after creating
                        return CreatedAtAction(nameof(GetToDoById), new { id = data.Id }, data);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
                //throw new Exception(ex.Message.ToString());
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
                    return NotFound();
                }                    
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
                //throw new Exception(ex.Message.ToString());
            }
        }

        [HttpPut]
        [ActionName("UpdateToDo")]
        public async Task<ActionResult> UpdateToDo(AddToDoModel updToDo)
        {
            try
            {
                var output = await _toDo.UpdateToDo(updToDo);
                if (output != null)
                {
                    return StatusCode(StatusCodes.Status200OK, "" + updToDo.TaskName.ToString() + " is Updated Successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent, "Not Updated");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
                //throw new Exception(ex.Message.ToString());
            }
        }

        [HttpDelete]
        [ActionName("DeleteToDo")]
        public async Task DeleteToDo(int Id)
        {
            try
            {
                //int delID = delToDo.Id;
                await _toDo.DeleteToDo(Id);
            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
                //throw new Exception(ex.Message.ToString());
            }
        }

        [HttpGet("{id:int}")]
        [ActionName("GetToDoData")]
        public async Task<ActionResult> GetToDoById(int id)
        {
            try
            {
                var dataToDo = await _toDo.GetToDoById(id);

                if (dataToDo != null)
                {
                    return Ok(dataToDo);
                }
                else
                {
                    return NotFound("No Data Found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
                //throw new Exception(ex.Message.ToString());
            }
        }

    }
}
