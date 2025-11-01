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
            Task<int> t_int = Task.FromResult<int>(0);
            double @double = await t_int.ContinueWith(Continue, "abc");
            Assert.AreEqual(@double, 1.0);
        }
        async Task<double> Continue(Task<int> t_int, string state)
        {
            await Task.Delay(0);
            return 1.0;
        }
    }
}
