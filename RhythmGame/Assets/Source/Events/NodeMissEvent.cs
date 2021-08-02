class NodeMissEvent : EventBase
{
    public PositionNode positionNode;
    public float accuracy;

    public NodeMissEvent(PositionNode node)
    {
        this.positionNode = node;
    }
}

