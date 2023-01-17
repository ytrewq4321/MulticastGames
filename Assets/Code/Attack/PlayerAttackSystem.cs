using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerAttackSystem))]
public sealed class PlayerAttackSystem : FixedUpdateSystem
{
    public PlayerConfig Config;
    private Filter filter;
    private float lastAttackTime;

    public override void OnAwake()
    {
        this.filter = this.World.Filter.With<TargetForAttack>();
        lastAttackTime = 0f;
        Config.CurrentCountEnemy = 0;
    }

    public override void OnUpdate(float deltaTime)
    {
        if (filter.IsEmpty())
            return;

        if (Time.time > 1f + lastAttackTime)
        {
            lastAttackTime = Time.time;

            foreach (var entity in filter)
            {
                entity.SetComponent(new DamageEvent { Damage = Config.damage.Amount.Value });
            }
        }   
    }
}