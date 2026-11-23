
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Tiles")]
    [SerializeField] private TileBase grassTile;
    [SerializeField] private TileBase dirtTile;

    [Header("Generation Settings")]
    [SerializeField] private int seed = 42;
    [SerializeField] private float noiseScale = 0.08f;
    [SerializeField, Range(0f, 1f)]
    private float dirtThreshold = 0.4f;

    public void Generate(Tilemap tilemap, Vector2Int chunkCoord, int chunkSize)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                int worldX = chunkCoord.x * chunkSize + x;
                int worldY = chunkCoord.y * chunkSize + y;

                float noise = Mathf.PerlinNoise(
                    (worldX + seed * 1000f) * noiseScale,
                    (worldY + seed * 1000f) * noiseScale
                );

                TileBase tile = noise < dirtThreshold
                    ? dirtTile
                    : grassTile;

                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}
