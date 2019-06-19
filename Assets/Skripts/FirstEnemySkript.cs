using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemySkript : EnemySkript
{

    public Transform target;
    public float chaseRadius = 5;
    public float attackRadius = 3;
    public Transform homePosition;
    public GameObject enemyProjectilePrefab;
    public float firstAttackSpeed = 1.0f;
    public float shootingCooldown = 1;
    public bool shootingNotOnCooldown = true;


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
        if(shootingNotOnCooldown)
        {
            shootingNotOnCooldown = false;
            StartCoroutine(ProcessShootingCooldown());

            if(Vector3.Distance(target.position, transform.position) < attackRadius)
            {
                Debug.Log("Enemy shooting!");
                Vector2 shootingDirection = -(transform.position-target.position);
                shootingDirection.Normalize();
                //Debug.Log("shootingDirection: "+shootingDirection);

                // fireball nur das optische, logik in fireballScript
                GameObject projectileObject = Instantiate(enemyProjectilePrefab, transform.position,  new Quaternion(transform.rotation.x, transform.rotation.y , 0, 1));  // Quaternion.identity means keep orientation/rotation
                
                // logic des projectileObject (Damage, hitdetection etc.) ist in EnemyProjectileSkript
                EnemyProjectileSkript fireballScript = projectileObject.GetComponent<EnemyProjectileSkript>();
                fireballScript.velocity = shootingDirection * firstAttackSpeed;
                fireballScript.enemyObject = gameObject; // gameObject is build in; in this case is the enemy

                projectileObject.GetComponent<EnemyProjectileSkript>().velocity = shootingDirection * firstAttackSpeed;
                projectileObject.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                projectileObject.transform.Translate(1, 0, 0);
                Destroy(projectileObject, 5.0f);
            }
        }
        
    }

    private IEnumerator ProcessShootingCooldown() // Coroutine läuft parallel ab
    {
        yield return new WaitForSeconds(shootingCooldown); //wait animation time
        shootingNotOnCooldown = true;
    }
}