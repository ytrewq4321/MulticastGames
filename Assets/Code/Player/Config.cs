using UnityEngine;
using UniRx;
using System;

[CreateAssetMenu(fileName = "Config", menuName = "Player/Configuration")]
public class Config : ScriptableObject, IComparable<Config>
{
    public ReactiveProperty<float> Amount = new ReactiveProperty<float>();
    public float IncrementStep;
    public float IncrementChance;

    public int CompareTo(Config config)
    {
        return IncrementChance.CompareTo(config.IncrementChance);
    }
}
