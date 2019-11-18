using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetCurrentScene : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
