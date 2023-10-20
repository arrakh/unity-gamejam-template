using System.Threading.Tasks;
using Arr.EventsSystem;
using Arr.ModulesSystem;
using UnityEngine;

namespace DefaultNamespace
{
    public struct TestStruct
    {
        public string hoho;
    }
    
    
    public class TestModule : BaseModule, IEventListener<TestStruct>
    {
        [InjectModule] public OtherTestModule module;

        public override async Task OnLoad()
        {
            Debug.Log($"ACCESSING OTHER MODULE, {module.a}");

            var test = GlobalEvent.Query<TestClass>();
            Debug.Log($"QUERY RESULT: {test.wat}");
            
            var test2 = GlobalEvent.Query<TestClass, TestStruct>(new (){hoho = "uwhuuuuuh?"});
            Debug.Log($"QUERY 2 RESULT: {test2.wat}");

            var test3 = GlobalEvent.Query<OtherTestClass>();
            Debug.Log($"QUERY 3 RESULT: {test3.num}");
        }

        public void OnEvent(TestStruct data)
        {
            Debug.Log($"Got Event {data.hoho}");
        }
    }
}