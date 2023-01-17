using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TargetSearchSystem))]
public sealed class TargetSearchSystem : UpdateSystem
{
    public PlayerConfig Config;
    private Filter enemyFilter;
    private Filter playerFilter;
    private float radiusSqr;

    public override void OnAwake()
    {
        enemyFilter = this.World.Filter.With<Enemy>();
        playerFilter = this.World.Filter.With<Player>();
        radiusSqr = Config.radius.Amount.Value * Config.radius.Amount.Value;
    }

    public override void OnUpdate(float deltaTime)
    {
        if (enemyFilter.IsEmpty())
            return;

        ref var playerTransformComponent = ref playerFilter.FirstOrDefault().GetComponent<Player>();

        foreach (var entity in enemyFilter)
        {
            ref var enemyTransformComponent = ref entity.GetComponent<Enemy>();

            var heading = enemyTransformComponent.Transform.position - playerTransformComponent.Transform.position;
            if (heading.sqrMagnitude <= radiusSqr && !entity.Has<TargetForAttack>()
                && Config.CurrentCountEnemy<Config.MaxCountEnemyForAttack)
            {
                entity.SetComponent(new TargetForAttack());
                Config.CurrentCountEnemy++;
            }
            else if(entity.Has<TargetForAttack>() &&  heading.sqrMagnitude > radiusSqr)
            {
                entity.RemoveComponent<TargetForAttack>();
                Config.CurrentCountEnemy--;
            }
        }
    }
}