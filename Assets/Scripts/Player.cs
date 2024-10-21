using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

   
    [SerializeField] private int hp;

    public int HP { get { return hp; } set { hp = value; } }

    [SerializeField] private int breadCount;
    public int BreadCount { get { return breadCount; } set { breadCount = value; } }

    [SerializeField] int bcMax;

    public static Player Instance
    {
        get
        {
            return instance;

        }
    }
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    public void PlusCount()
    {
        BreadCount++;

        if (BreadCount >= bcMax)
        {
           
            if (Bread.Instance.SetBread())
            {
                BreadCount -= bcMax;
            }
        }
    }

    public void TakeDamage()
    {
        HP--;
    }
    
}
