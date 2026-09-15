using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI; 
    public GameObject IngameMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void Resume()
    {
        IngameMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        IngameMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
