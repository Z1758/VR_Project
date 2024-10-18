using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdStat : MonoBehaviour
{
    private static BirdStat instance;

    [SerializeField] private float speed;

    public float SPEED { get { return speed; } }


    public static BirdStat Instance
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
}
