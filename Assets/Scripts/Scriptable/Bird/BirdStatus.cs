using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/BirdData", order = int.MinValue)]
public class BirdStatus : ScriptableObject
{
    public int attack;
    public float speed;
    public int hp;
}
