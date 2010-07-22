﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jint.Tests
{
    [TestClass]
    public class Prototype_array_methods
    {
        [TestMethod]
        public void Array_supports_clear()
        {
            var jint = new JintEngine();
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
 ar.clear();
 return ar;
");
            Assert.AreEqual(0,result.Count);
        }

        [TestMethod]
        public void Array_should_support_collect_method()
        {
            var jint = new JintEngine();
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect(function(a){
    return a+1;
});");
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3,result[1]);
            Assert.AreEqual(4, result[2]);
            Assert.AreEqual(5, result[3]);
        }
        [TestMethod]
        public void Array_collect_method_if_func_is_not_passed_returns_same_array()
        {
            var jint = new JintEngine();
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect();");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }
        [TestMethod]
        public void Array_collect_method_if_null_is_passed_returns_same_array()
        {
            var jint = new JintEngine();
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect(null);");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }
        [TestMethod]
        public void Array_collect_method_if_null_string_is_passed_returns_same_array()
        {
            var jint = new JintEngine();
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect('null');");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }
    }
}
