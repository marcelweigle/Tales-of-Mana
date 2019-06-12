using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0f; // to set speed in editor


    [Space]
    [Header("Character statistics:")]  
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool endOfAiming;

    [Space]
    [Header("References:")]  
    public Rigidbody2D rb;
    public Animator animator;

    public bool isMagician;
    public VectorValue startingPosition;

 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();

        if (Input.GetButtonDown("Fire1") && isMagician)
        {
            StartCoroutine(AttackCo());
        }
        //Shoot();
    }

    private IEnumerator AttackCo() // Coroutine läuft parallel ab
    {
        animator.SetBool("Attacking", true);
        yield return null; //wait 1 frame
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.33f); //wait animation time
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementSpeed = Mathf.Clamp( movementDirection.magnitude, 0.0f, 1.0f);  // first value to clamp, min, max : Clamp to handle different Controller Input to behave the same
        movementDirection.Normalize(); // to dont change the speed of archer when using movementDirection

        endOfAiming = Input.GetButtonUp("Fire1");

    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate()
    {
        if(movementDirection != Vector2.zero) // that the character doesnt turn to front when stop moving
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        
        animator.SetFloat("Speed", movementSpeed);
    }

    void Shoot()
    {
        Vector2 shootingDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = shootingDirection;
        shootingDirection.Normalize();

    }
}
