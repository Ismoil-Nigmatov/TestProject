using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entity;
using TestProject.Repository;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<User> _userManager;

        public TaskController(ITaskRepository taskRepository, UserManager<User> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        public async Task<ViewResult> Index()
        {
            var tasks = await _taskRepository.GetAll();
            return View("Task", tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TaskViewModel taskViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _taskRepository.Add(taskViewModel, userId!, username!);
            var tasks = await _taskRepository.GetAll();
            return View("Task", tasks);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, TaskViewModel taskViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _taskRepository.Update(id, taskViewModel, userId!, username!);
            var tasks = await _taskRepository.GetAll();
            return View("Task", tasks);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _taskRepository.Delete(id, userId!, username!);
            var tasks = await _taskRepository.GetAll();
            return View("Task", tasks);
        }
    }
}
