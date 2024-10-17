using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BirdController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] LayerMask mask;
    [SerializeField] Transform wall;
    [SerializeField] WaitForSeconds wfs = new WaitForSeconds (0.07f);

    [SerializeField] float maxRange;
    [SerializeField] float minRange;
    Coroutine co;

    [SerializeField] public ActionBasedController input;

    [SerializeField] InputActionReference fbRef;
    [SerializeField] InputActionAsset fbAsset;

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

      
    }

    private void OnEnable()
    {
        fbAsset.Enable();
        fbRef.action.performed += FrontBack;
        fbRef.action.canceled += FrontBackCancle;
    }

    private void OnDisable()
    {
        fbAsset.Disable();
        fbRef.action.performed -= FrontBack;
        fbRef.action.canceled -= FrontBackCancle;
    }

    void FrontBack(InputAction.CallbackContext value)
    {
       
          
            if (value.ReadValue<Vector2>().y > 0.1f)
            {
                StopAllCoroutines();
                co = StartCoroutine(BackCoroutine());


            }
            else if (value.ReadValue<Vector2>().y < -0.1f)
            {
                StopAllCoroutines();
                co = StartCoroutine(FrontCoroutine());
            }

        

       
    }
    void FrontBackCancle(InputAction.CallbackContext value)
    {
        StopAllCoroutines();
    }

  
    IEnumerator FrontCoroutine()
    {
        while (true)
        {
            if(wall.position.z < maxRange )
            wall.position = wall.position + Vector3.forward;
            yield return wfs;
           
        }
    }

    IEnumerator BackCoroutine()
    {
        while (true)
        {
            if (wall.position.z > minRange)
                wall.position = wall.position - Vector3.forward;
            yield return wfs;
            
        }
    }
}
