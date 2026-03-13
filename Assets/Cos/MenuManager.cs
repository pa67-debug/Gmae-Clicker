using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Button Sounds")]
    public AudioSource audioSource;

    public AudioClip menuClickSound;
    public AudioClip gameClickSound;

    public void StartGame()
    {
        PlayClick(menuClickSound);
        SceneManager.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        PlayClick(gameClickSound);

        // ลบ Save ทั้งหมด
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameScene");
    }

    public void ExitToMenu()
    {
        PlayClick(gameClickSound);

        GameManager.Instance.SaveGameData();
        SceneManager.LoadScene("MainMenu");
    }

    void PlayClick(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}