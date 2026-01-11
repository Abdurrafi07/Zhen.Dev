using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Simpan data terakhir sebelum pindah scene
            GameManager.instance.SaveData();

            // Pindah ke FinishScene
            SceneManager.LoadScene("FinishScene");
        }
    }
}
