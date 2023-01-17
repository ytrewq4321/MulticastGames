using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem
{
    private Filter filter;

    public override void OnAwake()
    {
        this.filter = this.World.Filter.With<HealthComponent>()
                                       .With<DamageEvent>();

    }

    public override void OnUpdate(float deltaTime)
    {
        foreach ( var entity in filter)
        {
            ref var health = ref entity.GetComponent<HealthComponent>();
            ref var damage = ref entity.GetComponent<DamageEvent>();
            health.HealthPoints -= damage.Damage;

            if (health.HealthPoints <= 0)
                entity.SetComponent(new IsDeadMarker());
        }
    }
}