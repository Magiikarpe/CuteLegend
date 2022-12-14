using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Transform playerSpawnFix;
    private Animator fadeSystem;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        playerSpawnFix = GameObject.FindGameObjectWithTag("PlayerSpawnFix").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMovement.instance.enabled = false;
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        HealthManager playerHealth = collision.transform.GetComponent<HealthManager>();
        if(playerHealth.health > 1)
        {
            if(!playerHealth.isInvinsible)
            {
                playerHealth.TakeDamage();
            }
            fadeSystem.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1f);
            collision.transform.position = playerSpawn.position;
            yield return new WaitForSeconds(1f);
            PlayerMovement.instance.enabled = true;
            PlayerMovement.instance.animator.SetTrigger("Idle");
        } else {
            if(!playerHealth.isInvinsible)
            {
                playerHealth.Die();
                playerHealth.health = 3;
            }
            else {
                fadeSystem.SetTrigger("FadeIn");
                yield return new WaitForSeconds(1f);
                playerSpawn.position = playerSpawnFix.position;
                collision.transform.position = playerSpawnFix.position;
                yield return new WaitForSeconds(1f);
                PlayerMovement.instance.enabled = true;
                PlayerMovement.instance.animator.SetTrigger("Idle");
            }
        }
    }
}
