//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ItemParticleEntity {

    public Item.PhysicsStateComponent itemPhysicsState { get { return (Item.PhysicsStateComponent)GetComponent(ItemParticleComponentsLookup.ItemPhysicsState); } }
    public bool hasItemPhysicsState { get { return HasComponent(ItemParticleComponentsLookup.ItemPhysicsState); } }

    public void AddItemPhysicsState(KMath.Vec2f newPosition, KMath.Vec2f newPreviousPosition, KMath.Vec2f newVelocity, KMath.Vec2f newAcceleration, bool newOnGrounded) {
        var index = ItemParticleComponentsLookup.ItemPhysicsState;
        var component = (Item.PhysicsStateComponent)CreateComponent(index, typeof(Item.PhysicsStateComponent));
        component.Position = newPosition;
        component.PreviousPosition = newPreviousPosition;
        component.Velocity = newVelocity;
        component.Acceleration = newAcceleration;
        component.OnGrounded = newOnGrounded;
        AddComponent(index, component);
    }

    public void ReplaceItemPhysicsState(KMath.Vec2f newPosition, KMath.Vec2f newPreviousPosition, KMath.Vec2f newVelocity, KMath.Vec2f newAcceleration, bool newOnGrounded) {
        var index = ItemParticleComponentsLookup.ItemPhysicsState;
        var component = (Item.PhysicsStateComponent)CreateComponent(index, typeof(Item.PhysicsStateComponent));
        component.Position = newPosition;
        component.PreviousPosition = newPreviousPosition;
        component.Velocity = newVelocity;
        component.Acceleration = newAcceleration;
        component.OnGrounded = newOnGrounded;
        ReplaceComponent(index, component);
    }

    public void RemoveItemPhysicsState() {
        RemoveComponent(ItemParticleComponentsLookup.ItemPhysicsState);
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
public sealed partial class ItemParticleMatcher {

    static Entitas.IMatcher<ItemParticleEntity> _matcherItemPhysicsState;

    public static Entitas.IMatcher<ItemParticleEntity> ItemPhysicsState {
        get {
            if (_matcherItemPhysicsState == null) {
                var matcher = (Entitas.Matcher<ItemParticleEntity>)Entitas.Matcher<ItemParticleEntity>.AllOf(ItemParticleComponentsLookup.ItemPhysicsState);
                matcher.componentNames = ItemParticleComponentsLookup.componentNames;
                _matcherItemPhysicsState = matcher;
            }

            return _matcherItemPhysicsState;
        }
    }
}
