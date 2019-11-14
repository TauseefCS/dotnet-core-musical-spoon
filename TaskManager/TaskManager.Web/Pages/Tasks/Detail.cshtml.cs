using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Core.Models;
using TaskManager.Data.Repositories;

namespace TaskManager.Web.Pages.Tasks
{
    public class DetailModel : PageModel
    {
        private readonly ITaskRepository repo;
        public TaskItem TaskItem { get; set; }
        [TempData]
        public string Message { get; set; }
        public DetailModel(ITaskRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult OnGet(int taskId)
        {
            TaskItem = repo.GetById(taskId);
            if (TaskItem == null)
            {
                // redirect to not found page
                return RedirectToPage("../NotFound");
            }
            return Page();
        }
    }
}