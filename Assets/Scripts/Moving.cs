using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPuzzles : MonoBehaviour
{
    public GameObject form;              
    public GameObject Manager;

    private bool move;
    private bool finish;
    private Rigidbody2D rb;
    private Vector2 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    void OnMouseDown()
    {
        if (finish) return;
        move = true;
    }

    void OnMouseUp()
    {
        move = false;

        if (Vector2.Distance(rb.position, form.transform.position) <= 0.5f)
        {
            rb.MovePosition(form.transform.position);
            finish = true;
            GetComponent<AudioSource>().Play();
            Manager.GetComponent<PuzzleManager>().PuzzleCompleted();
        }
        else
        {
            StartCoroutine(SmoothReturn());
        }
    }

    private void Update()
    {
        if (move && !finish)
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(mouseWorld);
        }
    }

    private IEnumerator SmoothReturn()
    {
        var elapsed = 0f;
        var duration = 0.3f;
        var current = rb.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            rb.MovePosition(Vector2.Lerp(current, startPosition, elapsed / duration));
            yield return null;
        }

        rb.MovePosition(startPosition);
    }
}