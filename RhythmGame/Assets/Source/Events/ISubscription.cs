using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscription
{
    object Token { get; }
    void Publish(EventBase eventBase);
}
