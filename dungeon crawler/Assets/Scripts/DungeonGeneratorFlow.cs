using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DungeonGeneratorFlow : MonoBehaviour
{
    [SerializeField] private List<GameObject> normalRooms;
    [SerializeField] private List<GameObject> rewardRooms;
    [SerializeField] private List<GameObject> hubRooms;
    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject shop;

    enum roomTypes
    {
        Normal,
        Reward,
        Hub,
        Entrance,
        Shop,
        Boss
    }

    enum roomSide
    {
        Bottom, Right, Top, Left
    }

    class Node
    {
        public roomTypes room_type { get; }
        public int nodeID { get; }
        public Dictionary<roomSide, int> connectingIDs { get; }
        public Node(int nodeNum, roomTypes roomtype, List<int> connections = null)
        {
            this.room_type = roomtype;
            this.nodeID = nodeNum;
            int sideCount = 0;
            for (int i = 0; i < connections.Count; i++)
            {
                connectingIDs.Add((roomSide)sideCount, connections[i]);
            }
        }
    }

    private List<Node> flow = new List<Node>
    {
        new Node(0, roomTypes.Entrance, new List<int> { 1, -1, -1, -1}),
        new Node(1, roomTypes.Normal, new List<int> { 2, -1, 0, -1}),
        new Node(2, roomTypes.Normal, new List<int> { 3, -1, 1, -1}),
        new Node(3, roomTypes.Hub, new List < int > { 4, 6, 2, -1}),
        new Node(4, roomTypes.Normal, new List < int > { -1, -1, 3, 5}),
        new Node(5, roomTypes.Reward, new List < int > { -1, 4, 1, -1}),
        new Node(6, roomTypes.Normal, new List < int > { -1, 7, -1, 3}),
        new Node(7, roomTypes.Hub, new List < int > { 8, 10, -1, 6}),
        new Node(8, roomTypes.Normal, new List < int > { 9, -1, 8, -1}),
        new Node(9, roomTypes.Shop, new List < int > { -1, -1, 8, -1}),
        new Node(10, roomTypes.Normal, new List < int > { -1, -1, 11, 7}),
        new Node(11, roomTypes.Normal, new List < int > { 10, -1, -1, 12}),
        new Node(12, roomTypes.Reward, new List < int > { 7, 11, -1, -1}),
    };

    private void Start()
    {

        List<GameObject> renderedRooms = new List<GameObject>();
        Vector3 pos = new Vector3 (0f, 0f, 0f);
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
                newRoom= Instantiate<GameObject>(normalRooms[Random.Range(0, normalRooms.Count)], pos, Quaternion.identity);
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

            Dictionary<roomSide, int> connectingNodes = room.connectingIDs;
            foreach (int key in connectingNodes.Keys)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (connectingNodes[(roomSide)i] != -1)
                    {

                    }
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
