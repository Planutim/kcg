//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AgentEntity {

    static readonly Agent.AliveComponent agentAliveComponent = new Agent.AliveComponent();

    public bool isAgentAlive {
        get { return HasComponent(AgentComponentsLookup.AgentAlive); }
        set {
            if (value != isAgentAlive) {
                var index = AgentComponentsLookup.AgentAlive;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : agentAliveComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class AgentMatcher {

    static Entitas.IMatcher<AgentEntity> _matcherAgentAlive;

    public static Entitas.IMatcher<AgentEntity> AgentAlive {
        get {
            if (_matcherAgentAlive == null) {
                var matcher = (Entitas.Matcher<AgentEntity>)Entitas.Matcher<AgentEntity>.AllOf(AgentComponentsLookup.AgentAlive);
                matcher.componentNames = AgentComponentsLookup.componentNames;
                _matcherAgentAlive = matcher;
            }

            return _matcherAgentAlive;
        }
    }
}
