using UnityEngine;
using UnityEngine.UI;

public class AmmoUIController : MonoBehaviour
{
    public Image ammoBar;    // assign image type = Filled
    public Text ammoText;    // optional, untuk "5 / 10"

    public void SetAmmo(int current, int max)
    {
        if (ammoBar != null)
        {
            ammoBar.fillAmount = (float)current / max;  // 0 - 1
        }

        if (ammoText != null)
        {
            ammoText.text = $"{current} / {max}";
        }
    }
}
