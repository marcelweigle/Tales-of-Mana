using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float health = 2;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
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
        
        Debug.Log("Damage, Health: " + health);

        health = health - damage;

        Debug.Log("Damage: " + damage);
        Debug.Log("Damage dealt! Health left: " + health);

        if(health <= 0)
        {
            StartCoroutine(PlayDeathAnimation());
            Debug.Log("enemy killed");
        }
    }

    private IEnumerator PlayDeathAnimation() // Coroutine läuft parallel ab
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(1); //wait animation time
        this.gameObject.SetActive(false);
    }

    
}
