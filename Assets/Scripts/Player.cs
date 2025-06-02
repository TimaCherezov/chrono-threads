using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    protected Vector2 lastDirection;
    private SpriteRenderer sr;
    [SerializeField] private AudioSource movementAudioSource;
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioClip movementClip;
    public bool IsAllowedMove { get; set; } = true;
    [SerializeField] public GameObject heroTarget;
    public float attackCooldown = 1.0f;
    private float lastAttackTime = 0f;

    // [SerializeField] private AudioSource audioSourc;
    [SerializeField] private AudioClip attackSound;

    protected bool isAttacking = false;

    public float minScale = 0.6f;
    public float maxScale = 1.2f;
    public float minY = -3.5f;
    public float maxY = 3.5f;

    private Collider2D collider2D;
    private Vector2 originalColliderSize;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
        originalColliderSize = (collider2D as CapsuleCollider2D)?.size ?? Vector2.one;
    }

    // private void Awake()
    // {
    //     audioSource2 = GetComponent<AudioSource>();
    // }

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

        UpdatePlayerScale();
    }

    private void UpdatePlayerScale()
    {
        var normalizedY = Mathf.InverseLerp(minY, maxY, transform.position.y);
        var scale = Mathf.Lerp(maxScale, minScale, normalizedY);
        transform.localScale = new Vector3(scale, scale, 1f);
        if (collider2D != null)
        {
            if (collider2D is CapsuleCollider2D capsuleCollider2D)
                capsuleCollider2D.size = originalColliderSize * scale;
        }
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
        if (direction.x != 0)
            sr.flipX = direction.x < 0;
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

        var angle = Vector2.Angle(facingDirection, directionToTarget);
        return angle <= 45f;
    }

    protected GameObject FindTargetAttackRange(float attackTange)
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, attackTange);
        var cloestDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach (var hit in hits)
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

    protected bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }

    protected void Attack()
    {
        lastAttackTime = Time.time;
        attackAudioSource.PlayOneShot(attackSound);
        SetAttacking(true);
        Invoke("ResetAttack", 0.5f);
        var target = FindTargetAttackRange(2f);
        if (target != null)
        {
            var heroHealth = target.GetComponentInParent<HeroHealth>();
            if (heroHealth != null && target.tag == "Boss" && IsFacingTarget(target))
            {
                Debug.Log("����� ������� �����!");
                heroHealth.ApplyDamage(-5);
            }
        }
    }

    private void ResetAttack()
    {
        SetAttacking(false);
    }
}