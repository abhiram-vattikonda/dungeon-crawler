using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField] private GameObject rect;

    private float initalCircleRadius = 64f;
    private int initalRectNumber = 128;

    private void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        List<GameObject> allSquares = new List<GameObject>();
        for (int i = 0; i < initalRectNumber; i++)
        {
            Vector2 rectPos = GetRandomPostionInsideCircle(initalCircleRadius);
            allSquares.Add(Instantiate<GameObject>(rect, rectPos, Quaternion.identity));

        }

    }

    private Vector2 GetRandomPostionInsideCircle(float radius)
    {
        float t = 2 * Mathf.PI * Random.value;
        float u = Random.value + Random.value;
        float r = 0;
        if (u > 1)
            r = 2 - u;

        else
            r = u;

            return new Vector2(r * Mathf.Sin(t), r * Mathf.Cos(t));
    }





}
