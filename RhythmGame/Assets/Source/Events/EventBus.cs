using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus<EventClass> : IEventBus<EventClass>
{
    public void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventClass
    {
        throw new NotImplementedException();
    }

    public void Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventClass
    {
        throw new NotImplementedException();
    }

    public void Unsubscribe<TEventBase>(Action<TEventBase> token) where TEventBase : EventClass
    {
        throw new NotImplementedException();
    }

}
