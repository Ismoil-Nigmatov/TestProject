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

    [HttpGet("id")]
    public async Task<IActionResult> Index(int id) => Ok(await _taskRepository.Get(id));

    [HttpPost]
    public async Task<IActionResult> Create(TaskViewModel task)
    {
        task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);

        DateTime now = DateTime.Now;
        if (task.DueDate < now)
        {
            throw new BadHttpRequestException("Date must not be before today");
        }
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.GetUserAsync(User);
        var username = user!.UserName;
        await _taskRepository.Add(task, userId!, username!);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Edit(int id, TaskViewModel task)
    {
        DateTime now = DateTime.Now;
        if (task.DueDate < now)
        {
            throw new Exception("Date must not be before today");
        }
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.GetUserAsync(User);
        var username = user!.UserName;
        await _taskRepository.Update(id, task, userId!, username!);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.GetUserAsync(User);
        var username = user!.UserName;
        await _taskRepository.Delete(id, userId!, username!);
        return Ok();
    }
}