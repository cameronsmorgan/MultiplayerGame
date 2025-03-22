using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour
{

    [SerializeField] private float speedIncrease = 3f; 
    [SerializeField] private float boostDuration = 5f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has either PlayerMovement or Player2Movement
        PlayerMovement player1 = collision.GetComponent<PlayerMovement>();
        Player2Movement player2 = collision.GetComponent<Player2Movement>();

        if (player1 != null && !player1.isBoosted)
        {
            player1.StartCoroutine(ApplySpeedBoost(player1));
            Destroy(gameObject);
        }
        else if (player2 != null && !player2.isBoosted)
        {
            player2.StartCoroutine(ApplySpeedBoost(player2));
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplySpeedBoost(MonoBehaviour playerScript)
    {
        if (playerScript is PlayerMovement player)
        {
            player.isBoosted = true;
            player.moveSpeed += speedIncrease;
            yield return new WaitForSeconds(boostDuration);
            player.moveSpeed -= speedIncrease;
            player.isBoosted = false;
        }
        else if (playerScript is Player2Movement player2)
        {
            player2.isBoosted = true;
            player2.moveSpeed += speedIncrease;
            yield return new WaitForSeconds(boostDuration);
            player2.moveSpeed -= speedIncrease;
            player2.isBoosted = false;
        }
    }
}
