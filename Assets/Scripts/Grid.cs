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

    private Dictionary<Vector2, bool> gridData = new();

    [SerializeField]
    private bool debug;

    private void Awake()
    {
        for (float i = 0; i < gridWidth; i += gridSize)
        {
            for (float j = 0; j < gridHeight; j += gridSize)
            {
                gridData.TryAdd(new Vector2(i, j), false);
            }
        }
    }

    /// <summary>
    /// Keeps track of what's in what position in the grid
    /// </summary>
    /// <param name="pos">The position to check</param>
    /// <returns>Whether the position is occupied</returns>
    public bool GetTileData(Vector2 pos)
    {
        if (gridData.TryGetValue(pos, out var data))
        {
            return data;
        }

        return false;
    }

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
}
