using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject hero { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, hero.transform.position,
            2 * Time.deltaTime);
        Vector2 direction = hero.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "FutureHero") return;
        other.GetComponent<FutureHero>().TakeDamage(2);
        Destroy(gameObject);
    }
}