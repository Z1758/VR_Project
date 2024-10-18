using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject prefab;

    // x 15 y 3 z 10

    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            Vector3 vec = transform.position;
            vec.x += Random.Range(-15, 15);
            vec.y += Random.Range(-3, 3);
            vec.z += Random.Range(-10, 10);
            Instantiate(prefab, vec, Quaternion.identity);
        }
    }
}
