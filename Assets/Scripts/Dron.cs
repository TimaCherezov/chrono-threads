using System.Collections;
using UnityEngine;

public class Dron : MonoBehaviour
{
    private float sideLength = 3f;
    private float delta = 1f;
    private float speed = 1f;
    private Vector2[] waypoints;
    private int currentWaypoint;
    [SerializeField] private GameObject bulletPrefab;
    public float attackCooldown = 2f; // Время между атаками
    private float nextAttackTime;

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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name != "FutureHero") return;
        var distance = Vector2.Distance(transform.position, other.transform.position);
        if (distance <= 14f && Time.time >= nextAttackTime)
        {
            AttackHero(other.gameObject);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void AttackHero(GameObject hero)
    {
        var heroMovement = hero.GetComponent<FutureHero>();
        if (heroMovement.isMoving)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().hero = hero;
            bullet.transform.position = transform.position;
            Debug.Log("Дрон атакует FutureHero через триггер!");
        }
    }
}