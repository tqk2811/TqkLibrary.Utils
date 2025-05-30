//using TqkLibrary.Utils;

//namespace TestProject
//{
//    [TestClass]
//    public sealed class UriHelpersTest
//    {
//        public static IEnumerable<object[]> Datas
//        {
//            get
//            {
//                yield return new object[] { new Uri("https://www.facebook.com/?sk=nf"), "www.facebook.com" };
//                yield return new object[] { new Uri("https://localhost:2132/"), "localhost:2132" };
//                yield return new object[] { new Uri("https://127.0.0.1:2132/"), "127.0.0.1:2132" };
//                yield return new object[] { new Uri("https://abc:313s@localhost:2132/"), "localhost:2132" };
//            }
//        }


//        [TestMethod, DynamicData(nameof(Datas))]
//        public void TestMethod(Uri uri, string? domain)
//        {

//        }
//    }
//}
