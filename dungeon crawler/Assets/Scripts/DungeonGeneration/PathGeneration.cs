using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    public static PathGeneration Instance { get; private set; }

    public string[,] dungeon;
    public Vector2Int dimensions;
    
    private Vector2Int start;
    private int dungeonPathLength = 13;
    private int branchNumber = 3;
    private Vector2Int branchLenghtRange = new Vector2Int(1, 4);
    private List<Vector2Int> branchCandidates = new List<Vector2Int>();



    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;

        dimensions = new Vector2Int(9, 9);
    }


    public void GenerateDungeonMap()
    {
        InitializeGrid();
        GeneratePath(start, dungeonPathLength, "C");
        GenerateBranchs();
        PrintGrid();
    }    


    private void InitializeGrid()
    {

        start = new Vector2Int(UnityEngine.Random.Range(0, dimensions.x), 0);

        dungeon = new string[dimensions.x, dimensions.y];

        for (int i = 0; i < dimensions.y; i++)
        {
            for (int j = 0; j < dimensions.x; j++)
            {
                dungeon[j, i] = "";
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

    private bool GeneratePath(Vector2Int current, int length, string mark)
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
                dungeon[next.x, next.y] == "")
            {
                if (length != dungeonPathLength)
                    dungeon[next.x, next.y] = mark;
                else
                    dungeon[next.x, next.y] = "S";


                if (length > 0)
                    branchCandidates.Add(new Vector2Int(next.x, next.y));
                if (GeneratePath(next, length - 1, mark))
                    return true;

                
                dungeon[next.x, next.y] = "";
                branchCandidates.Remove(new Vector2Int(next.x, next.y));
            }
        }

        return false;
    }



    public void GenerateBranchs()
    {
        int branchsCreated = 0;
        Vector2Int candidate;

        while (branchsCreated < branchNumber && branchCandidates.Count > 0)
        {
            candidate = branchCandidates[UnityEngine.Random.Range(0, branchCandidates.Count - 1)];
            if (GeneratePath(candidate, UnityEngine.Random.Range(branchLenghtRange.x, branchLenghtRange.y), (branchsCreated+1).ToString()))
                branchsCreated += 1;

            else
                branchCandidates.Remove(candidate);

        }



    }


}
