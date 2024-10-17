using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirds : MonoBehaviour
{

    private static SpawnBirds instance;




    public static SpawnBirds Instance
    {
        get
        {
            return  instance;

        }
    }



    public GameObject[] prefab;
    public float spawnRadius = 7;
    public int spawnCount;

    public List<Boids> allBoids;

    [SerializeField] AnimationInstancing.AnimationInstancing[] proto;

    [Range(0f, 10f)]
    [SerializeField] public float cDistance;

    [Range(0f, 10f)]
    [SerializeField] public float sDistance;

    [Range(0f, 10f)]
    [SerializeField] public float aDistance;

    [Range(0f, 10f)]
    [SerializeField] public float bDistance;

    [Range(0f, 10f)]
    [SerializeField] public float cWeight;
    [Range(0f, 10f)]
    [SerializeField] public float sWeight;
    [Range(0f, 10f)]
    [SerializeField] public float aWeight ;
    [Range(0f, 10f)]
    [SerializeField] public float bWeight;



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

        allBoids = new List<Boids>();

        int ran = 0;

        for (int i = 0; i < spawnCount; i++)
        {
            ran = Random.Range(0, prefab.Length);
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            GameObject bird = Instantiate(prefab[ran]);
            Boids boid = bird.GetComponent<Boids>();
            if (bird.TryGetComponent(out Bird b))
            {
                b.ani.prototype = proto[ran].prototype;
                b.ani.playSpeed = Random.Range(0.5f, 1.5f);
            }

         


            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;
            allBoids.Add(boid);
        }
    }

}
