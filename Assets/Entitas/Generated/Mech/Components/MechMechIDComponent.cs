//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MechEntity {

    public Mech.IDComponent mechID { get { return (Mech.IDComponent)GetComponent(MechComponentsLookup.MechID); } }
    public bool hasMechID { get { return HasComponent(MechComponentsLookup.MechID); } }

    public void AddMechID(int newID, int newIndex) {
        var index = MechComponentsLookup.MechID;
        var component = (Mech.IDComponent)CreateComponent(index, typeof(Mech.IDComponent));
        component.ID = newID;
        component.Index = newIndex;
        AddComponent(index, component);
    }

    public void ReplaceMechID(int newID, int newIndex) {
        var index = MechComponentsLookup.MechID;
        var component = (Mech.IDComponent)CreateComponent(index, typeof(Mech.IDComponent));
        component.ID = newID;
        component.Index = newIndex;
        ReplaceComponent(index, component);
    }

    public void RemoveMechID() {
        RemoveComponent(MechComponentsLookup.MechID);
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

    static Entitas.IMatcher<MechEntity> _matcherMechID;

    public static Entitas.IMatcher<MechEntity> MechID {
        get {
            if (_matcherMechID == null) {
                var matcher = (Entitas.Matcher<MechEntity>)Entitas.Matcher<MechEntity>.AllOf(MechComponentsLookup.MechID);
                matcher.componentNames = MechComponentsLookup.componentNames;
                _matcherMechID = matcher;
            }

            return _matcherMechID;
        }
    }
}
