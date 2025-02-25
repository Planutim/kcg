//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MechEntity {

    public Mech.InventoryComponent mechInventory { get { return (Mech.InventoryComponent)GetComponent(MechComponentsLookup.MechInventory); } }
    public bool hasMechInventory { get { return HasComponent(MechComponentsLookup.MechInventory); } }

    public void AddMechInventory(int newInventoryID) {
        var index = MechComponentsLookup.MechInventory;
        var component = (Mech.InventoryComponent)CreateComponent(index, typeof(Mech.InventoryComponent));
        component.InventoryID = newInventoryID;
        AddComponent(index, component);
    }

    public void ReplaceMechInventory(int newInventoryID) {
        var index = MechComponentsLookup.MechInventory;
        var component = (Mech.InventoryComponent)CreateComponent(index, typeof(Mech.InventoryComponent));
        component.InventoryID = newInventoryID;
        ReplaceComponent(index, component);
    }

    public void RemoveMechInventory() {
        RemoveComponent(MechComponentsLookup.MechInventory);
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

    static Entitas.IMatcher<MechEntity> _matcherMechInventory;

    public static Entitas.IMatcher<MechEntity> MechInventory {
        get {
            if (_matcherMechInventory == null) {
                var matcher = (Entitas.Matcher<MechEntity>)Entitas.Matcher<MechEntity>.AllOf(MechComponentsLookup.MechInventory);
                matcher.componentNames = MechComponentsLookup.componentNames;
                _matcherMechInventory = matcher;
            }

            return _matcherMechInventory;
        }
    }
}
