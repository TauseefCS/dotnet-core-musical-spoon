using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Repositories;

namespace TaskManager.Web.ViewComponents
{
    public class OverDueTasks : ViewComponent
    {
        private readonly ITaskRepository repo;

        public OverDueTasks(ITaskRepository repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = repo.GetOverdueTasks();
            return View(list);
        }
    }
}
