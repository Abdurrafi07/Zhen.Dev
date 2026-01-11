using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 2f;
    [HideInInspector] public bool horizontal = true;

    private Vector2 startPos;

    public event Action OnDestroyed;

    private bool isDead = false; // 🔑 flag agar DestroyByBullet hanya terpanggil sekali

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (horizontal)
        {
            float x = Mathf.PingPong(Time.time * speed, distance);
            transform.position = startPos + new Vector2(x, 0);
        }
        else
        {
            float y = Mathf.PingPong(Time.time * speed, distance);
            transform.position = startPos + new Vector2(0, y);
        }
    }

    // Fungsi untuk dihancurkan oleh peluru
    public void DestroyByBullet()
    {
        if (isDead) return; // ⚠️ jangan panggil dua kali
        isDead = true;

        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
