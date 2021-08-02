using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventBus<EventClass>
{
    void Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventClass;
    void Unsubscribe<TEventBase>(Action<TEventBase> token) where TEventBase : EventClass;
    void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventClass;
}
