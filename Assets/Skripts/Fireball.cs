using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject wizard;
    public Vector2 offset = new Vector2(0.0f, 0.0f);
    public float damage;
    private bool enemyHit = true;
    public Animator animator;


    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;
            if (other != wizard) {
                if(other.CompareTag("Walls"))
                {
                    Destroy(gameObject);
                    break;
                }
                if(other.CompareTag("Enemy"))
                {
                    // to remove bug multiple hit with Collider of same Enemy (and multiple TakeDamage)
                    if(enemyHit)
                    {
                        enemyHit = false;
                        Debug.Log("Enemy hit detected by RayCast!");
                        StartCoroutine(PlayHitAnimation());
                        other.GetComponent<FirstEnemy>().TakeDamage(damage);
                        break;
                    }
                    
                }
            }
        }

        transform.position = newPosition;
    }

    private IEnumerator PlayHitAnimation() // Coroutine läuft parallel ab
    {
        animator.SetBool("fireballHitSomething", true);
        velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(0.2f); //wait animation time
        Destroy(gameObject);
    }
}
