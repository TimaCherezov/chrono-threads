using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastHero : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera cam;
    public bool IsAllowedMove { get; set; } = true;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!IsAllowedMove) return;
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
            lastDirection = inputVector; // нужен для поворота игрока при нажатии боковых кнопок
        }

        anim.SetFloat("MoveX", inputVector.x);
        anim.SetFloat("MoveY", inputVector.y);
        anim.SetBool("IsMoving", inputVector != Vector2.zero);
        anim.SetBool("IsJumping", isJumping);

        var sr = GetComponent<SpriteRenderer>(); // для поворота персонажа в стороны

        if (lastDirection.x < 0)
            sr.flipX = true;
        else if (lastDirection.x > 0)
            sr.flipX = false;


        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
        cam.transform.position = transform.position + new Vector3(0, 0, cam.transform.position.z);
    }

    public void DisableAnimations()
    {
        anim.SetBool("IsMoving", false);
    }

}