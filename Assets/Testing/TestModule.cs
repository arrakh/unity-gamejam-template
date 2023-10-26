using System;
using System.Threading.Tasks;
using Arr.EventsSystem;
using Arr.ModulesSystem;
using Arr.PrefabRegistrySystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Testing
{
    public struct TestStruct
    {
        public string hoho;
    }
    
    
    public class TestModule : BaseModule, IEventListener<TestStruct>
    {
        [InjectModule] public OtherTestModule module;

        protected override async Task OnLoad()
        {
            Debug.Log($"ACCESSING OTHER MODULE, {module.a}");

            var test = GlobalEvents.Query<TestClass>();
            Debug.Log($"QUERY RESULT: {test.wat}");
            
            var test2 = GlobalEvents.Query<TestClass, TestStruct>(new (){hoho = "uwhuuuuuh?"});
            Debug.Log($"QUERY 2 RESULT: {test2.wat}");

            var test3 = GlobalEvents.Query<OtherTestClass>();
            Debug.Log($"QUERY 3 RESULT: {test3.num}");

            var spherePrefab = PrefabRegistry.Get("sphere");

            var boxPrefab = PrefabRegistry.Get("box");
            
            var otherPrefab = PrefabRegistry.Get("other");

            Object.Instantiate(spherePrefab);
            Object.Instantiate(boxPrefab);
            Object.Instantiate(otherPrefab);

            if (PrefabRegistry.TryGet("test", out var prefab)) throw new Exception("uwhuuhhh???");
            else Debug.Log("Can't get test prefab, perfect!");
        }

        public void OnEvent(TestStruct data)
        {
            Debug.Log($"Got Event {data.hoho}");
        }
    }
}