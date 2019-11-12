using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Core.Models;
using TaskManager.Data.Repositories;

namespace TaskManager.Web.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskRepository repo;
        private readonly IHtmlHelper htmlHelper;
        [BindProperty]
        public TaskItem TaskItem { get; set; }
        public IEnumerable<SelectListItem> TaskStatusList { get; set; }
        public IEnumerable<SelectListItem> TaskPriorityList { get; set; }
        public EditModel(ITaskRepository repo, IHtmlHelper htmlHelper)
        {
            this.repo = repo;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(long taskId)
        {
            //find task
            TaskItem = repo.GetById(taskId);
            if (TaskItem == null)
            {
                // redirect to not found page
                return RedirectToPage("../NotFound");
            }
            FillDropdownItems();
            return Page();
        }
        public IActionResult OnPost(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                repo.Edit(taskItem);
                TempData["Message"] = "Task Saved Successfully";
                return RedirectToPage("./Detail", new { taskId = taskItem.Id });
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