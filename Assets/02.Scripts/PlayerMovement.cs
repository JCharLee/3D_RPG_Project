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

    // 애니메이션 ID
    int speedAnim = Animator.StringToHash("Speed");
    int groundedAnim = Animator.StringToHash("Grounded");
    int jumpAnim = Animator.StringToHash("Jump");
    int fallAnim = Animator.StringToHash("Fall");

    // 컴포넌트
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        GroundedCheck();
    }

    void Move()
    {
        
    }

    void Jump()
    {
        if (grounded)
        {
            anim.SetBool(jumpAnim, false);
        }
    }

    void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
        anim.SetBool(groundedAnim, grounded);
    }
}