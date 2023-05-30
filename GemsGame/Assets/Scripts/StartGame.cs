using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OpenGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
