using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    [Header("Movement Direction")]
    public bool moveHorizontal = true;
    public bool moveVertical = false;

    private Vector2 startPos;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
    }

    void FixedUpdate()
    {
        float x = moveHorizontal ? Mathf.PingPong(Time.time * speed, distance) : 0f;
        float y = moveVertical ? Mathf.PingPong(Time.time * speed, distance) : 0f;

        Vector2 targetPos = startPos + new Vector2(x, y);
        rb.MovePosition(targetPos);
    }
}
