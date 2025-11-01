using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TqkLibrary.Utils;

namespace TestProject
{
    [TestClass]
    public class TaskUtilTest
    {
        [TestMethod]
        public async Task TestContinueWith()
        {
            Task task = Task.CompletedTask;
            Task<int> t_int = Task.FromResult<int>(0);

            await task.ContinueWith(Continue_void_Task, "abc");
            await t_int.ContinueWith(Continue_void_TaskT, "abc");
            await task.ContinueWith(Continue_Task_Task, "abc");
            await t_int.ContinueWith(Continue_Task_TaskT, "abc");
            double double_4 = await task.ContinueWith(Continue_TaskT_Task, "abc");
            double double_5 = await t_int.ContinueWith(Continue_TaskT_TaskT, "abc");
            Assert.AreEqual(double_4, 1.0);
            Assert.AreEqual(double_5, 1.0);
        }
        void Continue_void_Task(Task task, string state)
        {

        }
        void Continue_void_TaskT(Task<int> task, string state)
        {

        }
        Task Continue_Task_Task(Task task, string state)
        {
            return Task.CompletedTask;
        }
        Task Continue_Task_TaskT(Task<int> t_int, string state)
        {
            return Task.CompletedTask;
        }
        async Task<double> Continue_TaskT_Task(Task task, string state)
        {
            await Task.Delay(0);
            return 1.0;
        }
        async Task<double> Continue_TaskT_TaskT(Task<int> t_int, string state)
        {
            await Task.Delay(0);
            return 1.0;
        }
    }
}
