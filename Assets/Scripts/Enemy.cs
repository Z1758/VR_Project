using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] private float maxHp;

    public float MAXHP { get { return maxHp; } }

    [SerializeField] private float hp;

    public float HP { get { return hp; } }

    [SerializeField] float range;
    [SerializeField] float speed;
    Transform target;

    [SerializeField] float defaultCoolDown;
    [SerializeField] float coolDown;

    // Start is called before the first frame update
    void Start()
    {
      target =  GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (range > transform.position.z)
        {

            Move();
        }
        else
        {
            Atk();
        }
    }


    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
    }

    void Atk()
    {
        
        coolDown-= Time.deltaTime;
        if (coolDown <= 0)
        {
            Instantiate(bullet, muzzle.position, muzzle.rotation);
            coolDown = defaultCoolDown;
        }
    }

    public void TakeDamage(int dmg)
    {
     

        hp-=dmg;

        if(hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
