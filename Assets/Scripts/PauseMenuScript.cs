using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private GameObject pauseCanvas;
    private bool currentlyPaused = false;

    private void Start()
    {
        pauseCanvas = GameObject.Find("PausePanel");

        Unpause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !currentlyPaused)
        {
            Pause();
            currentlyPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentlyPaused)
        {
            Unpause();
            currentlyPaused = false;
        }


    }

    public bool IsPaused() { return currentlyPaused; }

    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.GetComponent<Canvas>().enabled = true;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.GetComponent<Canvas>().enabled = false;
        Cursor.visible = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
