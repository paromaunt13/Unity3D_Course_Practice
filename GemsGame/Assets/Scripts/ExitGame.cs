using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
