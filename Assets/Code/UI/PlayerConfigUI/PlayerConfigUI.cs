using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct PlayerConfigUI : IComponent {
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI RadiusText;
    public Button UpgradeBtn;
}