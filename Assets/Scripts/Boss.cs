using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed = 1f;

    private int currentWaypoint;

    // private bool isRotating;
    // private int rotatingTimes;
    // private const float RotationSpeed = 90f;
    [SerializeField] private float RangeAction = 2f;

    [SerializeField] private GameObject target;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // if (isRotating)
        // {
        //     RotateTowardsCurrentWaypoint();
        //     return;
        // }

        MoveTowardsCurrentWaypoint();
    }

    // private void RotateTowardsCurrentWaypoint()
    // {
    //     var direction = waypoints[currentWaypoint].transform.position - transform.position;
    //     var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     var targetRotation = Quaternion.Euler(0, 0, angle);
    //     transform.rotation =
    //         Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    //     
    //     if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
    //     {
    //         isRotating = false;
    //     }
    // }

    private void MoveTowardsCurrentWaypoint()
    {
        var moved = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );

        rb.MovePosition(moved);

        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        // isRotating = true;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != target)
            return;
        var distance = Vector2.Distance(transform.position, other.transform.position);
        if (distance <= RangeAction) AttackHero(other.gameObject);
        // GetComponent<AudioSource>().Play();
    }

    private void AttackHero(GameObject hero)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<StraightBullet>().Target = hero; //.GetComponent<FutureHero>().heroTarget;
        bullet.transform.position = transform.position;
        Debug.Log("Дрон атакует FutureHero через триггер!");
    }
}