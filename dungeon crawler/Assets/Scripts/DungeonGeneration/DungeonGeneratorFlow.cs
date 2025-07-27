using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Node = DungeonRoomModules.Node;
using roomSide = DungeonRoomModules.roomSide;
using roomTypes = DungeonRoomModules.roomTypes;

public class DungeonGeneratorFlow : MonoBehaviour
{
    [SerializeField] private List<GameObject> normalRooms;
    [SerializeField] private List<GameObject> rewardRooms;
    [SerializeField] private List<GameObject> hubRooms;
    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject shop;

    

    private void Start()
    {
        List<Node> flow = FlowPatterns.flows[UnityEngine.Random.Range(0, FlowPatterns.flows.Count)];

        Graph newGraph = new Graph();
        foreach (Node node in flow)
        {
            foreach (int connection in node.connectingIDs)
            {
                newGraph.AddEdge(node.nodeID, connection);
            }

        }



        RenderDungeon(flow);
       
    }


    private void RenderDungeon(List<Node> flow)
    {
        List<GameObject> renderedRooms = new List<GameObject>();
        Vector3 pos = new Vector3(0f, 0f, 0f);
        Dictionary<roomSide, Vector3> additionPos = new Dictionary<roomSide, Vector3>
        {
            { roomSide.Top, new Vector3(0f, 1f, 0f) },
            { roomSide.Left ,new Vector3(-1f, 0f, 0f) },
            { roomSide.Bottom ,new Vector3(0f, -1f, 0f) },
            { roomSide.Right ,new Vector3(1f, 0f, 0f) },
        };
        Dictionary<int, Vector3> nodePositions = new Dictionary<int, Vector3>();
        nodePositions.Add(0, pos);

        foreach (Node room in flow)
        {
            GameObject newRoom = null;
            if (room.room_type == roomTypes.Entrance)
            {
                newRoom = Instantiate<GameObject>(entrance, pos, Quaternion.identity);
            }
            else if (room.room_type == roomTypes.Normal)
            {
                newRoom = Instantiate<GameObject>(normalRooms[Random.Range(0, normalRooms.Count)], pos, Quaternion.identity);
            }
            else if (room.room_type == roomTypes.Reward)
            {
                newRoom = Instantiate<GameObject>(rewardRooms[Random.Range(0, rewardRooms.Count)], pos, Quaternion.identity);
            }
            else if (room.room_type == roomTypes.Hub)
            {
                newRoom = Instantiate<GameObject>(hubRooms[Random.Range(0, hubRooms.Count)], pos, Quaternion.identity);
            }
            else if (room.room_type == roomTypes.Shop)
            {
                newRoom = Instantiate<GameObject>(shop, pos, Quaternion.identity);
            }

            renderedRooms.Add(newRoom);
            pos += new Vector3(UnityEngine.Random.Range(-25, 25), UnityEngine.Random.Range(0, 25), 0);


            List<int> connectingNodes = room.connectingIDs;
            foreach (int connectingNode in connectingNodes)
            {
                if (connectingNode < renderedRooms.Count)
                {
                    RenderLine(newRoom.transform.position, renderedRooms[connectingNode].transform.position);
                }
            }
        }
    }


    private void RenderLine(Vector3 x, Vector3 y)
    {

        //For creating line renderer object
        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, new Vector3(x.x, x.y, 2)); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, new Vector3(y.x, y.y, 2));
    }

}
