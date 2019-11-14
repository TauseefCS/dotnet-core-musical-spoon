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
    public class ListModel : PageModel
    {
        private readonly ITaskRepository repo;
        public IEnumerable<TaskItem> TaskItems { get; set; }
        [TempData]
        public string Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ListModel(ITaskRepository repo)
        {
            this.repo = repo;
        }
        public void OnGet()
        {
            TaskItems = repo.SearchByTitle(this.SearchTerm);
        }
        public IActionResult OnGetDelete(int taskId)
        {
            if (repo.RemoveById(taskId))
            {
                TempData["Message"] = "Task : " + taskId + " Deleted Successfully";
            }
            return RedirectToPage("./List");
        }
    }
}