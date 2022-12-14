using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static bool gameIsWin = false;
    public GameObject winMenuUi;
    private Transform playerSpawn;
    private Transform playerSpawnFix;
    private Animator fadeSystem;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        playerSpawnFix = GameObject.FindGameObjectWithTag("PlayerSpawnFix").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    void Update()
    {
        if(gameIsWin)
        {
            Paused();
        }
    }

    private void Paused()
    {
        winMenuUi.SetActive(true);
        Time.timeScale = 0;
        gameIsWin = true;
        PlayerMovement.instance.enabled = false;
    }

    public void Resume()
    {
        winMenuUi.SetActive(false);
        Time.timeScale = 1;
        gameIsWin = false;
        PlayerMovement.instance.enabled = true;
    }

    public void RestartGame()
    {
        winMenuUi.SetActive(false);
        Time.timeScale = 1;
        gameIsWin = false;
        PlayerMovement.instance.enabled = false;
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Idle");
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("Level01");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
