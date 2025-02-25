//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class ProjectileComponentsLookup {

    public const int AnimationState = 0;
    public const int ECSInput = 1;
    public const int PhysicsBox2DCollider = 2;
    public const int PhysicsSphere2DCollider = 3;
    public const int ProjectileDamage = 4;
    public const int ProjectileDelete = 5;
    public const int ProjectileExplosive = 6;
    public const int ProjectileFirstFrame = 7;
    public const int ProjectileFirstHIt = 8;
    public const int ProjectileID = 9;
    public const int ProjectileLinearDrag = 10;
    public const int ProjectileOnHit = 11;
    public const int ProjectilePhysicsState = 12;
    public const int ProjectileRange = 13;
    public const int ProjectileSprite2D = 14;
    public const int ProjectileStart = 15;
    public const int ProjectileType = 16;

    public const int TotalComponents = 17;

    public static readonly string[] componentNames = {
        "AnimationState",
        "ECSInput",
        "PhysicsBox2DCollider",
        "PhysicsSphere2DCollider",
        "ProjectileDamage",
        "ProjectileDelete",
        "ProjectileExplosive",
        "ProjectileFirstFrame",
        "ProjectileFirstHIt",
        "ProjectileID",
        "ProjectileLinearDrag",
        "ProjectileOnHit",
        "ProjectilePhysicsState",
        "ProjectileRange",
        "ProjectileSprite2D",
        "ProjectileStart",
        "ProjectileType"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Animation.StateComponent),
        typeof(ECSInput.Component),
        typeof(Physics.Box2DColliderComponent),
        typeof(Physics.Sphere2DColliderComponent),
        typeof(Projectile.DamageComponent),
        typeof(Projectile.DeleteComponent),
        typeof(Projectile.ExplosiveComponent),
        typeof(Projectile.FirstFrameComponent),
        typeof(Projectile.FirstHItComponent),
        typeof(Projectile.IDComponent),
        typeof(Projectile.LinearDragComponent),
        typeof(Projectile.OnHitComponent),
        typeof(Projectile.PhysicsStateComponent),
        typeof(Projectile.RangeComponent),
        typeof(Projectile.Sprite2DComponent),
        typeof(Projectile.StartComponent),
        typeof(Projectile.TypeComponent)
    };
}
