using Arr;
using Arr.ModulesSystem;

namespace Testing
{
    public class TestModulesLoader : ModulesLoader
    {
        protected override BaseModule[] Modules => new BaseModule[]
        {
            new TestViewModule(),
            new OtherTestModule(),
            new TestModule()
        };
    }
}