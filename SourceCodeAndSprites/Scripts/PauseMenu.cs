using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUi;
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            } else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        PlayerMovement.instance.enabled = true;
    }

    public void RestartLevel()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        PlayerMovement.instance.enabled = false;
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Idle");
        HealthManager playerHealth = PlayerMovement.instance.transform.GetComponent<HealthManager>();
        playerHealth.health = 3;
        if(CurrentSceneManagment.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
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

    private void Paused()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        PlayerMovement.instance.enabled = false;
    }
}
