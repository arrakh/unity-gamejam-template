using System;
using Arr.EventsSystem;
using UnityEngine;
using EventHandler = Arr.EventsSystem.EventHandler;

namespace Arr.ModulesSystem
{
    public abstract class ModulesLoader : MonoBehaviour
    {
        protected abstract BaseModule[] Modules { get; }
        
        protected virtual EventHandler EventHandler => GlobalEvent.Instance;

        private ModulesHandler modulesHandler;
        
        private void Start()
        {
            modulesHandler = new ModulesHandler(Modules, EventHandler);
            
            modulesHandler.Start().CatchExceptions();
        }
    }
}