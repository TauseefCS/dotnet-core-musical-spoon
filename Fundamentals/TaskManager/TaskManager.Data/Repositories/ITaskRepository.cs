using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Data.Repositories
{
    public interface ITaskRepository
    {
        TaskItem Add(TaskItem model);
        TaskItem Edit(TaskItem model);
        bool Remove(TaskItem model);
        bool RemoveById(long taskId);
        IEnumerable<TaskItem> SearchByTitle(string searchTerm);
        int Commit();
        TaskItem GetById(long Id);
        List<TaskItem> GetUpcomingTasks();
        List<TaskItem> GetOverdueTasks();
    }
}
