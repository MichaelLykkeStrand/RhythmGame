using System;

public class CommonEvent : EventBase
{
    public Object sender;
    public EventBaseArgs args;
    public CommonEvent(Object sender, EventBaseArgs e)
    {
        this.sender = sender;
        this.args = e;
    }
}
