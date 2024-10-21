using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject prefab;

    Queue<GameObject> enemyQueue;
    [SerializeField] int enemyCount;

    void Start()
    {
        enemyQueue = new Queue<GameObject>();
        SetEnemy();
        StartCoroutine(Spawn());
    }

  

    void SetEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
          GameObject enemy =  Instantiate(prefab, Vector3.zero, Quaternion.identity);
            enemy.SetActive(false);
            enemyQueue.Enqueue(enemy);
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            if (enemyQueue.Count > 0)
            {
                GameObject enemy = enemyQueue.Dequeue();
                Vector3 vec = transform.position;
                vec.x += Random.Range(-15, 15);
                vec.y += Random.Range(-3, 3);
                vec.z += Random.Range(-10, 10);
                enemy.transform.position = vec;
                enemy.SetActive(true);
            }
            
        }
    }
}
