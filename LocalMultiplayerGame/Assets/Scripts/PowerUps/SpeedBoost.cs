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


    /* Title: How to have a speed boost in my game?
     * Author: Suddoha
     * Date: 22 March 2025
     * Code Version: Unity 5
     * Availability:  https://discussions.unity.com/t/how-to-have-a-speed-boost-in-my-game/148117
     */
    private IEnumerator ApplySpeedBoost(MonoBehaviour playerScript)  //takes a monobehavior param so it can be used for both players
    {
        if (playerScript is PlayerMovement player)   //applies to player 1
        {
            player.isBoosted = true;
            player.moveSpeed += speedIncrease;
            yield return new WaitForSeconds(boostDuration);
            player.moveSpeed -= speedIncrease;
            player.isBoosted = false;
        }
        else if (playerScript is Player2Movement player2)  //applies to player 2
        {
            player2.isBoosted = true;
            player2.moveSpeed += speedIncrease;
            yield return new WaitForSeconds(boostDuration);
            player2.moveSpeed -= speedIncrease;
            player2.isBoosted = false;
        }
    }
}
