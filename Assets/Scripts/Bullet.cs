using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject target { get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, target.transform.position,
            1 * Time.deltaTime);
        Vector2 direction = target.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "HeroTarget")
            return;
        other.GetComponentInParent<HeroHealth>().ApplyDamage(-2);
        Destroy(gameObject);
    }
}