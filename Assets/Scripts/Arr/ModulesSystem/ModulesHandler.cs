using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Arr.EventsSystem;
using Unity.VisualScripting;
using UnityEngine;
using EventHandler = Arr.EventsSystem.EventHandler;

namespace Arr.ModulesSystem
{
    public class ModulesHandler
    {
        private Dictionary<Type, BaseModule> modules;
        private EventHandler eventHandler;

        public ModulesHandler(BaseModule[] modules, EventHandler eventHandler)
        {
            this.modules = new(modules.Length);
            foreach (var module in modules)
            {
                var type = module.GetType();
                if (this.modules.ContainsKey(type))
                    throw new Exception($"Trying to add duplicate instance of type {type.Name}");
                this.modules[type] = module;
            }
            
            this.eventHandler = eventHandler;
        }

        public async Task Start()
        {
            foreach (var module in modules.Values)
            {
                InjectEvents(module);
                await module.OnInitialized();
            }

            foreach (var pair in modules)
                InjectDependencies(pair.Key, pair.Value);

            foreach (var module in modules.Values)
                await module.OnLoad();
        }

        private void InjectDependencies(Type moduleType, BaseModule instance)
        {
            var fields = moduleType.GetFields();
            Debug.Log($"INJECTING DEPENDENCY FOR {moduleType.Name} WITH {fields.Length}");

            foreach (var field in fields)
            {
                var attrib = field.GetCustomAttribute(typeof(InjectModuleAttribute));
                if (attrib is not InjectModuleAttribute) continue;
                var type = field.FieldType;
                Debug.Log($"GOT INJECT MODULE ATTRIB WITH TYPE {type.Name}");

                if (!typeof(BaseModule).IsAssignableFrom(type))
                    throw new Exception($"Trying to inject type {type.Name} but it is not a Module!");
                
                if (!modules.TryGetValue(type, out var module))
                    throw new Exception($"Trying to inject type {type.Name} but could not find the Module!");
                
                TypedReference tr = __makeref(instance);
                field.SetValueDirect(tr, module);
            }
        }

        private void InjectEvents(BaseModule module)
        {
            var interfaces = module.GetType().GetInterfaces();
            foreach (var i in interfaces)
            {
                
                if (!i.IsGenericType) continue;

                var genericType = i.GetGenericTypeDefinition();
                var args = i.GetGenericArguments();
                if (genericType == typeof(IEventListener<>) && args.Length == 1) RegisterEventType(module, args[0]);
                else if (genericType == typeof(IQueryProvider<>)) RegisterQueryType(module, args[0]);
                else if (genericType == typeof(IQueryProvider<,>) && args.Length == 2) RegisterParamQueryType(module, args[0], args[1]);
            }
        }

        private void RegisterEventType(BaseModule module, Type eventType)
        {
            var method = typeof(EventHandler)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.IsGenericMethod && m.Name == nameof(EventHandler.Register)
                                                       && m.GetParameters().Length == 1
                                                       && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEventListener<>));
            
            var genericMethod = method.MakeGenericMethod(eventType);
            genericMethod.Invoke(eventHandler, new object[] { module });
        }

        private void RegisterQueryType(BaseModule module, Type returnType)
        {
            var method = typeof(EventHandler)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.IsGenericMethod && m.Name == nameof(EventHandler.Register)
                                                       && m.GetParameters().Length == 1
                                                       && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryProvider<>));
                             
            if (method == null)
            {
                Debug.LogError("Failed to find the Register method.");
                return;
            }
            var genericMethod = method.MakeGenericMethod(returnType);

            genericMethod.Invoke(eventHandler, new object[] { module });
        }

        private void RegisterParamQueryType(BaseModule module, Type returnType, Type paramType)
        {
            var method = typeof(EventHandler)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.IsGenericMethod && m.Name == nameof(EventHandler.Register)
                                                       && m.GetParameters().Length == 1
                                                       && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryProvider<,>));
            Debug.Log($"Register Param Query Type Return {returnType.Name}, Param {paramType.Name}");
            if (method == null)
            {
                Debug.LogError("Failed to find the Register method.");
                return;
            }
            var genericMethod = method.MakeGenericMethod(returnType, paramType);

            genericMethod.Invoke(eventHandler, new object[] { module });
        }

        public async Task Stop()
        {
            foreach (var module in modules.Values)
            {
                await module.OnUnload();
            }
        }
    }
}