using UnityEngine;

public class PastHero : Player
{
    public bool IsMoving;
    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected override Vector2 GetDirection()
    {
        var moveX = 0f;
        var moveY = 0f;
        if (Input.GetKey(KeyCode.W))
            moveY = 1f;
        if (Input.GetKey(KeyCode.S))
            moveY = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        IsMoving = moveX == 0 || moveY == 0;
        return new Vector2(moveX, moveY);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    private void Attack()
    {
        audioSource.PlayOneShot(attackSound);
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