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

    [Space]
    [Header("References:")]  
    public Rigidbody2D rb;
    public Animator animator;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
    }


    void ProcessInputs(){
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp( movementDirection.magnitude, 0.0f, 1.0f);  // first value to clamp, min, max : Clamp to handle different Controller Input to behave the same
        movementDirection.Normalize(); // to dont change the speed of archer when using movementDirection
    }

    void Move(){
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate(){
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
    }
}
