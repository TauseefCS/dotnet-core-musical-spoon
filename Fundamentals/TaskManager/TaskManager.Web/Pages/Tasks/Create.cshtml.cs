using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TaskManager.Core.Models;
using TaskManager.Data.Repositories;

namespace TaskManager.Web.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskRepository repo;
        private readonly IHtmlHelper htmlHelper;
        [BindProperty]
        public TaskItem TaskItem { get; set; }
        public IEnumerable<SelectListItem> TaskStatusList { get; set; }
        public IEnumerable<SelectListItem> TaskPriorityList { get; set; }
        public CreateModel(ITaskRepository repo, IHtmlHelper htmlHelper)
        {
            this.repo = repo;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet()
        {
            FillDropdownItems();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                repo.Add(TaskItem);
                TempData["Message"] = "Task Saved Successfully";
                return RedirectToPage("Detail", new { taskId = TaskItem.Id });
            }
            FillDropdownItems();
            return Page();
        }
        private void FillDropdownItems()
        {
            TaskStatusList = htmlHelper.GetEnumSelectList<TaskStatusType>();
            TaskPriorityList = htmlHelper.GetEnumSelectList<TaskPriorityType>();
        }
    }
}