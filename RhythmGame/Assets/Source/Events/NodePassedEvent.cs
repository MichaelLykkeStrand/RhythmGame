using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePassedEvent : EventBase
{
    public PositionNode positionNode;
    public NodePassedEvent(PositionNode node)
    {
        this.positionNode = node;
    }
}
