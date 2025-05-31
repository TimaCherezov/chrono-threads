using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    [Header("Настройки стрельбы")]
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public int bulletsPerWave = 8;
    public float bulletSpeed = 5f;
    public float spiralFactor = 30f; 

    [Header("Настройки движения")]
    public float moveSpeed = 2f;
    public float changeTargetInterval = 3f;
    public float stopDistance = 2f;
    
    private Transform[] players;
    private Transform currentTarget;
    private float attackAngle;

    void Start()
    {
        FindPlayers();
        StartCoroutine(ChangeTargetRoutine());
        StartCoroutine(AttackPatternRoutine());
    }

    void FindPlayers()
    {
        var playerObjects = GameObject.FindGameObjectsWithTag("Player");
        players = new Transform[playerObjects.Length];
        for (var i = 0; i < playerObjects.Length; i++)
        {
            players[i] = playerObjects[i].transform;
        }
    }

    void Update()
    {
        //var distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);
        // && distanceToTarget > stopDistance
        if (currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                currentTarget.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    IEnumerator ChangeTargetRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTargetInterval);
            if (players.Length > 0)
            {
                currentTarget = players[Random.Range(0, players.Length)];
            }
        }
    }

    IEnumerator AttackPatternRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / fireRate);

            // Круговой выстрел
            GetComponent<AudioSource>().Play();
            CircleAttack();

            // Спиральный выстрел с поворотом
            yield return new WaitForSeconds(0.5f);
            GetComponent<AudioSource>().Play();
            yield return StartCoroutine(SpiralAttack(10, 0.15f));
        }
    }

    void CircleAttack()
    {
        for (var i = 0; i < bulletsPerWave; i++)
        {
            var angle = i * (360f / bulletsPerWave);
            var dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                                     Mathf.Sin(angle * Mathf.Deg2Rad));
            FireBullet(dir);
        }
    }

    IEnumerator SpiralAttack(int bulletsCount, float delay)
    {
        for (var i = 0; i < bulletsCount; i++)
        {
            attackAngle += spiralFactor;
            var dir = new Vector2(Mathf.Cos(attackAngle * Mathf.Deg2Rad),
                                     Mathf.Sin(attackAngle * Mathf.Deg2Rad));
            FireBullet(dir.normalized);
            yield return new WaitForSeconds(delay);
        }
    }

    void FireBullet(Vector2 direction)
    {
        if (bulletPrefab == null) return;

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogError("У пули нет Rigidbody2D!");
        }

        // Поворот пули в направлении движения
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(bullet, 3f);
    }
}