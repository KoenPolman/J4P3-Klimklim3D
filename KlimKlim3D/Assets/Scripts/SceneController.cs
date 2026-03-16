using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void RebuildScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
