using System.Threading.Tasks;
using Arr.EventsSystem;
using Arr.ViewModuleSystem;
using UnityEngine;

namespace Testing
{
    public class TestViewModule : ViewModule<TestView>, IQueryProvider<TestClass>
    {
        protected override async Task OnLoad()
        {
            await base.OnLoad();
            Debug.Log("On load");
        }

        TestClass IQueryProvider<TestClass>.OnQuery()
        {
            return new TestClass() {wat = view.GetInput()};
        }
    }
}