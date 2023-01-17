using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DeathSystem))]
public sealed class DeathSystem : LateUpdateSystem
{
    public PlayerConfig Config;
    private Filter deadEntities;
    private Score score;
    
    public override void OnAwake()
    {
        deadEntities = this.World.Filter.With<IsDeadMarker>();
        score = this.World.Filter.With<Score>().FirstOrDefault().GetComponent<Score>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in deadEntities)
        {
            IncreaseScore();

            GameObject enemy =  entity.GetComponent<Enemy>().Transform.gameObject;
            
            Config.CurrentCountEnemy--;
            World.RemoveEntity(entity);
            Destroy(enemy);
        }
    }

    private void IncreaseScore()
    {
        score.TotalScore.Value++;
    }
}