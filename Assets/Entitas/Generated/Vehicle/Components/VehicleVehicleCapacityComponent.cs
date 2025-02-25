//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class VehicleEntity {

    public Vehicle.CapacityComponent vehicleCapacity { get { return (Vehicle.CapacityComponent)GetComponent(VehicleComponentsLookup.VehicleCapacity); } }
    public bool hasVehicleCapacity { get { return HasComponent(VehicleComponentsLookup.VehicleCapacity); } }

    public void AddVehicleCapacity(System.Collections.Generic.List<AgentEntity> newAgentsInside) {
        var index = VehicleComponentsLookup.VehicleCapacity;
        var component = (Vehicle.CapacityComponent)CreateComponent(index, typeof(Vehicle.CapacityComponent));
        component.agentsInside = newAgentsInside;
        AddComponent(index, component);
    }

    public void ReplaceVehicleCapacity(System.Collections.Generic.List<AgentEntity> newAgentsInside) {
        var index = VehicleComponentsLookup.VehicleCapacity;
        var component = (Vehicle.CapacityComponent)CreateComponent(index, typeof(Vehicle.CapacityComponent));
        component.agentsInside = newAgentsInside;
        ReplaceComponent(index, component);
    }

    public void RemoveVehicleCapacity() {
        RemoveComponent(VehicleComponentsLookup.VehicleCapacity);
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
public sealed partial class VehicleMatcher {

    static Entitas.IMatcher<VehicleEntity> _matcherVehicleCapacity;

    public static Entitas.IMatcher<VehicleEntity> VehicleCapacity {
        get {
            if (_matcherVehicleCapacity == null) {
                var matcher = (Entitas.Matcher<VehicleEntity>)Entitas.Matcher<VehicleEntity>.AllOf(VehicleComponentsLookup.VehicleCapacity);
                matcher.componentNames = VehicleComponentsLookup.componentNames;
                _matcherVehicleCapacity = matcher;
            }

            return _matcherVehicleCapacity;
        }
    }
}
