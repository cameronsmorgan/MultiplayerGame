using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public AudioClip powerUpSound; // Assign in Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
        }

        // Ensure the power-up sound does NOT play when it spawns
        audioSource.playOnAwake = false;
        audioSource.clip = powerUpSound;
        audioSource.volume = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Power-up collected!");
            StartCoroutine(PlaySoundAndDestroy());
        }
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        audioSource.Play(); // Now the sound only plays when collected
        GetComponent<Collider2D>().enabled = false; // Disable collider to prevent re-trigger
        GetComponent<SpriteRenderer>().enabled = false; // Hide power-up while sound plays
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}


