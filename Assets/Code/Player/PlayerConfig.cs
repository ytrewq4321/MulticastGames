using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerConfig", menuName ="Player/PlayerConfiguration")]   
public class PlayerConfig : ScriptableObject
{
    public Config speed;
    public Config damage;
    public Config radius;
    public int MaxCountEnemyForAttack;

    [System.NonSerialized] public int CurrentCountEnemy;
    [System.NonSerialized] public List<Config> list;

    public void CreateList()
    {
        list = new List<Config>();
        list.Add(speed);
        list.Add(damage);
        list.Add(radius);
        list.Sort();
    }
}
