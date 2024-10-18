using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTarget : MonoBehaviour
{
    private static BirdTarget instance;

  
    [SerializeField] GameObject target;
    [SerializeField] GameObject lockOn;

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

    private void OnTriggerEnter(Collider other)
    {

        target.SetActive(false);
        lockOn.SetActive(true); ;
    }

    private void OnTriggerExit(Collider other)
    {

        SetTarget();
    }

    public void SetTarget()
    {
        target.SetActive(true);
        lockOn.SetActive(false); 
    }
}
