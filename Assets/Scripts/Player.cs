using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    protected Vector2 lastDirection;
    private SpriteRenderer sr;
    [FormerlySerializedAs("audioSource")] [SerializeField] private AudioSource movementAudioSource;
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
    }

    private void FixedUpdate()
    {
        var direction = GetDirection().normalized;
        if (!IsAllowedMove)
            direction = Vector2.zero;
        rb.linearVelocity = direction * moveSpeed;

        if (lastDirection != direction)
            Animation(direction);

        if (direction != Vector2.zero && !movementAudioSource.isPlaying)
        {
            movementAudioSource.clip = movementClip;
            movementAudioSource.loop = true;
            movementAudioSource.Play();
        }
        else if (direction == Vector2.zero && movementAudioSource.isPlaying && movementAudioSource.clip == movementClip)
        {
            movementAudioSource.Stop();
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

    protected bool IsFacingTarget(GameObject target)
    {
        if (target == null) return false;

        var facingDirection = lastDirection.normalized;
        if (facingDirection == Vector2.zero)
            facingDirection = sr.flipX ? Vector2.left : Vector2.right;

        var directionToTarget = (target.transform.position - transform.position).normalized;

        var dot = Vector2.Dot(facingDirection, directionToTarget);
        return dot >= 0f;
    }

    protected GameObject FindTargetAttackRange(float attackTange)
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, attackTange);
        var cloestDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach ( var hit in hits )
        {
            if (hit.gameObject == gameObject || hit.gameObject.tag == "Player") continue;
            var health = hit.GetComponent<HeroHealth>();
            if (health != null)
            {
                var distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < cloestDistance)
                {
                    cloestDistance = distance;
                    closestTarget = hit.gameObject;
                }
            }   
        }
        return closestTarget;
    }
}