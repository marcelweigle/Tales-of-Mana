using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSkript : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public Vector2 offset = new Vector2(0.0f, 0.0f);
    public float damage = 1;
    private bool playerAlreadyHit = true;
    public Animator animator;
    public GameObject enemyObject;


    void Update()
    {
        

        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        //Debug.Log("currentPosition: "+currentPosition+" newPosition: "+newPosition);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log("foreach RaycastHit2D wird durchlaufen");
            GameObject other = hit.collider.gameObject;
            if (other != enemyObject) {
                Debug.Log("other != enemyObject is true");
                if(other.CompareTag("Walls"))
                {
                    Destroy(gameObject);
                    break;
                }
                if(other.CompareTag("Player"))
                {
                    Debug.Log("Enemy hit a Player with Raycast!");
                    // to remove bug multiple hit with Collider of same Enemy (and multiple TakeDamage)
                    if(playerAlreadyHit)
                    {
                        playerAlreadyHit = false;
                        //Debug.Log("Enemy hit detected by RayCast!");
                        StartCoroutine(PlayHitAnimation());
                        other.GetComponent<PlayerController>().TakeDamage(damage);
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
