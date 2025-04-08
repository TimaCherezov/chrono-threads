using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastHero : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera cam;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        var inputVector = new Vector2(0, 0);
        var isJumping = false;

        if (Input.GetKey(KeyCode.W)) inputVector.y = 1f;
        if (Input.GetKey(KeyCode.S)) inputVector.y = -1f;
        if (Input.GetKey(KeyCode.D)) inputVector.x = 1f;
        if (Input.GetKey(KeyCode.A)) inputVector.x = -1f;
        if (Input.GetKey(KeyCode.Space)) isJumping = true;

        inputVector = inputVector.normalized;

        if (inputVector != Vector2.zero)
        {
            lastDirection = inputVector; // ����� ��� �������� ������ ��� ������� ������� ������
        }

        anim.SetFloat("MoveX", inputVector.x);
        anim.SetFloat("MoveY", inputVector.y);
        anim.SetBool("IsMoving", inputVector != Vector2.zero);
        anim.SetBool("IsJumping", isJumping);

        switch (lastDirection.x)
        {
            case < 0:
                sr.flipX = true;
                break;
            case > 0:
                sr.flipX = false;
                break;
        }


        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
        cam.transform.position = transform.position + new Vector3(0,0,cam.transform.position.z);
    }
}
