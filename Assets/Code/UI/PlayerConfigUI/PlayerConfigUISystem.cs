using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UniRx;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerConfigUISystem))]
public sealed class PlayerConfigUISystem : UpdateSystem {
    public PlayerConfig config;
    private Filter filter;
    private CompositeDisposable disposable;
    private float total;
    private float randomPoint;

    public override void OnAwake() {
        filter = this.World.Filter.With<PlayerConfigUI>();
        disposable = new CompositeDisposable();

        foreach (var entity in filter)
        {
            ref var text = ref entity.GetComponent<PlayerConfigUI>();
            var damagetext = text.DamageText;
            var speedtext = text.SpeedText;
            var radiustext = text.RadiusText;
            var button = text.UpgradeBtn;

            config.damage.Amount.Subscribe(x =>  damagetext.text = x.ToString()).AddTo(disposable);
            config.speed.Amount.Subscribe(x => speedtext.text = x.ToString()).AddTo(disposable);
            config.radius.Amount.Subscribe(x => radiustext.text = x.ToString()).AddTo(disposable);
            button.OnClickAsObservable().Subscribe(x => UpgradePlayer());
        }
    }

    public override void OnUpdate(float deltaTime) {
    }

    public int Choose()
    {
        config.CreateList();
        total = 0;

        for (int i = 0; i < config.list.Count; i++)
            total += config.list[i].IncrementChance;

        randomPoint = Random.value * total;

        for (int i = 0; i < config.list.Count; i++)
        {
            if (randomPoint < config.list[i].IncrementChance)
                return i;
            else
                randomPoint -= config.list[i].IncrementChance;
        }
        return config.list.Count - 1;
    }

    public void UpgradePlayer()
    {
        var index = Choose();
        config.list[index].Amount.Value += config.list[index].IncrementStep;
    }
}