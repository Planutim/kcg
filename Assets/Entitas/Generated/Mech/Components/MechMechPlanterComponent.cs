//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MechEntity {

    public Mech.PlanterComponent mechPlanter { get { return (Mech.PlanterComponent)GetComponent(MechComponentsLookup.MechPlanter); } }
    public bool hasMechPlanter { get { return HasComponent(MechComponentsLookup.MechPlanter); } }

    public void AddMechPlanter(bool newGotPlant, int newPlantMechID) {
        var index = MechComponentsLookup.MechPlanter;
        var component = (Mech.PlanterComponent)CreateComponent(index, typeof(Mech.PlanterComponent));
        component.GotPlant = newGotPlant;
        component.PlantMechID = newPlantMechID;
        AddComponent(index, component);
    }

    public void ReplaceMechPlanter(bool newGotPlant, int newPlantMechID) {
        var index = MechComponentsLookup.MechPlanter;
        var component = (Mech.PlanterComponent)CreateComponent(index, typeof(Mech.PlanterComponent));
        component.GotPlant = newGotPlant;
        component.PlantMechID = newPlantMechID;
        ReplaceComponent(index, component);
    }

    public void RemoveMechPlanter() {
        RemoveComponent(MechComponentsLookup.MechPlanter);
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
public sealed partial class MechMatcher {

    static Entitas.IMatcher<MechEntity> _matcherMechPlanter;

    public static Entitas.IMatcher<MechEntity> MechPlanter {
        get {
            if (_matcherMechPlanter == null) {
                var matcher = (Entitas.Matcher<MechEntity>)Entitas.Matcher<MechEntity>.AllOf(MechComponentsLookup.MechPlanter);
                matcher.componentNames = MechComponentsLookup.componentNames;
                _matcherMechPlanter = matcher;
            }

            return _matcherMechPlanter;
        }
    }
}
