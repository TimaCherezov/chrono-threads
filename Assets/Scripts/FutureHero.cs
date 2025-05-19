using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FutureHero : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera cam;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Vector2 lastDirection = Vector2.down;
    [SerializeField] private int health = 10;
    public bool IsMoving;
    private AudioSource audioSource; 
    [SerializeField] private AudioClip movementClip; 
    [SerializeField]public GameObject heroTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        var inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow)) inputVector.y = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) inputVector.y = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) inputVector.x = 1f;
        if (Input.GetKey(KeyCode.LeftArrow)) inputVector.x = -1f;

        inputVector = inputVector.normalized;

        if (inputVector != Vector2.zero)
        {
            IsMoving = true;
            lastDirection = inputVector;

            if (!audioSource.isPlaying )
            {
                audioSource.clip = movementClip;
                audioSource.loop = true; 
                audioSource.Play();
            }
        }
        else
        {
            IsMoving = false;

            if (audioSource.isPlaying && audioSource.clip == movementClip)
            {
                audioSource.Stop();
            }
        }

        anim.SetFloat("MoveX", inputVector.x);
        anim.SetFloat("MoveY", inputVector.y);
        anim.SetBool("IsMoving", inputVector != Vector2.zero);


        sr.flipX = lastDirection.x < 0;


        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
        cam.transform.position = transform.position + new Vector3(0, 0, cam.transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Future target has taken damage");
    }
}