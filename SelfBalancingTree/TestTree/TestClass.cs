using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTree
{
    public class TestClass : IComparable<TestClass>
    {
        private static int counter = 0;
        public TestClass()
        {
            this.MyProperty = counter;
            counter++;
        }
        public int MyProperty { get; set; }

        public int CompareTo(TestClass other)
        {
            if (this.MyProperty == other.MyProperty)
            {
                return 0;
            }
            else if (this.MyProperty > other.MyProperty)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
