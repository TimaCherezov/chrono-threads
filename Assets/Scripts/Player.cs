using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 lastDirection;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioClip movementClip;
    public bool IsAllowedMove { get; set; } = true;
    [SerializeField] public GameObject heroTarget;

    protected bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        var direction = GetDirection().normalized;
        if (!IsAllowedMove)
            direction = Vector2.zero;
        rb.linearVelocity = direction * moveSpeed;

        if (lastDirection != direction)
            Animation(direction);

        if (direction != Vector2.zero && !audioSource.isPlaying)
        {
            audioSource.clip = movementClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (direction == Vector2.zero && audioSource.isPlaying && audioSource.clip == movementClip)
        {
            audioSource.Stop();
        }

        cam.transform.position = transform.position + new Vector3(0, 0, cam.transform.position.z);
        lastDirection = direction;
    }

    protected virtual Vector2 GetDirection()
    {
        throw new NotImplementedException();
    }

    protected virtual void Animation(Vector2 direction)
    {
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
        anim.SetBool("IsMoving", direction != Vector2.zero);
        anim.SetBool("IsAttacking", isAttacking);

        sr.flipX = lastDirection.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => sr.flipX
        };
    }

    protected void SetAttacking(bool state)
    {
        isAttacking = state;
        Animation(lastDirection); 
    }
}