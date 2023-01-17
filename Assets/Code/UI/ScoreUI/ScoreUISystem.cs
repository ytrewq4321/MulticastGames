using Scellecs.Morpeh.Systems;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UniRx;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ScoreUISystem))]
public sealed class ScoreUISystem : UpdateSystem
{
    private ScoreUI scoreUI;
    private Score score;
    private CompositeDisposable disposable;

    public override void OnAwake() {
        scoreUI = this.World.Filter.With<ScoreUI>().FirstOrDefault().GetComponent<ScoreUI>() ;
        score = this.World.Filter.With<Score>().FirstOrDefault().GetComponent<Score>();
        disposable = new CompositeDisposable();

        score.TotalScore.Subscribe(x => scoreUI.textField.text = x.ToString());
    }

    public override void OnUpdate(float deltaTime) {
    }
}