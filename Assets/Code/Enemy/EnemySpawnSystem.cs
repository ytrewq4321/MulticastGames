using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemySpawnSystem))]
public sealed class EnemySpawnSystem : UpdateSystem
{
    public GameObject[] enemies;
    private Filter isDeadMarker;
    private MeshCollider groundMesh;

    public override void OnAwake()
    {
        isDeadMarker = this.World.Filter.With<IsDeadMarker>();
        groundMesh = this.World.Filter.With<Ground>().FirstOrDefault().GetComponent<Ground>().mesh;

    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in isDeadMarker)
        {
            Instantiate(enemies[Random.Range(0, 3)],
                new Vector3(Random.Range(groundMesh.bounds.min.x, groundMesh.bounds.max.x), 0,
                Random.Range(groundMesh.bounds.min.z, groundMesh.bounds.max.z)), Quaternion.identity);
        }
    }
}