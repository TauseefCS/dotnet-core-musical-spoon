using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Data.Repositories;

namespace TaskManager.Web.ViewComponents
{
    public class UpcomingTasksViewComponent : ViewComponent
    {
        private readonly ITaskRepository repo;

        public UpcomingTasksViewComponent(ITaskRepository repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var upcomingTasks = repo.GetUpcomingTasks();
            return View(upcomingTasks);
        }
    }
}
