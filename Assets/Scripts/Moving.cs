using UnityEngine;

public class MovingPuzzles : MonoBehaviour
{
    public GameObject form;
    public GameObject Manager;

    private bool move;
    private bool finish;
    private Rigidbody2D rb;
    private Vector2 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        if (finish) return;
        move = true;
        Debug.Log("Mouse Down2");
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        move = false;

        if (Mathf.Abs(rb.position.x - form.transform.position.x) <= 0.5f &&
            Mathf.Abs(rb.position.y - form.transform.position.y) <= 0.5f)
        {
            rb.position = new Vector2(form.transform.position.x, form.transform.position.y);
            finish = true;
            GetComponent<AudioSource>().Play();
            Manager.GetComponent<PuzzleManager>().PuzzleCompleted();
        }
        else
        {
            rb.position = startPosition;
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