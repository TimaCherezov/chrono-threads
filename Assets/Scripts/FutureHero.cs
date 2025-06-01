using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureHero : Player
{
    [SerializeField] private AudioClip attackSound;
    public bool IsMoving;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override Vector2 GetDirection()
    {
        var moveX = 0f;
        var moveY = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
            moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            moveY = -1f;
        if (Input.GetKey(KeyCode.RightArrow))
            moveX = 1f;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveX = -1f;
        IsMoving = moveX != 0 || moveY != 0;
        return new Vector2(moveX, moveY);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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
                Debug.Log("Игрок атакует босса!");
                heroHealth.ApplyDamage(-5);
            }
        }
    }

    private void ResetAttack()
    {
        SetAttacking(false);
    }
}