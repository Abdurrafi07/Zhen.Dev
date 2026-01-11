using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 3;
    public int scoreValue = 1;

    [Header("Movement Settings")]
    [HideInInspector] public bool vertical = false;
    [HideInInspector] public float moveSpeed = 2f;
    [HideInInspector] public float distance = 2f;

    [Header("Audio")]
    public AudioClip destroySFX;     // 🔥 Tambahan: suara saat musuh hancur
    public AudioSource audioSource; // 🔥 Tambahan

    [Header("Events")]
    public Action OnDestroyed;

    private int currentHealth;
    private bool isDead = false;
    private Vector2 startPos;

    void Start()
    {
        currentHealth = maxHealth;
        startPos = transform.position;

        // ambil audioSource dari gameobject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (!vertical)
        {
            float x = Mathf.PingPong(Time.time * moveSpeed, distance);
            transform.position = startPos + new Vector2(x, 0);
        }
        else
        {
            float y = Mathf.PingPong(Time.time * moveSpeed, distance);
            transform.position = startPos + new Vector2(0, y);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        GameManager.instance?.AddScore(scoreValue);
        OnDestroyed?.Invoke();

        if (destroySFX != null)
        {
            // Buat sementara objek untuk mainkan audio
            GameObject sfx = new GameObject("SFX");
            AudioSource a = sfx.AddComponent<AudioSource>();
            a.PlayOneShot(destroySFX);
            Destroy(sfx, destroySFX.length); // hancurkan setelah audio selesai
        }

        Destroy(gameObject); // musuh mati
    }

    public void DestroyByBullet()
    {
        TakeDamage(currentHealth);
    }
}
