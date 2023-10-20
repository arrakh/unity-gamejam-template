using Arr.ModulesSystem;

namespace DefaultNamespace
{
    public class TestModulesLoader : ModulesLoader
    {
        protected override BaseModule[] Modules => new BaseModule[]
        {
            new TestModule(),
            new OtherTestModule()
        };
    }
}