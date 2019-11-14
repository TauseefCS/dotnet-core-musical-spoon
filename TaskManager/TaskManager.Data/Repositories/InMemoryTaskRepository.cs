using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Data.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        public InMemoryTaskRepository()
        {
            TaskItems = new List<TaskItem>() {
                new TaskItem(){
                    Id=1,
                    Title="Plan Daily Items",
                    Description="Plan Daily Items",
                    Priority = TaskPriorityType.Normal,
                    Status =TaskStatusType.Open
                },
                new TaskItem(){
                    Id=2,
                    Title="Review Emails",
                    Description="Review Emails",
                    Priority = TaskPriorityType.High,
                    Status =TaskStatusType.Open
                },
                new TaskItem(){
                    Id=3,
                    Title="Followup Clients",
                    Description="Followup Clients",
                    Priority = TaskPriorityType.Critical,
                    Status =TaskStatusType.Open
                }
            };
        }
        public List<TaskItem> TaskItems { get; set; }
        public TaskItem Add(TaskItem model)
        {
            model.Id = TaskItems.Count > 0 ? TaskItems.Max(x => x.Id) + 1 : 0;
            TaskItems.Add(model);
            return model;
        }
        public int Commit()
        {
            return 1;
        }
        public TaskItem Edit(TaskItem model)
        {
            var item = TaskItems.FirstOrDefault(x => x.Id == model.Id);
            if (item != null)
            {
                var index = TaskItems.IndexOf(item);
                TaskItems[index] = model;
            }
            return item;
        }
        public TaskItem GetById(long Id)
        {
            return TaskItems.Find(x => x.Id == Id);
        }
        public IEnumerable<TaskItem> SearchByTitle(string searchTerm = null)
        {
            var query = from item in TaskItems
                        where string.IsNullOrWhiteSpace(searchTerm) || item.Title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1
                        select item;
            return query.ToList();
        }
        public bool Remove(TaskItem taskItem)
        {
            return TaskItems.Remove(taskItem);
        }
        public bool RemoveById(long taskId)
        {
            var taskItem = TaskItems.FirstOrDefault(x => x.Id == taskId);
            if (taskItem != null)
            {
                return TaskItems.Remove(taskItem);
            }
            return false;
        }
        public List<TaskItem> GetUpcomingTasks()
        {
            var query = from item in TaskItems
                        where item.DueBy != null && item.DueBy.Value.Date >= DateTime.Now.Date
                        orderby item.DueBy ascending
                        select item;
            return query.ToList();
        }
        public List<TaskItem> GetOverdueTasks()
        {            
            var query = from item in TaskItems
                        where item.DueBy != null && item.DueBy.Value.Date < DateTime.Now.Date &&
                        (item.Status != TaskStatusType.Cancelled || item.Status != TaskStatusType.Completed)
                        orderby item.DueBy ascending
                        select item;
            return query.ToList();
        }
    }
}
