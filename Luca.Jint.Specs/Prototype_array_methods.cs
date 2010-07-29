﻿using System.Collections.Generic;
using Jint;
using Luca.Jint.PrototypeExtension;
using NUnit.Framework;
using SharpTestsEx;

namespace Luca.Jint.Specs
{
    [TestFixture]
    public class When_we_extend_arrays_with_PrototypeExtensions
    {
        public IList<IExtensionRegister> prototype = new List<IExtensionRegister> {new Registration()};

        [Test]
        public void should_support_uniq()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"return [[1, 3, 2, 1].uniq(), ['A','a'].uniq(),[1,2,2,3,3,3,4,5].uniq(true)];");
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(3, result[0][1]);
            Assert.AreEqual(2, result[0][2]);
            Assert.AreEqual("A", result[1][0]);
            Assert.AreEqual("a", result[1][1]);
            Assert.AreEqual(1, result[2][0]);
            Assert.AreEqual(2, result[2][1]);
            Assert.AreEqual(3, result[2][2]);
            Assert.AreEqual(4, result[2][3]);
            Assert.AreEqual(5, result[2][4]);

        }

        [Test]
        public void should_support_size()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"return [3, 6, 1, 20].size();");
            Assert.AreEqual(4,result);
        }

        [Test]
        public void should_support_reverse()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var nums = [3, 6, 1, 20];
var rev = nums.reverse(false);
var nums2 = [3, 5, 6, 1];
nums2.reverse();
return [nums,rev, nums2];
");
            Assert.AreEqual(3, result[0][0]);
            Assert.AreEqual(6, result[0][1]);
            Assert.AreEqual(1, result[0][2]);
            Assert.AreEqual(20, result[0][3]);
            Assert.AreEqual(20, result[1][0]);
            Assert.AreEqual(1, result[1][1]);
            Assert.AreEqual(6, result[1][2]);
            Assert.AreEqual(3, result[1][3]); 
            Assert.AreEqual(1, result[2][0]);
            Assert.AreEqual(6, result[2][1]);
            Assert.AreEqual(5, result[2][2]);
            Assert.AreEqual(3, result[2][3]);
        }

        [Test]
        public void should_support_without()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [3, 5, 6].without(3);
                var br = [3, 5, 6, 20].without(20, 6)
                var cr = [1,2,3].without(4);
return [ar,br,cr];
");
            Assert.AreEqual(5,result[0][0]);
            Assert.AreEqual(6, result[0][1]);
            Assert.AreEqual(3, result[1][0]);
            Assert.AreEqual(5, result[1][1]);
            Assert.AreEqual(1, result[2][0]);
            Assert.AreEqual(2, result[2][1]);
            Assert.AreEqual(3, result[2][2]);
        }

        [Test]
        public void should_support_toArray()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
            var br = ar;
            var cr = ar.toArray();
            cr[0] = 3;
            return [ar,br,cr]
");
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(1, result[1][0]);
            Assert.AreEqual(3, result[2][0]);
        }

        [Test]
        public void if_value_to_search_is_of_the_wrong_type_lastIndexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['1','2','3'];
return ar.lastIndexOf(2);
");
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void if_value_to_search_is_not_found_lastIndexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','mary'];
return ar.lastIndexOf('joseph');
");
            Assert.AreEqual(-1, result);
        }
        [Test]
        public void if_value_to_search_is_not_passed_lastIndexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','mary'];
