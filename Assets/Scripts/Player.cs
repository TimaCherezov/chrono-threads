using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 lastDirection;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera cam;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        var direction = GetDirection().normalized;
        rb.linearVelocity = direction * moveSpeed;
        
        if (lastDirection != direction)
            Animation(direction);
        cam.transform.position = transform.position + new Vector3(0, 0, cam.transform.position.z);
        lastDirection = direction;
    }

    protected virtual Vector2 GetDirection()
    {
        var moveX = 0f;
        var moveY = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        return new Vector2(moveX, moveY);
    }

    protected virtual void Animation(Vector2 direction)
    {
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        anim.SetBool("IsMoving", direction != Vector2.zero);

        sr.flipX = lastDirection.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => sr.flipX
        };
    }
}