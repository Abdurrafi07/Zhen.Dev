using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Weapon System")]
    public FireSlot[] fireSlots;
    public int currentWeaponIndex = 0;

    [Header("Ammo System")]
    public int maxAmmo = 10;           // Maksimum peluru
    public float regenDelay = 0.5f;    // Waktu untuk regen 1 peluru
    private int currentAmmo;
    private float regenTimer = 0f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shootSfx;


    [Header("UI")]
    public WeaponUIController weaponUI;
    public Image ammoBar;
    public TextMeshProUGUI ammoText;

    [Header("Player Info")]
    public string playerName;
    public TMP_Text nameDisplay; // Text di UI untuk menampilkan nama pemain

    [Header("Visual")]
    public Transform body;
    public float bodyOffset = 0.4f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;
    private Rigidbody2D platformRb;
    private Vector3 bodyOriginalScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one;

        if (body == null)
            Debug.LogError("Body belum di-assign!");

        bodyOriginalScale = body.localScale;
    }

    void Start()
    {
        // Ambil nama pemain dari PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            if (nameDisplay != null)
                nameDisplay.text = "Player: " + playerName;
        }

        currentAmmo = maxAmmo; // mulai penuh
        UpdateWeaponUI();
        UpdateVisual();
        UpdateAmmoUI();
    }

    void Update()
    {
        Move();
        Jump();
        SwitchWeapon();
        SwitchWeaponByNumber();
        Shoot();
        RegenAmmo();
    }

    void FixedUpdate()
    {
        CheckGround();
        if (platformRb != null && isGrounded)
        {
            rb.position += platformRb.linearVelocity * Time.fixedDeltaTime;
        }
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (move > 0) { facingRight = true; UpdateVisual(); }
        else if (move < 0) { facingRight = false; UpdateVisual(); }
    }

    void UpdateVisual()
    {
        if (facingRight)
        {
            body.localScale = new Vector3(Mathf.Abs(bodyOriginalScale.x), bodyOriginalScale.y, bodyOriginalScale.z);
            body.localPosition = new Vector3(bodyOffset, 0, 0);
        }
        else
        {
            body.localScale = new Vector3(-Mathf.Abs(bodyOriginalScale.x), bodyOriginalScale.y, bodyOriginalScale.z);
            body.localPosition = new Vector3(-bodyOffset, 0, 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && fireSlots.Length > 0 && currentAmmo > 0)
        {
            FireSlot slot = fireSlots[currentWeaponIndex];

            GameObject bullet = Instantiate(slot.bulletPrefab, slot.firePoint.position, Quaternion.identity);
            Bullet b = bullet.GetComponent<Bullet>();
            if (b != null)
                b.SetDirection(facingRight ? 1 : -1);

            // 🔊 AUDIO TEMBAKAN
            if (audioSource != null && shootSfx != null)
                audioSource.PlayOneShot(shootSfx);

            currentAmmo--;
            UpdateAmmoUI();
        }
    }

    void RegenAmmo()
    {
        if (currentAmmo < maxAmmo)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenDelay)
            {
                currentAmmo++;
                regenTimer = 0f;
                UpdateAmmoUI();
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoBar != null)
            ammoBar.fillAmount = (float)currentAmmo / maxAmmo;

        if (ammoText != null)
            ammoText.text = currentAmmo.ToString();
    }

    void SwitchWeapon()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0) ChangeWeapon(1);
        else if (scroll < 0) ChangeWeapon(-1);
    }

    void SwitchWeaponByNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeapon(2);
    }

    void ChangeWeapon(int dir)
    {
        currentWeaponIndex += dir;
        if (currentWeaponIndex < 0) currentWeaponIndex = fireSlots.Length - 1;
        else if (currentWeaponIndex >= fireSlots.Length) currentWeaponIndex = 0;

        UpdateWeaponUI();
    }

    void SetWeapon(int index)
    {
        if (index < 0 || index >= fireSlots.Length) return;
        currentWeaponIndex = index;
        UpdateWeaponUI();
    }

    void UpdateWeaponUI()
    {
        if (weaponUI != null && fireSlots.Length > 0)
            weaponUI.UpdateUI(fireSlots[currentWeaponIndex]);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            platformRb = collision.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            platformRb = null;
    }
}
