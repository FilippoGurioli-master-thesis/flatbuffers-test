public class CollektiveEngineBehaviour : SingletonBehaviour<CollektiveEngineBehaviour>
{
    public GlobalData GlobalData => _engine.GlobalData;
    private ICollektiveEngine _engine;
    private List<CollektiveNode<SensorData, NodeState>> _nodes;
    //...
}

// handles the links visualization and management
// exposes something in the editor to configure its visualization
public class CollektiveLinkManager : SingletonBehaviour<CollektiveLinkManager>;
// other visualization related classes...

// to be implemented by the user
public abstract class CollektiveNode<TSensorData, TNodeState> : MonoBehaviour
{
    protected abstract TSensorData Sense();
    protected abstract void Act(TNodeState state);
}