using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    public class StringHelper
    {
        public string Reverse_Linq(string str)
        {
            return new string(str.Reverse().ToArray());
        }

        public string Reverse_Array(string str)
        {
            var s = "";
            for (var i = str.Length - 1; i >= 0; i--)
            {
                s += str[i];
            }

            return s;
        }

        public string Reverse_Stack(string str)
        {
            var stack = new Stack<char>();
            for (var i = 0; i < str.Length; i++)
            {
                stack.Push(str[i]);
            }

            return new string(stack.ToArray());
        }
    }

    [TestClass]
    public class StringReverseTests
    {
        private StringHelper _sh;

        [TestInitialize]
        public void SetUp()
        {
            _sh = new StringHelper();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var actual = _sh.Reverse_Linq("ABC");
            Assert.AreEqual("CBA", actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var actual = _sh.Reverse_Array("ABC");
            Assert.AreEqual("CBA", actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var actual = _sh.Reverse_Stack("ABC");
            Assert.AreEqual("CBA", actual);
        }
    }
}