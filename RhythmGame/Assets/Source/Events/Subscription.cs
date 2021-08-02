using System;

internal class Subscription<TEventBase> : ISubscription where TEventBase : EventBase
{
    private readonly Action<TEventBase> _action;
    public object Token { get { return _action; } }

    public Subscription(Action<TEventBase> action)
    {
        if (action == null)
            throw new ArgumentNullException("action");

        _action = action;
    }

    public void Publish(EventBase eventItem)
    {
        if (!(eventItem is TEventBase))
            throw new ArgumentException("Event Item is not the correct type.");

        _action.Invoke(eventItem as TEventBase);
    }
}
