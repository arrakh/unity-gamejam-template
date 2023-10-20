using System.Threading.Tasks;

namespace Arr.ModulesSystem
{
    public abstract class BaseModule
    {
        public virtual Task OnInitialized() => Task.CompletedTask;

        public virtual Task OnLoad() => Task.CompletedTask;

        public virtual Task OnUnload() => Task.CompletedTask;
    }
}