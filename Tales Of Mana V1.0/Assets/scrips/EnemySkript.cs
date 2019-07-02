using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkript : MonoBehaviour
{

    private float health = 2;
    public string enemyName;
    public int baseAttack = 1;
    public float moveSpeed = 3;
    public FloatValue maxHealth;
    public Animator animator;


    void Awake()
    {
        this.gameObject.SetActive(true);
        health = maxHealth.initialValue;
        Debug.Log("Awake, Health: " + health);
    }

    public void TakeDamage(float damage)
    {
        
        //Debug.Log("Damage, Health: " + health);

        health = health - damage;

        Debug.Log("Enemy Skript Damage: " + damage);
        Debug.Log("Enemy Skript Damage dealt! Health left: " + health);

        if(health <= 0)
        {
            StartCoroutine(PlayDeathAnimation());
            //Debug.Log("enemy killed");
        }
    }

    private IEnumerator PlayDeathAnimation() // Coroutine läuft parallel ab
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(1); //wait animation time
        this.gameObject.SetActive(false);
    }

    
}
