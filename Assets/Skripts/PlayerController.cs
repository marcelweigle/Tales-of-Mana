﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0f; // to set speed in editor
    public float FIREBALL_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character statistics:")]  
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool endOfAiming;
    public VectorValue startingPosition;
    public int isMagician;
    public Vector2 lastMovementDirection;

    [Space]
    [Header("References:")]  
    public Rigidbody2D rb;
    public Animator animator;

    [Space]
    [Header("Prefabs:")]  
    public GameObject fireballPrefab;

    
    

 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
        isMagician = PlayerPrefs.GetInt("isMagician"); 
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();

        if (Input.GetButtonDown("Fire1") && (isMagician == 1))
        {
            StartCoroutine(AttackCo());
        }

        if(isMagician == 1)
        {
            animator.SetInteger("isMagician", 1);
        }

        Shoot();
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

        // save InputDirection for movement only if currently moving(either horizontal or vertical equals 1), cause its 0 else
        // used for spawning fireball
        if((Input.GetAxisRaw("Horizontal") != 0) | (Input.GetAxisRaw("Vertical") != 0) ){
            lastMovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            lastMovementDirection.Normalize();
        }
        
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
        //Vector2 shootingDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = lastMovementDirection;
        shootingDirection.Normalize();

        if(endOfAiming && isMagician==1){
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);  // Quaternion.identity means keep orientation/rotation
            
            Fireball fireballScript = fireball.GetComponent<Fireball>();
            fireballScript.velocity = shootingDirection * FIREBALL_BASE_SPEED;
            fireballScript.wizard = gameObject; // gameObject is build in; in this case is the wizard/main character
            fireball.GetComponent<Fireball>().velocity = shootingDirection * FIREBALL_BASE_SPEED;
            fireball.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            fireball.transform.Translate(1, 0, 0);
            Destroy(fireball, 2.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MagicBook"))
        {
            isMagician = 1;
            PlayerPrefs.SetInt("isMagician", 1);
        }
    }
}
