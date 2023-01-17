using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.InputSystem;


[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputSystem))]
public sealed class PlayerInputSystem : UpdateSystem
{
    private InputActions inputActions;
    private Filter filter;
    private Vector2 moveInput;

    public override void OnAwake() {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Move.performed += SetMove;
        inputActions.Player.Move.canceled += SetMove;
        this.filter = this.World.Filter.With<PlayerInput>();     
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in filter)
        {
            ref var playerInputComponent = ref entity.GetComponent<PlayerInput>();
            playerInputComponent.MoveInput = moveInput; 
        }   
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public override void Dispose()
    {
        inputActions.Player.Move.performed -= SetMove;
        inputActions.Player.Move.canceled -= SetMove;
        inputActions.Disable();
    }


}