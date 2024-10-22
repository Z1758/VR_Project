using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private static SpawnEnemy instance;




    public static SpawnEnemy Instance
    {
        get
        {
            return instance;

        }
    }


    public GameObject[] prefab;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    Queue<GameObject> enemyQueue;
    [SerializeField] int enemyCount;
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

       audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }
    void Start()
    {
        enemyQueue = new Queue<GameObject>();
        SetEnemy();
        StartCoroutine(Spawn());
    }



    void SetEnemy()
    {
        int ran;
        for (int i = 0; i < enemyCount; i++)
        {
            ran = Random.Range(0, prefab.Length);
            GameObject enemy = Instantiate(prefab[ran], Vector3.zero, Quaternion.identity);
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
                vec.x += Random.Range(-12, 12);
                vec.y += Random.Range(-3, 3);
                vec.z += Random.Range(-7, 7);
                enemy.transform.position = vec;
                enemy.SetActive(true);
            }

        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        audioSource.PlayOneShot(clip);
        enemyQueue.Enqueue(enemy);
        enemy.SetActive(false);
    }
}
