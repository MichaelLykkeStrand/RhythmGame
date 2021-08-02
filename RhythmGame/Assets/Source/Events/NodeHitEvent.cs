class NodeHitEvent : EventBase
{
    public PositionNode positionNode;
    public float accuracy;

    public NodeHitEvent(PositionNode node, float acc)
    {
        this.positionNode = node;
        this.accuracy = acc;
    }
}

