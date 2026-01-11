using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 1;

    private int direction = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Score di-handle musuh
            Destroy(gameObject);
        }
    }
}
