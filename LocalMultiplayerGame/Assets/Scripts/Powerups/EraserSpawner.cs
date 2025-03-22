using UnityEngine;
using UnityEngine.Tilemaps;

public class EraserSpawner : MonoBehaviour
{
    //public GameObject EraseBombPrefab;
    public GameObject EraserBombPrefab;
    public Tilemap tilemaps;
    public BoxCollider2D playAreaColliders;

    public float SpawnInterval = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEraserBomb), SpawnInterval, SpawnInterval);  //SpawnEraseBomb Method, SpawnInterval = how long to wait before spawning the first bomb, Spawn Interval = how often to spawn
    }

    private void SpawnEraserBomb()
    {
        Vector2 randomPosition = GetRandomPositionWithinPlayAreas();
        GameObject newBomb = Instantiate(EraserBombPrefab, randomPosition, Quaternion.identity);

        // Pass the Tilemap reference to the spawned object
        //EraseBomb eraseBombScript = newBomb.GetComponent<EraseBomb>();
        //if (eraseBombScript != null)
        //{
        //    eraseBombScript.tilemap = tilemaps;
        //}
    }


    private Vector2 GetRandomPositionWithinPlayAreas()
    {
        Bounds bounds = playAreaColliders.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}