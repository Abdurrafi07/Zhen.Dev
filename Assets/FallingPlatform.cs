using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float fallDelay = 1f;
    public float destroyTime = 2f;

    [HideInInspector]
    public TrapRespawner respawner; // Referensi ke respawn manager

    private Rigidbody2D rb;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;

        // Beri waktu jatuh, lalu hancur
        yield return new WaitForSeconds(destroyTime);

        // Lapor ke respawner sebelum hancur
        if (respawner != null)
            respawner.NotifyDestroyed();

        Destroy(gameObject);
    }
}