return ar.lastIndexOf();
");
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void if_array_is_empty_lastIndexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [];
return ar.lastIndexOf('bob');
");
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void should_support_lastIndexOf_with_offset()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','lisa','mark','bob',3];
return ar.lastIndexOf('bob',2);
");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void should_support_lastIndexOf()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','lisa','mark','bob',3];
return ar.lastIndexOf('bob');
");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void should_support_intersect()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['john','ruiz','mary','bob','julian','esther',3];
var br = ['john','mary','luther','bob','cesar',3,'esther',6,new Date()];
return ar.intersect(br);
");
            Assert.AreEqual(5,result.Count);
            Assert.AreEqual("john", result[0]);
            Assert.AreEqual("mary", result[1]);
            Assert.AreEqual("bob", result[2]);
            Assert.AreEqual("esther", result[3]);
            Assert.AreEqual(3, result[4]);
        }

        [Test]
        public void if_value_to_search_is_of_the_wrong_type_indexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['1','2','3'];
return ar.indexOf(2);
");
            Assert.AreEqual(-1, result);            
        }

        [Test]
        public void if_value_to_search_is_not_found_indexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','mary'];
return ar.indexOf('joseph');
");
            Assert.AreEqual(-1, result);
        }
        [Test]
        public void if_value_to_search_is_not_passed_indexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','mary'];
return ar.indexOf();
");
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void if_array_is_empty_indexOf_should_return_minus_one()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [];
return ar.indexOf('bob');
");
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void should_support_indexOf()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = ['frank','bob','lisa'];
return ar.indexOf('bob');
");
            Assert.AreEqual(1, result);
        }

        [Test]
        public void should_support_flattern()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var a = ['frank', ['bob', 'lisa'], ['jill', ['tom', 'sally']]];
var b = a.flatten();
return [a,b];
");
            var a = result[0];
            var b = result[1];
            Assert.AreEqual("frank",a[0]);
            Assert.AreEqual("bob", a[1][0]);
            Assert.AreEqual("lisa", a[1][1]);
            Assert.AreEqual("jill", a[2][0]);
            Assert.AreEqual("tom", a[2][1][0]);
            Assert.AreEqual("sally", a[2][1][1]);


            Assert.AreEqual("frank",b[0]);
            Assert.AreEqual("bob", b[1]);
            Assert.AreEqual("lisa", b[2]);
            Assert.AreEqual("jill", b[3]);
            Assert.AreEqual("tom", b[4]);
            Assert.AreEqual("sally", b[5]);
        }

        [Test]
        public void if_array_is_empty_last_should_return_null()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [];
return ar.last();
");
            Assert.IsNull(result);
        }
        [Test]
        public void should_support_last()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.last();
");
            Assert.AreEqual(4,result);            
        }

        [Test]
        public void if_array_is_empty_first_should_return_null()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [];
return ar.first();
");
            Assert.IsNull(result);
        }
        [Test]
        public void should_support_first()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.first();
");
            Assert.AreEqual(1,result);
        }

        [Test]
        public void should_support_compact()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,null,3, undefined,6, 'null'];
return ar.compact()");

            Assert.AreEqual(1,result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(6, result[3]);
            Assert.AreEqual("null", result[4]);

        }

        [Test]
        public void should_support_clone()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
            var br = ar;
            var cr = ar.clone();
            cr[0] = 3;
            return [ar,br,cr]
");
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1,result[0][0]);
            Assert.AreEqual(1, result[1][0]);
            Assert.AreEqual(3, result[2][0]);
        }
        [Test]
        public void should_supports_clear()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
 ar.clear();
 return ar;
");
            Assert.AreEqual(0,result.Count);
        }

        [Test]
        public void should_support_collect()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect(function(a){
    return a+1;
});");
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3,result[1]);
            Assert.AreEqual(4, result[2]);
            Assert.AreEqual(5, result[3]);
        }
        [Test]
        public void if_collect_dont_get_a_func_should_return_same_array()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect();");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }
        [Test]
        public void if_collect_gets_a_null_func_should_return_same_array()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect(null);");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }
        [Test]
        public void if_collect_gets_another_object_instead_of_a_func_should_return_same_array()
        {
            var jint = new JintEngine(prototype);
            dynamic result = jint.Run(@"var ar = [1,2,3,4];
return ar.collect('null');");
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }
    }
}