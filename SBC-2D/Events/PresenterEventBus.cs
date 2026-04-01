using System;
using System.Collections.Generic;

namespace SBC_2D.Events
{
    /// <summary>
    /// 事件總線 — 簡單的 Pub/Sub 管道
    /// 用途：A 和 B 透過事件通訊，互相不知道彼此
    /// </summary>
    public class PresenterEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers;
        private readonly object _lock = new object();

        public PresenterEventBus()
        {
            _handlers = new Dictionary<Type, List<Delegate>>();
        }

        public void Publish<T>(T payload)
        {
            Delegate[] snapshot;

            lock (_lock)
            {
                var type = typeof(T);
                if (!_handlers.TryGetValue(type, out var list))
                    return;
                snapshot = list.ToArray();
            }

            // 在鎖外執行，避免長時間卡住
            foreach (var handler in snapshot)
            {
                try
                {
                    ((Action<T>)handler)(payload);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[EventBus] {typeof(T).Name} handler failed: {ex.Message}");
                }
            }
        }

        public void Subscribe<T>(Action<T> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var type = typeof(T);

            lock (_lock)
            {
                if (!_handlers.TryGetValue(type, out var list))
                {
                    list = new List<Delegate>();
                    _handlers[type] = list;
                }
                list.Add(handler);
            }
        }

        /// <summary>取消訂閱</summary>
        public void Unsubscribe<T>(Action<T> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var type = typeof(T);

            lock (_lock)
            {
                if (_handlers.TryGetValue(type, out var list))
                {
                    list.Remove(handler);
                    if (list.Count == 0)
                        _handlers.Remove(type);
                }
            }
        }
    }
}