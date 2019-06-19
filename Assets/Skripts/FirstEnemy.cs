using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : Enemy
{

    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public GameObject firstAttack;
    public float firstAttackSpeed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        CheckDistance();
        Attack();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); // Time.deltaTime is time since last frame
        }
    }

    void Attack()
    {
        if(Vector3.Distance(target.position, transform.position) < attackRadius)
        {
            Vector2 shootingDirection = transform.rotation;
            shootingDirection.Normalize();

            // fireball nur das optische, logik in fireballScript
            GameObject fireball = Instantiate(firstAttack, transform.position, Quaternion.identity);  // Quaternion.identity means keep orientation/rotation
            
            // logic des Fireballs (Damage, hitdetection etc.) ist in fireballScript
            Fireball fireballScript = fireball.GetComponent<Fireball>();
            fireballScript.velocity = shootingDirection * firstAttackSpeed;
            fireballScript.wizard = gameObject; // gameObject is build in; in this case is the enemy

            fireball.GetComponent<Fireball>().velocity = shootingDirection * firstAttackSpeed;
            fireball.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            fireball.transform.Translate(1, 0, 0);
            Destroy(fireball, 2.0f);
        }
    }
}