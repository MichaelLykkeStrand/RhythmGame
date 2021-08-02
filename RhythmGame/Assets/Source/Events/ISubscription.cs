using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscription<EventClass>
{
    object Token { get; }
    void Publish(EventClass eventBase);
}
