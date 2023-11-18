using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtoB : MonoBehaviour
{
    public Transform target; // Reference to the empty object
    public float speed = 5.0f;// Force applied to the object
    private Animator animator; // Reference to the Animator component
    private bool isMoving = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found. Please attach an Animator component to the object.");
        }
       // animator.SetBool("IsMoving", true);
    }


    private void Update()
    {
        if (target != null)
        {
            // Move the object towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Check if the object has reached the target
            if (Vector3.Distance(transform.position, target.position) < 0.1f && isMoving)
            {
                //isMoving = false;
                animator.SetBool("IsMoving", true);
            }
            
        }

    }
}
