using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entity;
using TestProject.Repository;
using TestProject.ViewModels;

namespace TestProject.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ADMIN")]
public class TaskApiController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly UserManager<User> _userManager;
    public TaskApiController(ITaskRepository taskRepository, UserManager<User> userManager)
    {
        _taskRepository = taskRepository;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> Index() => Ok(await _taskRepository.GetAll());

    [HttpPost]
    public async Task<IActionResult> Create(TaskViewModel task)
    {
        task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);

        DateTime thresholdDate = new DateTime(2030, 1, 1);
        DateTime now = DateTime.Now;
        if (task.DueDate > thresholdDate)
        {
            throw new Exception("please enter again date");
        }

        if (task.DueDate < now)
        {
            throw new Exception("please enter again date");
        }
        await _taskRepository.Add(task);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Edit(int id, TaskViewModel task)
    {
        await _taskRepository.Update(id, task);
        return Ok();
    }



    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _taskRepository.Delete(id);
        return Ok();
    }
}