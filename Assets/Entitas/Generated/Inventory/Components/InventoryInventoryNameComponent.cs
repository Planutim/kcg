//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    public Inventory.NameComponent inventoryName { get { return (Inventory.NameComponent)GetComponent(InventoryComponentsLookup.InventoryName); } }
    public bool hasInventoryName { get { return HasComponent(InventoryComponentsLookup.InventoryName); } }

    public void AddInventoryName(string newName) {
        var index = InventoryComponentsLookup.InventoryName;
        var component = (Inventory.NameComponent)CreateComponent(index, typeof(Inventory.NameComponent));
        component.Name = newName;
        AddComponent(index, component);
    }

    public void ReplaceInventoryName(string newName) {
        var index = InventoryComponentsLookup.InventoryName;
        var component = (Inventory.NameComponent)CreateComponent(index, typeof(Inventory.NameComponent));
        component.Name = newName;
        ReplaceComponent(index, component);
    }

    public void RemoveInventoryName() {
        RemoveComponent(InventoryComponentsLookup.InventoryName);
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
public sealed partial class InventoryMatcher {

    static Entitas.IMatcher<InventoryEntity> _matcherInventoryName;

    public static Entitas.IMatcher<InventoryEntity> InventoryName {
        get {
            if (_matcherInventoryName == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.InventoryName);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherInventoryName = matcher;
            }

            return _matcherInventoryName;
        }
    }
}
