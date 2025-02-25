//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class FloatingTextEntity {

    public FloatingText.IDComponent floatingTextID { get { return (FloatingText.IDComponent)GetComponent(FloatingTextComponentsLookup.FloatingTextID); } }
    public bool hasFloatingTextID { get { return HasComponent(FloatingTextComponentsLookup.FloatingTextID); } }

    public void AddFloatingTextID(int newID, int newIndex) {
        var index = FloatingTextComponentsLookup.FloatingTextID;
        var component = (FloatingText.IDComponent)CreateComponent(index, typeof(FloatingText.IDComponent));
        component.ID = newID;
        component.Index = newIndex;
        AddComponent(index, component);
    }

    public void ReplaceFloatingTextID(int newID, int newIndex) {
        var index = FloatingTextComponentsLookup.FloatingTextID;
        var component = (FloatingText.IDComponent)CreateComponent(index, typeof(FloatingText.IDComponent));
        component.ID = newID;
        component.Index = newIndex;
        ReplaceComponent(index, component);
    }

    public void RemoveFloatingTextID() {
        RemoveComponent(FloatingTextComponentsLookup.FloatingTextID);
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
public sealed partial class FloatingTextMatcher {

    static Entitas.IMatcher<FloatingTextEntity> _matcherFloatingTextID;

    public static Entitas.IMatcher<FloatingTextEntity> FloatingTextID {
        get {
            if (_matcherFloatingTextID == null) {
                var matcher = (Entitas.Matcher<FloatingTextEntity>)Entitas.Matcher<FloatingTextEntity>.AllOf(FloatingTextComponentsLookup.FloatingTextID);
                matcher.componentNames = FloatingTextComponentsLookup.componentNames;
                _matcherFloatingTextID = matcher;
            }

            return _matcherFloatingTextID;
        }
    }
}
