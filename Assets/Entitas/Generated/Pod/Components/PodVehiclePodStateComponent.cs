//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class PodEntity {

    public Vehicle.Pod.StateComponent vehiclePodState { get { return (Vehicle.Pod.StateComponent)GetComponent(PodComponentsLookup.VehiclePodState); } }
    public bool hasVehiclePodState { get { return HasComponent(PodComponentsLookup.VehiclePodState); } }

    public void AddVehiclePodState(Enums.PodState newState) {
        var index = PodComponentsLookup.VehiclePodState;
        var component = (Vehicle.Pod.StateComponent)CreateComponent(index, typeof(Vehicle.Pod.StateComponent));
        component.State = newState;
        AddComponent(index, component);
    }

    public void ReplaceVehiclePodState(Enums.PodState newState) {
        var index = PodComponentsLookup.VehiclePodState;
        var component = (Vehicle.Pod.StateComponent)CreateComponent(index, typeof(Vehicle.Pod.StateComponent));
        component.State = newState;
        ReplaceComponent(index, component);
    }

    public void RemoveVehiclePodState() {
        RemoveComponent(PodComponentsLookup.VehiclePodState);
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
public sealed partial class PodMatcher {

    static Entitas.IMatcher<PodEntity> _matcherVehiclePodState;

    public static Entitas.IMatcher<PodEntity> VehiclePodState {
        get {
            if (_matcherVehiclePodState == null) {
                var matcher = (Entitas.Matcher<PodEntity>)Entitas.Matcher<PodEntity>.AllOf(PodComponentsLookup.VehiclePodState);
                matcher.componentNames = PodComponentsLookup.componentNames;
                _matcherVehiclePodState = matcher;
            }

            return _matcherVehiclePodState;
        }
    }
}
