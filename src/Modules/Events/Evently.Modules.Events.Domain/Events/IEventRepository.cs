using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Domain.Events;
public interface IEventRepository
{
    void Insert(Event @event);
}
