using UnityEngine;
using UnityEngine.Serialization;

public class StraightBullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 0.1f;
    public GameObject Target { get; set; }
    private Vector3 _vector;
    private Vector3 _initial;


    private void Start()
    {
        _initial = transform.position;
        _vector = Target.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position += (_vector - _initial) * (Time.deltaTime * speed);
        if (Vector2.Distance(_initial, transform.position) > 10)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != Target)
            return;
        // other.gameObject.GetComponent<Rigidbody2D>().MovePosition((other.transform.position - transform.position) * 1000f + other.transform.position);
        other.gameObject.GetComponent<HeroHealth>().ApplyDamage(-damage);
        Destroy(gameObject);
    }
}