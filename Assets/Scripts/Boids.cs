using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

using UnityEngine.Jobs;



public class Boids : MonoBehaviour
{
    private List<Boids> cBoids;
    private List<Boids> sBoids;
    private List<Boids> aBoids;

    [SerializeField] int hp;

    [SerializeField] float fovAngle;

    [SerializeField] float speed;

    [SerializeField] LayerMask mask;

    Vector3 resultVector;

    public WaitForSeconds wfs;

    private void Awake()
    {

        cBoids = new List<Boids>();
        sBoids = new List<Boids>();
        aBoids = new List<Boids>();

        wfs = new WaitForSeconds(Random.Range(0.03f, 0.1f));
    }


    void CalDistance()
    {
        cBoids.Clear();
        sBoids.Clear();
        aBoids.Clear();
      

        
        for (int i = 0; i < SpawnBirds.Instance.allBoids.Count; i++)
        {
            Boids bird = SpawnBirds.Instance.allBoids[i];
            int cnt = 0;
            if (bird != this)
            {
             
                float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(bird.transform.position - transform.position);
                if (currentNeighbourDistanceSqr <= SpawnBirds.Instance.cDistance * SpawnBirds.Instance.cDistance)
                {
                    if (cBoids.Count < 5)
                    {

                        cBoids.Add(bird);
                    }
                    else
                    {
                        cnt++;
                    }
                }
                if (currentNeighbourDistanceSqr <= SpawnBirds.Instance.sDistance * SpawnBirds.Instance.sDistance)
                {
                    if (sBoids.Count < 5)
                    {

                        sBoids.Add(bird);
                    }
                    else
                    {
                        cnt++;
                    }
                }
                if (currentNeighbourDistanceSqr <= SpawnBirds.Instance.aDistance * SpawnBirds.Instance.aDistance)
                {
                    if (aBoids.Count < 5)
                    {

                        aBoids.Add(bird);
                    }
                    else
                    {
                        cnt++;
                    }
                }
            }
            if (cnt >= 3)
                break;
        }
     
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 cohesionVec = CalculateCohesionVector() * SpawnBirds.Instance.cWeight;
        Vector3 alignmentVec = CalculateAlignmentVector() * SpawnBirds.Instance.aWeight;
        Vector3 separationVec = CalculateSeparationVector() * SpawnBirds.Instance.sWeight;

        Vector3 boundsVec = CalculateBoundsVector() * SpawnBirds.Instance.bWeight;

        Vector3 obstacleVec = CalculateObstacleVector() * 1f;


        resultVector = cohesionVec + alignmentVec + separationVec + boundsVec + obstacleVec;

        resultVector = Vector3.Lerp(this.transform.forward, resultVector, Time.deltaTime);


        if (resultVector == Vector3.zero)
            resultVector = Random.insideUnitSphere * 1.2f;

        resultVector = resultVector.normalized;

        transform.rotation = Quaternion.LookRotation(resultVector);
        transform.position += resultVector * BirdStat.Instance.SPEED * Time.deltaTime;
    }

    private void Start()
    {

        StartCoroutine(Cal());
    }

    private IEnumerator Cal()
    {
        while (true)
        {
            yield return wfs;
            CalDistance();

        }
    }

    private bool FOV(Vector3 position)
    {
        return Vector3.Angle(transform.forward, position - transform.position) <= fovAngle;
    }

    private Vector3 CalculateCohesionVector()
    {
        Vector3 cohesionVector = Vector3.zero;
        if (cBoids.Count <= 0)
            return cohesionVector;

        int fov = 0;

        for (int i = 0; i < cBoids.Count; i++)
        {
            if (FOV(cBoids[i].transform.position))
            {
                fov++;
                cohesionVector += cBoids[i].transform.position;
            }
        }


        cohesionVector /= fov;
        cohesionVector -= transform.position;
        cohesionVector.Normalize();
        return cohesionVector;
    }

    private Vector3 CalculateAlignmentVector()
    {
        Vector3 alignmentVec = transform.forward;
        if (aBoids.Count <= 0)
            return alignmentVec;
        int fov = 0;
        for (int i = 0; i < aBoids.Count; i++)
        {
            if (FOV(aBoids[i].transform.position))
            {
                fov++;
                alignmentVec += aBoids[i].transform.forward;
            }
        }


        alignmentVec /= fov;
        alignmentVec.Normalize();
        return alignmentVec;
    }

    private Vector3 CalculateSeparationVector()
    {
        Vector3 separationVec = Vector3.zero;
        if (sBoids.Count <= 0)
            return separationVec;
        int fov = 0;

        for (int i = 0; i < sBoids.Count; i++)
        {
            if (FOV(sBoids[i].transform.position))
            {
                fov++;
                separationVec += (transform.position - sBoids[i].transform.position);
            }
        }

        separationVec /= fov;
        separationVec.Normalize();
        return separationVec;
    }

    private Vector3 CalculateBoundsVector()
    {
        Vector3 offsetToCenter = BirdTarget.Instance.transform.position - transform.position;
        if (offsetToCenter.magnitude >= SpawnBirds.Instance.bDistance)
        {
            return offsetToCenter.normalized;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 CalculateObstacleVector()
    {
        Vector3 obstacleVec = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, mask))
        {

            Debug.DrawLine(transform.position, hit.point, Color.black);
            obstacleVec = hit.normal;
        }
        return obstacleVec;
    }

    public void TakeDamage()
    {
        hp--;

        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
      
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(1);
        }
    }


}
