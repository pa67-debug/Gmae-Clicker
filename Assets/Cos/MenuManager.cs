using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ExitToMenu()
    {
        PlayerAttack player = FindObjectOfType<PlayerAttack>();

        if (player != null)
        {
            GameManager.Instance.SaveGame(player);
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}