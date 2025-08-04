using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    public static PathGeneration Instance { get; private set; }

    public int[,] dungeon;
    public Vector2Int dimensions = new Vector2Int(7, 5);
    
    private Vector2Int start;
    private int dungeonPathLength = 13;



    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;

        dungeon = new int[dimensions.x, dimensions.y];
    }



    // Start is called before the first frame update
    void Start()
    {
        
        start = new Vector2Int(UnityEngine.Random.Range(0, dimensions.x), 0);
        
    }

    public void GenerateDungeonMap()
    {
        InitializeGrid();
        GeneratePath(start, dungeonPathLength);
        PrintGrid();
    }    


    private void InitializeGrid()
    {
        for (int i = 0; i < dimensions.y; i++)
        {
            for (int j = 0; j < dimensions.x; j++)
            {
                dungeon[j, i] = 0;
            }
        }
    }



    private void PrintGrid()
    {
        for (int i = 0; i < dimensions.y; i++)
        {
            string row = "";
            for (int j = 0; j < dimensions.x; j++)
            {
                row += " [" + dungeon[j, i] + "] ";
            }
            Debug.Log(row);
        }
    }

    private bool GeneratePath(Vector2Int current, int length)
    {
        if (length <= 0)
        {
            return true;
        }

        
        List<Vector2Int> directions = new List<Vector2Int>
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        for (int i = 0; i < directions.Count; i++)
        {
            int randIndex = UnityEngine.Random.Range(i, directions.Count);
            var temp = directions[i];
            directions[i] = directions[randIndex];
            directions[randIndex] = temp;
        }

        foreach (var direction in directions)
        {
            Vector2Int next = current + direction;

            
            if (next.x >= 0 && next.x < dimensions.x &&
                next.y >= 0 && next.y < dimensions.y &&
                dungeon[next.x, next.y] == 0)
            {
                dungeon[next.x, next.y] = length;

                if (GeneratePath(next, length - 1))
                    return true;

                
                dungeon[next.x, next.y] = 0;
            }
        }

        return false;
    }




}
