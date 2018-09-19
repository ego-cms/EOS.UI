using System;
using System.Threading.Tasks;

namespace EOS.UI.Shared.Themes.Extensions
{
    public static class TaskExtension
    {
        /// <summary>
        /// Animates the following animation with task wrapping
        /// </summary>
        /// <returns>Task with wrapped animation</returns>
        /// <param name="animateNextTask">Following task</param>
        /// <param name="action">Action.</param>
        public static Task AnimateNext(this Task animateNextTask, Action<Task> action)
        {
            return animateNextTask.ContinueWith(action, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Animates the following animation with task wrapping
        /// </summary>
        /// <returns>Task with wrapped animation</returns>
        /// <param name="animateNextTask">Following task</param>
        /// <param name="action">Action.</param>
        public static Task<TResult> AnimateNext<TResult>(this Task animateNextTask, Func<Task, TResult> action)
        {
            return animateNextTask.ContinueWith(action, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
