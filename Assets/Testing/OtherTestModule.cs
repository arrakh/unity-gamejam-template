using Arr.EventsSystem;
using Arr.ModulesSystem;
using UnityEngine;

namespace Testing
{
    public class TestClass
    {
        public string wat;
    }

    public class OtherTestClass
    {
        public int num;
    }

    public struct TestGenericStruct<T>{}

    public class OtherTestModule : BaseModule, 
        IQueryProvider<OtherTestClass>, 
        IQueryProvider<TestClass, TestStruct>,
        IEventListener<TestGenericStruct<TestClass>>
    {
        public int a = 3;

        public TestClass OnQuery(TestStruct data)
        {
            Debug.Log("WWWWWW");
            return new TestClass() {wat = data.hoho};
        }

        OtherTestClass IQueryProvider<OtherTestClass>.OnQuery()
        {
            return new OtherTestClass() {num = 69};
        }

        public void OnEvent(TestGenericStruct<TestClass> data)
        {
            
        }
    }
}