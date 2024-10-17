using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BirdController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] LayerMask mask;
    public void MoveTarget()
    {
      
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, mask))
        {
            
           target.position = hit.point;
        }

    }

    private void Update()
    {
        MoveTarget();
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.red);
    }
}
