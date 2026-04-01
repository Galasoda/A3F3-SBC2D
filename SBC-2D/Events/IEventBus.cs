using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Events
{
    public interface IEventBus
    {
        //T是事件型別
        void Publish<T>(T message);
        void Subscribe<T>(Action<T> handler);
    }
}
