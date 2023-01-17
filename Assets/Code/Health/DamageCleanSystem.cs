using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageCleanSystem))]
public sealed class DamageCleanSystem : LateUpdateSystem
{
    private Filter filter;
    public override void OnAwake()
    {
        filter = World.Filter.With<DamageEvent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            entity.RemoveComponent<DamageEvent>();
        }
    }
}