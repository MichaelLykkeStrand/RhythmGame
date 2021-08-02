using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventBus
{
    void Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventBase;
    void Unsubscribe<TEventBase>(Action<TEventBase> token) where TEventBase : EventBase;
    void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventBase;
}
