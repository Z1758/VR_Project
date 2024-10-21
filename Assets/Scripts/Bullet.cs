using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    Transform target;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + (Vector3.up*2), speed * Time.deltaTime);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.HP--;
        Destroy(gameObject);
    }
}
