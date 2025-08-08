using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;


public class DungeonGeneratorGrind : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject roomPrefab;


    private float platformHeight;
    private float platformWidth;
    private Vector3 startRoomPos;
    private List<GameObject> rooms = new List<GameObject>();


    private void Awake()
    {
        if (platformPrefab != null)
        {
            Renderer renderer = platformPrefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                platformWidth = MathF.Round(renderer.bounds.size.x, 1);
                platformHeight = MathF.Round(renderer.bounds.size.y, 1);
            }
        }
            
    }



    // Start is called before the first frame update
    void Start()
    {
        PathGeneration.Instance.GenerateDungeonMap();
        rooms = RenderMap();
        InitialPlayerSpawn();
    }

    private List<GameObject> RenderMap()
    {
        float platformSpawnX = 0f;
        float platformSpawnY = 0f;
        for (int i = 0; i < PathGeneration.Instance.dimensions.y; i++)
        {
            for(int j = 0; j <  PathGeneration.Instance.dimensions.x; j++)
            {
                if (PathGeneration.Instance.dungeon[j, i] == "C")
                {
                    rooms.Add(Instantiate<GameObject>(platformPrefab, new Vector3(platformSpawnX, platformSpawnY, 0), Quaternion.identity));
                    
                }
                else if (PathGeneration.Instance.dungeon[j, i] == "S")
                {
                    rooms.Add(Instantiate<GameObject>(platformPrefab, new Vector3(platformSpawnX, platformSpawnY, 0), Quaternion.identity));
                    startRoomPos = new Vector3(platformSpawnX, platformSpawnY, -1);
                }
                else if (PathGeneration.Instance.dungeon[j, i] != "")
                {
                    rooms.Add(Instantiate<GameObject>(roomPrefab, new Vector3(platformSpawnX, platformSpawnY, 0), Quaternion.identity));
                    Color a = rooms[rooms.Count - 1].GetComponent<Renderer>().material.color;
                    rooms[rooms.Count - 1].GetComponent<Renderer>().material.color = new Color(a.r, a.g, a.b, 0.5f);
                }

                platformSpawnX += platformWidth;
            }
            platformSpawnX = 0f;
            platformSpawnY -= platformHeight;

        }

        return rooms;
    }


    private void InitialPlayerSpawn()
    {
        Player.instance.transform.position = startRoomPos;

    }




}
