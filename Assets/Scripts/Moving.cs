using UnityEngine;

public class Moving : MonoBehaviour
{
    public GameObject form;
    public GameObject Manager;

    private bool move;
    private bool finish;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        if (finish) return;
        move = true;
    }

    void OnMouseUp()
    {
        move = false;

        if (Mathf.Abs(rb.position.x - form.transform.position.x) <= 0.5f &&
            Mathf.Abs(rb.position.y - form.transform.position.y) <= 0.5f)
        {
            rb.position = new Vector2(form.transform.position.x, form.transform.position.y);
            finish = true;
            GetComponent<AudioSource>().Play();
            Manager.GetComponent<PuzzleManager>().PuzzleCompleted();
        }
    }

    void Update()
    {
        if (move && !finish)
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(mouseWorld);
        }
    }
}