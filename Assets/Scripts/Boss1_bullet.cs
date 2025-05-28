using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float rotationSpeed = 360f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "HeroTarget")
            return;

        var heroHealth = other.GetComponentInParent<HeroHealth>();
        if (heroHealth != null)
        {
            Debug.Log("Bullet ������ � ������!");
            heroHealth.ApplyDamage(-1);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("��������� HeroHealth �� ������ � ������������ �������� " + other.gameObject.name);
        }
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}