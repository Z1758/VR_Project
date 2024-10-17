using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTarget : MonoBehaviour
{
    private static BirdTarget instance;




    public static BirdTarget Instance
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
