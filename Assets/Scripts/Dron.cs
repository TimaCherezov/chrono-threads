using UnityEngine;

public class Dron : MonoBehaviour
{
    private float sideLength = 3f;
    private float delta = 1f;
    private float speed = 1f;
    // private Vector2[] waypoints;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint;
    [SerializeField] private GameObject bulletPrefab;
    private bool isRotating = false;
    private int rotatingTimes = 0;

    void FixedUpdate()
    {
        if (isRotating)
        {
            Vector2 direction = waypoints[currentWaypoint].transform.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            rotatingTimes++;
            if (rotatingTimes >= 100)
                isRotating = false;
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            rotatingTimes = 0;
            isRotating = true;
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
        bullet.GetComponent<Bullet>().target = hero.GetComponent<FutureHero>().heroTarget;
        bullet.transform.position = transform.position;
        Debug.Log("Дрон атакует FutureHero через триггер!");
    }
}