using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;


public class DungeonGeneratorGrind : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;


    private float platformHeight;
    private float platformWidth;


    private void Awake()
    {
        if (platformPrefab != null)
        {
            Renderer renderer = platformPrefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                platformWidth = renderer.bounds.size.x;
                platformHeight = renderer.bounds.size.y;
            }
        }
            
    }



    // Start is called before the first frame update
    void Start()
        {
            PathGeneration.Instance.GenerateDungeonMap();
            RenderMap();
        }

    private void RenderMap()
    {
    float platformSpawnX = 0f;
    float platformSpawnY = 0f;
        for (int i = 0; i < PathGeneration.Instance.dimensions.y; i++)
        {
            for(int j = 0; j <  PathGeneration.Instance.dimensions.x; j++)
            {
                if (PathGeneration.Instance.dungeon[j, i] != 0)
                {
                    Instantiate<GameObject>(platformPrefab, new Vector3(platformSpawnX, platformSpawnY, 0), Quaternion.identity);
                }
                platformSpawnX -= platformWidth;
            }
            platformSpawnX = 0f;
            platformSpawnY -= platformHeight;
        }
    }


}
