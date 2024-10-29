using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;

    public bool grounded;
    public float groundedOffset = -0.14f;
    public float groundedRadius = 0.28f;
    public LayerMask groundLayer;

    int speedAnim = Animator.StringToHash("Speed");
    int groundedAnim = Animator.StringToHash("Grounded");
    int jumpAnim = Animator.StringToHash("Jump");
    int fallAnim = Animator.StringToHash("Fall");

    Animator anim;
    Rigidbody rb;
    PlayerInput input;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Jump();
        GroundedCheck();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(input.move.x, 0f, input.move.y).normalized;
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (grounded)
        {
            anim.SetBool(jumpAnim, false);

            if (input.jump)
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                anim.SetBool(jumpAnim, true);
            }
        }
    }

    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
        anim.SetBool(groundedAnim, grounded);
    }
}