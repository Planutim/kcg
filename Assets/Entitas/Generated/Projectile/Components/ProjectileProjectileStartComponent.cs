//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ProjectileEntity {

    public Projectile.StartComponent projectileStart { get { return (Projectile.StartComponent)GetComponent(ProjectileComponentsLookup.ProjectileStart); } }
    public bool hasProjectileStart { get { return HasComponent(ProjectileComponentsLookup.ProjectileStart); } }

    public void AddProjectileStart(KMath.Vec2f newStarPos, float newStartTime) {
        var index = ProjectileComponentsLookup.ProjectileStart;
        var component = (Projectile.StartComponent)CreateComponent(index, typeof(Projectile.StartComponent));
        component.StarPos = newStarPos;
        component.StartTime = newStartTime;
        AddComponent(index, component);
    }

    public void ReplaceProjectileStart(KMath.Vec2f newStarPos, float newStartTime) {
        var index = ProjectileComponentsLookup.ProjectileStart;
        var component = (Projectile.StartComponent)CreateComponent(index, typeof(Projectile.StartComponent));
        component.StarPos = newStarPos;
        component.StartTime = newStartTime;
        ReplaceComponent(index, component);
    }

    public void RemoveProjectileStart() {
        RemoveComponent(ProjectileComponentsLookup.ProjectileStart);
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
public sealed partial class ProjectileMatcher {

    static Entitas.IMatcher<ProjectileEntity> _matcherProjectileStart;

    public static Entitas.IMatcher<ProjectileEntity> ProjectileStart {
        get {
            if (_matcherProjectileStart == null) {
                var matcher = (Entitas.Matcher<ProjectileEntity>)Entitas.Matcher<ProjectileEntity>.AllOf(ProjectileComponentsLookup.ProjectileStart);
                matcher.componentNames = ProjectileComponentsLookup.componentNames;
                _matcherProjectileStart = matcher;
            }

            return _matcherProjectileStart;
        }
    }
}
