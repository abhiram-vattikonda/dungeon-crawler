using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class DungeonGenerator : MonoBehaviour
{

    [SerializeField] private GameObject rectPrefab;

    private float initalCircleRadius = 8f;
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
            allSquares.Add(InitializeSquare());
        }
        StartCoroutine(GetSortedRooms(allSquares));
        
    }

    private IEnumerator GetSortedRooms(List<GameObject> allSquares)
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Started disappering");
        List<GameObject> sortedSquares = allSquares.OrderBy(o => o.transform.localScale.x * o.transform.localScale.x).ToList();
        int percent = 16;
        for (int i = 0; i < sortedSquares.Count; i++)
        {
            if (i < (percent - 1) * sortedSquares.Count / percent)
                sortedSquares[i].SetActive(false);
        }
    }

    private GameObject InitializeSquare()
    {
        float size = 1f;
        float mean = 1f, std = 2f;

        Vector2 rectPos = GetRandomPositionInsideCircle(initalCircleRadius);
        GameObject rect = Instantiate<GameObject>(rectPrefab, rectPos, Quaternion.identity);
        float sizeMultiplier = GetNormalDistribution(mean, std);
        if (sizeMultiplier > 0)
            size *= sizeMultiplier;


        float height = size * UnityEngine.Random.Range(0.5f, 2f);
        float width = size*size / height;

        rect.transform.localScale = new Vector3(width, height, 1);

        return rect;
    }

    private Vector2 GetRandomPositionInsideCircle(float radius)
    {
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        float distance = Mathf.Sqrt(UnityEngine.Random.value) * radius;

        float x = distance * Mathf.Cos(angle);
        float y = distance * Mathf.Sin(angle);

        return new Vector2((float)Math.Round(x), (float)Math.Round(y));
    }

    private float GetNormalDistribution(float mean = 0f, float std = 1f)
    {
        float u = UnityEngine.Random.value; // uniform in [0, 1]
        return mean + std * Mathf.Sqrt(2) * InverseErf(2 * u - 1);
    }


    private float InverseErf(float x)
    {
        float a = 0.147f;
        float ln = Mathf.Log(1 - x * x);
        float firstPart = 2 / (Mathf.PI * a) + ln / 2;
        float secondPart = ln / a;
        return Mathf.Sign(x) * Mathf.Sqrt(Mathf.Sqrt(firstPart * firstPart - secondPart) - firstPart);
    }

}
