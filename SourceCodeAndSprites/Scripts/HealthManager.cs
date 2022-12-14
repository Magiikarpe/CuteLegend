using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool isInvinsible = false;
    public SpriteRenderer graphics;
    public float invincibilityFlashDelay = 0.15f;
    public float invincibilityTimeAfterHit = 3f;
    private Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    void Update()
    {
        foreach(Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for(int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void TakeDamage()
    {
        if(!isInvinsible && health > 1)
        {
            health -= 1;
            PlayerMovement.instance.animator.SetTrigger("Damage");
            isInvinsible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        Debug.Log("The player is eliminated");
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.animator.SetTrigger("Die");
        StartCoroutine(LoadTime());
    }

    private IEnumerator LoadTime()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Idle");
        if(CurrentSceneManagment.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator InvincibilityFlash()
    {
        while(isInvinsible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvinsible = false;
    }

    public IEnumerator HandleBonusInvincibilityDelay(float s)
    {
        yield return new WaitForSeconds(s);
        isInvinsible = false;
    }

    public void BonusInvincible(float s)
    {
        isInvinsible = true;
        StartCoroutine(InvincibilityFlash());
        StartCoroutine(HandleBonusInvincibilityDelay(s));
    }

    public void BonusHealth()
    {
        if (health < 3)
        {
            health += 1;
        }
    }
}
