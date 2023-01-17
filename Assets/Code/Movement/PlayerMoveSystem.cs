using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMoveSystem))]
public sealed class PlayerMoveSystem : UpdateSystem
{
    public PlayerConfig config;
    private Filter filter;

    public override void OnAwake() {
        this.filter = this.World.Filter.With<PlayerInput>()
                                       .With<Player>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in filter)
        {
            ref var inputComponent = ref entity.GetComponent<PlayerInput>();
            ref var transformComponent = ref entity.GetComponent<Player>();

            transformComponent.Transform.position += new Vector3(inputComponent.MoveInput.x,
            0, inputComponent.MoveInput.y)*config.speed.Amount.Value*deltaTime;
        }
    }
}