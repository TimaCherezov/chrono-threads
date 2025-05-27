using UnityEngine;

public class Moving : MonoBehaviour
{
    public GameObject form;

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
            PuzzleManager.Instance.PuzzleCompleted();
            GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        if (move && !finish)
        {
            Vector2 mouseScreen = Input.mousePosition;
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            rb.MovePosition(mouseWorld);
        }
    }
}