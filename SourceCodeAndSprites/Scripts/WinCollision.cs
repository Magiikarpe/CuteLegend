using UnityEngine;

public class WinCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            WinMenu.gameIsWin = true;
        }
    }
}
