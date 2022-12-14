using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public bool invinsible;
    public float delayInvincibilityBonus = 5f;
    public AudioClip sound;
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HealthManager playerHealth = collision.transform.GetComponent<HealthManager>();
            if(invinsible && !playerHealth.isInvinsible)
            {
                // audioSource.PlayOneShot(sound);
                Destroy(gameObject);
                playerHealth.BonusInvincible(delayInvincibilityBonus);
            }
            if(!invinsible && playerHealth.health < 3)
            {
                // audioSource.PlayOneShot(sound);
                Destroy(gameObject);
                playerHealth.BonusHealth();
            }
        }
    }
}
