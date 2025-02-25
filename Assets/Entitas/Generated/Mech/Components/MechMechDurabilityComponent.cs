//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MechEntity {

    public Mech.DurabilityComponent mechDurability { get { return (Mech.DurabilityComponent)GetComponent(MechComponentsLookup.MechDurability); } }
    public bool hasMechDurability { get { return HasComponent(MechComponentsLookup.MechDurability); } }

    public void AddMechDurability(int newDurability) {
        var index = MechComponentsLookup.MechDurability;
        var component = (Mech.DurabilityComponent)CreateComponent(index, typeof(Mech.DurabilityComponent));
        component.Durability = newDurability;
        AddComponent(index, component);
    }

    public void ReplaceMechDurability(int newDurability) {
        var index = MechComponentsLookup.MechDurability;
        var component = (Mech.DurabilityComponent)CreateComponent(index, typeof(Mech.DurabilityComponent));
        component.Durability = newDurability;
        ReplaceComponent(index, component);
    }

    public void RemoveMechDurability() {
        RemoveComponent(MechComponentsLookup.MechDurability);
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

    static Entitas.IMatcher<MechEntity> _matcherMechDurability;

    public static Entitas.IMatcher<MechEntity> MechDurability {
        get {
            if (_matcherMechDurability == null) {
                var matcher = (Entitas.Matcher<MechEntity>)Entitas.Matcher<MechEntity>.AllOf(MechComponentsLookup.MechDurability);
                matcher.componentNames = MechComponentsLookup.componentNames;
                _matcherMechDurability = matcher;
            }

            return _matcherMechDurability;
        }
    }
}
