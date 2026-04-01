using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Events
{
    //這是mediator
    public class PresenterEventBus : IEventBus
    {
        //這是事件資料的型別，這個事件型別會被數個委派方法給使用
        //Delegate 是 C# 裡所有方法的基底
        private Dictionary<Type, List<Delegate>> _handlers;
        
        public PresenterEventBus()
        {
            _handlers = new Dictionary<Type, List<Delegate>>();
        }

        public void Publish<T>(T payload)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
                return;

            foreach (Action<T> handler in _handlers[type].Cast<Action<T>>())
            {
                handler(payload);
            }
        }

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Delegate>();

            _handlers[type].Add(handler);
        }
    }
}
