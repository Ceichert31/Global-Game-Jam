using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private GameObject rubbishObject;

    private void Awake()
    {
        if (!isActive)
            return;

        for (float i = 0; i < gridWidth; i += gridSize)
        {
            for (float j = 0; j < gridHeight; j += gridSize)
            {
                gridData.TryAdd(new Vector2(i, j), new TileData(false, new Vector2(i, j)));
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

    public Vector2? GetClosestTile(Vector2 pos)
    {
        //Check around area
        //Check if occupied
        Vector2 coordinate = new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));

        List<TileData> unoccupiedTileList = new();

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                //get list of all points that aren't occupied
                int x = (int)coordinate.x + i;
                int y = (int)coordinate.y + j;

                var key = new Vector2(x, y);
                if (gridData.TryGetValue(key, out TileData data))
                {
                    if (data.isOccupied)
                        continue;

                    unoccupiedTileList.Add(data);
                }
            }
        }

        if (unoccupiedTileList.Count == 0)
        {
            return null;
        }

        float dist = UInt64.MaxValue;
        Vector2 closestTile = Vector2.zero;

        foreach (var tileData in unoccupiedTileList)
        {
            float currentDist = Vector2.Distance(pos, tileData.position);

            if (currentDist < dist)
            {
                closestTile = tileData.position;
            }
        }

        return closestTile;
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
