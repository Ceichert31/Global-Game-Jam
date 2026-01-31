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

    private Dictionary<Vector2, TileData> gridData = new();

    [SerializeField]
    private bool debug;

    public GameObject testObj;

    public GameObject obstacleObj;

    private void Awake()
    {
        for (float i = 0; i < gridWidth; i += gridSize)
        {
            for (float j = 0; j < gridHeight; j += gridSize)
            {
                int value = Random.Range(0, 50);

                if (value <= 40)
                {
                    Instantiate(testObj, new Vector2(i, j), Quaternion.identity, transform);
                    gridData.TryAdd(new Vector2(i, j), new TileData(false, new Vector2(i, j)));
                }
                else
                {
                    Instantiate(obstacleObj, new Vector2(i, j), Quaternion.identity, transform);
                    gridData.TryAdd(new Vector2(i, j), new TileData(true, new Vector2(i, j)));
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

        Vector2 previousPos = Vector2.zero;

        for (float i = 0; i < gridWidth; i += gridSize)
        {
            for (float j = 0; j < gridHeight; j += gridSize)
            {
                Gizmos.DrawLine(previousPos, new Vector2(i, j));

                previousPos = new Vector2(i, j);
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
