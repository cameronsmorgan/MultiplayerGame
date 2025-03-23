using UnityEngine;
using UnityEngine.Tilemaps;

public class ColourSpawner : MonoBehaviour
{
    public GameObject ColourBombPrefab;
    public Tilemap tilemap;
    public BoxCollider2D playAreaCollider;

    public float spawnInterval = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnColorBomb), spawnInterval, spawnInterval);
    }

    private void SpawnColorBomb()
    {
        Vector2 randomPosition = GetRandomPositionWithinPlayArea();
        GameObject newBomb = Instantiate(ColourBombPrefab, randomPosition, Quaternion.identity);

        // Pass the Tilemap reference to the spawned object
        EraseBomb colorBombScript = newBomb.GetComponent<EraseBomb>();
        if (colorBombScript != null)
        {
            colorBombScript.tilemap = tilemap;
        }
    }


    /* Title: Random Position Within a 2d Collider
     * Author: PapayaLimon
     * Date: 13 March 2025
     * Code version: Unity 2022.2
     * Availability: https://discussions.unity.com/t/random-position-inside-2d-collider/907682
     */
    private Vector2 GetRandomPositionWithinPlayArea()
    {
        Bounds bounds = playAreaCollider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}