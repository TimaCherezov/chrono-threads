using UnityEngine;

public class Dron : MonoBehaviour
{
    private float sideLength = 3f;
    private float delta = 1f;
    private float speed = 1f;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint;
    [SerializeField] private GameObject bulletPrefab;
    private bool isRotating;
    private int rotatingTimes;
    private const float RotationSpeed = 90f; // review: константы обычно на самом верху класса перечисляются
    private const float RangeAction = 2f;

    void FixedUpdate()
    {
        if (isRotating)
        {
            RotateTowardsCurrentWaypoint();
            return; 
        }

        MoveTowardsCurrentWaypoint();
    }

    private void RotateTowardsCurrentWaypoint()
    {
        var direction = waypoints[currentWaypoint].transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
        {
            isRotating = false; 
        }
    }

    private void MoveTowardsCurrentWaypoint()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isRotating = true; 
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // review: лучше сравнивать через CompareTag/CompareHash или вообще по ссылке
        if (other.gameObject.name != "FutureHero") return;
        var distance = Vector2.Distance(transform.position, other.transform.position);
        if (distance <= RangeAction)
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