using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = int.MinValue)]

public class EnemyStatus : ScriptableObject
{
    public GameObject effect;
    public int hp;
    public float speed;
    public float range;
    public float cooltime;
}
