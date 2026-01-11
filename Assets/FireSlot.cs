using UnityEngine;

[System.Serializable]
public class FireSlot
{
    public string weaponName;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("UI")]
    public Sprite icon;
}
