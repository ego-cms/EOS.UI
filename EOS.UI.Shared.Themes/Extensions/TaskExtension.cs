using System;
using System.Threading.Tasks;

namespace EOS.UI.Shared.Themes.Extensions
{
    public static class TaskExtension
    {
        public static Task AnimateNext(this Task animateNextTask, Action<Task> action)
        {
            return animateNextTask.ContinueWith(action, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task<TResult> AnimateNext<TResult>(this Task animateNextTask, Func<Task, TResult> action)
        {
            return animateNextTask.ContinueWith(action, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
