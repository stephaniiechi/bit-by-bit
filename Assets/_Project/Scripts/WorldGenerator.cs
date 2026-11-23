
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject groundChunkPrefab;
    [SerializeField] private TerrainGenerator terrainGenerator;

    [Header("World Settings")]
    [SerializeField, Min(1)] private int chunkSize = 16;
    [SerializeField, Min(1)] private int renderDistance = 2;

    private readonly Dictionary<Vector2Int, GameObject> activeChunks = new();

    private Vector2Int currentChunk;

    private void Start()
    {
        currentChunk = GetChunkPosition(player.position);
        UpdateChunks();
    }

    private void Update()
    {
        Vector2Int newChunk = GetChunkPosition(player.position);

        if (newChunk == currentChunk)
            return;

        currentChunk = newChunk;
        UpdateChunks();
    }

    private Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.y / chunkSize)
        );
    }

    private void UpdateChunks()
    {
        HashSet<Vector2Int> requiredChunks = new();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int coord =
                    currentChunk + new Vector2Int(x, y);

                requiredChunks.Add(coord);

                if (!activeChunks.ContainsKey(coord))
                    SpawnChunk(coord);
            }
        }

        List<Vector2Int> chunksToRemove = new();

        foreach (var chunk in activeChunks)
        {
            if (!requiredChunks.Contains(chunk.Key))
                chunksToRemove.Add(chunk.Key);
        }

        foreach (Vector2Int coord in chunksToRemove)
        {
            Destroy(activeChunks[coord]);
            activeChunks.Remove(coord);
        }
    }

    private void SpawnChunk(Vector2Int coord)
    {
        Vector3 position = new Vector3(
            coord.x * chunkSize,
            coord.y * chunkSize,
            0f
        );

        GameObject chunk = Instantiate(
            groundChunkPrefab,
            position,
            Quaternion.identity,
            transform
        );

        Tilemap tilemap = chunk.GetComponentInChildren<Tilemap>();

        if (tilemap == null)
        {
            Debug.LogError("GroundChunk prefab has no Tilemap!");
            Destroy(chunk);
            return;
        }

        terrainGenerator.Generate(tilemap, coord, chunkSize);

        activeChunks.Add(coord, chunk);
    }
}
