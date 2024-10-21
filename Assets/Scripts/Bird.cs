using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] public AnimationInstancing.AnimationInstancing ani;
    [SerializeField] public BirdStatus status;


    [SerializeField] private int hp;
 
    public int HP { get { return hp; } set {  hp = value; } }
}
