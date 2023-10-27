using System.Threading.Tasks;
using Arr.ModulesSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Arr.MiscModules
{
    public class UnityEventSystemModule : BaseModule
    {
        private GameObject gameObject;
        
        protected override async Task OnLoad()
        {
            gameObject = Resources.Load<GameObject>("EventSystem");
        }

        protected override async Task OnUnload()
        {
            if (gameObject) Object.DestroyImmediate(gameObject);
        }
    }
}