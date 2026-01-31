using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float gridSize = 1f;

    [SerializeField]
    private float gridWidth = 20f;

    [SerializeField]
    private float gridHeight = 20f;

    [Range(0, 100)]
    [SerializeField]
    private float propSpawnChance = 5f;

    private Dictionary<Vector2, TileData> gridData = new();

    [SerializeField]
    private bool debug;

    public bool isActive;

    [SerializeField]
    private GameObject propObject;

    private void Awake()
    {
        if (!isActive)
            return;

        for (float i = 0; i < gridWidth; i += gridSize)
        {
            for (float j = 0; j < gridHeight; j += gridSize)
            {
                int value = Random.Range(0, 100);

                Vector2 key = new Vector2(i, j);

                if (value <= propSpawnChance)
                {
                    Instantiate(propObject, key, Quaternion.identity, transform);
                    gridData.TryAdd(new Vector2(i, j), new TileData(true, key));
                }
                else
                {
                    gridData.TryAdd(new Vector2(i, j), new TileData(true, key));
                }
            }
        }
    }

    /// <summary>
    /// Keeps track of what's in what position in the grid
    /// </summary>
    /// <param name="pos">The position to check</param>
    /// <returns>Whether the position is occupied</returns>
    public TileData GetTileData(Vector2 pos)
    {
        if (gridData.TryGetValue(pos, out var data))
        {
            return data;
        }

        return null;
    }

    public float GetTileSize() => gridSize;

    private void OnDrawGizmos()
    {
        if (!debug)
            return;

        Gizmos.color = Color.white;

        for (float i = 0; i < gridWidth; i += gridSize)
        {
            for (float j = 0; j < gridHeight; j += gridSize)
            {
                Gizmos.DrawCube(new Vector3(i, j), new Vector2(gridSize, gridSize));
            }
        }
    }

    public class TileData
    {
        public bool isOccupied;
        public Vector2 position;

        public TileData(bool occupied, Vector2 pos)
        {
            isOccupied = occupied;
            position = pos;
        }
    }
}
