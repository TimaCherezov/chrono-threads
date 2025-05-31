using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    protected Vector2 lastDirection;
    private SpriteRenderer sr;
    [SerializeField] private AudioSource movementAudioSource;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioClip movementClip;
    public bool IsAllowedMove { get; set; } = true;
    [SerializeField] public GameObject heroTarget;

    protected bool isAttacking;

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
        return dot > 0.3f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsFacingTarget(other.gameObject))
        {

        }
        if (other.gameObject.name != "Boss")
            return;

        var heroHealth = other.GetComponentInParent<HeroHealth>();
        if (heroHealth != null)
        {
            Debug.Log($"{gameObject.name} ������� {other.gameObject.name}");
            heroHealth.ApplyDamage(-1);
        }
        else
        {
            Debug.LogWarning("��������� HeroHealth �� ������ � ������������ �������� " + other.gameObject.name);
        }
    }
}