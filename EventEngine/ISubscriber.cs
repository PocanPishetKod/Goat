using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.EventEngine
{
    public interface ISubscriber<TEvent>
    {
        void HandleEvent(TEvent @event);
    }
}
