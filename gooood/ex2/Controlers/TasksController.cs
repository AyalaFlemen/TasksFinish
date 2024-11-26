using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ex2.Services;
using ex2.Models;

namespace ex2.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksServices _taskService;

        public TasksController(ITasksServices taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var task = _taskService.Get();
            return Ok(task);
        }
        [HttpGet("{id}")]
        public IActionResult GetTaskByUser(int id)
        {
            var task = _taskService.GetTaskByUser(id);
            if (task != null)
                return Ok(task);
            else
                return BadRequest("not found");
        }
        [HttpPost]
        public IActionResult Add([FromBody] Tasks task)
        {

            var success = _taskService.Add(task);
            if (success)
                return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
            return BadRequest("userId undefind");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Tasks task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _taskService.Update(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);
            return NoContent();
        }
        //[HttpPost]
        //public IActionResult addAttachment(Attachment attachment)
        //{
        //    return Ok(_taskService.addAttachment(attachment));
        //}

        //[HttpGet("{id}")]
        //public IActionResult getByProject(int ProjactId)
        //{
        //    var succeed = _taskService.getByProject(ProjactId);
        //    if (succeed != null)
        //        return Ok(succeed);
        //    else
        //        return BadRequest("not found");
        //}
    }
}
