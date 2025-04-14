using UnityEngine;

public class Dron : MonoBehaviour
{
    private float sideLength = 3f;
    private float delta = 1f;
    private float speed = 1f;
    private Vector2[] waypoints;
    private int currentWaypoint;
    [SerializeField] private GameObject bulletPrefab;

    void Start()
    {
        Vector2 startPos = transform.position;
        waypoints = new[]
        {
            startPos,
            startPos + new Vector2(sideLength, 0),
            startPos + new Vector2(sideLength + delta, -sideLength),
            startPos + new Vector2(-delta, -sideLength)
        };
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint],
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, waypoints[currentWaypoint]) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "FutureHero") return;
        var distance = Vector2.Distance(transform.position, other.transform.position);
        if (distance <= 2f)
        {
            AttackHero(other.gameObject);
        }
    }

    void AttackHero(GameObject hero)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().hero = hero;
        bullet.transform.position = transform.position;
        Debug.Log("Дрон атакует FutureHero через триггер!");
    }
}