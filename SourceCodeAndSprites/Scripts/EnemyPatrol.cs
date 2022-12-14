using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    private Transform target;
    private int destPoint = 0;
    public int damageOnCollision = 1;
    private Transform playerSpawn;
    private Transform playerSpawnFix;
    private Animator fadeSystem;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        playerSpawnFix = GameObject.FindGameObjectWithTag("PlayerSpawnFix").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //Change destination enemie
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collision2D collision)
    {
        HealthManager playerHealth = collision.transform.GetComponent<HealthManager>();
        if (!playerHealth.isInvinsible) {
            PlayerMovement.instance.enabled = false;
            if(playerHealth.health > 1)
            {
                playerHealth.TakeDamage();
                fadeSystem.SetTrigger("FadeIn");
                yield return new WaitForSeconds(1f);
                collision.transform.position = playerSpawn.position;
                yield return new WaitForSeconds(1f);
                PlayerMovement.instance.enabled = true;
                PlayerMovement.instance.animator.SetTrigger("Idle");
            } else
            {
                playerHealth.Die();
                playerHealth.health = 3;
            }
        }
    }
}
