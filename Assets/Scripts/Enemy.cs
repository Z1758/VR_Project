using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyStatus enemyStatus;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] Animator animator;

    [SerializeField] private float hp;


    Transform target;

    [SerializeField] float coolDown;

    bool animFlag;

    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();
      target =  GameObject.FindGameObjectWithTag("Player").transform;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyStatus.range > transform.position.z)
        {

            Move();
        }
        else
        {
            Atk();
        }
    }

    private void OnEnable()
    {
        hp = enemyStatus.hp;
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, enemyStatus.speed * Time.deltaTime);
      
    }

    void Atk()
    {
        
        coolDown-= Time.deltaTime;
        if (coolDown <= 0)
        {
            Instantiate(bullet, muzzle.position, muzzle.rotation);
            coolDown = enemyStatus.cooltime;
        }
    }

    public void TakeDamage(int dmg)
    {
     
        if (hp <= 0)
            return;

        hp-=dmg;

        if (!animFlag && hp < enemyStatus.hp * 0.5f)
        {
            animator.SetTrigger("Hit");
            animFlag = true;

        }

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(enemyStatus.effect,transform.position, Quaternion.identity);
        Player.Instance.PlusCount();
        animFlag = false;
        BirdTarget.Instance.SetTarget();

        SpawnEnemy.Instance.ReturnEnemy(gameObject);
    }
}
