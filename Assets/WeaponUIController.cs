using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUIController : MonoBehaviour
{
    public Image weaponIcon;
    public TextMeshProUGUI weaponName;

    public void UpdateUI(FireSlot slot)
    {
        if (weaponIcon != null)
            weaponIcon.sprite = slot.icon;

        if (weaponName != null)
            weaponName.text = slot.weaponName;
    }
}
