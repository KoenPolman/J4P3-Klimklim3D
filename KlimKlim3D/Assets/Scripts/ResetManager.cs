using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetManager : MonoBehaviour
{
    private SceneController sceneController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneController = GetComponent<SceneController>();
    }
    private void Update()
    {
        if (Keyboard.current.rKey.wasReleasedThisFrame)
        {
            /*
            PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
            Destroy(playerInput.gameObject);
            */

            sceneController.RebuildScene();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // The old way of doing it, currently coverd by the functionality of SceneController
        }
    }
}
