using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = DungeonRoomModules.Node;
using roomTypes = DungeonRoomModules.roomTypes;

public static class FlowPatterns
{
    public static List<List<Node>> flows = new List<List<Node>>{
        new List<Node>
        {
            new Node(0, roomTypes.Entrance, new List<int> { 1}),
            new Node(1, roomTypes.Normal, new List<int> { 2, 0}),
            new Node(2, roomTypes.Normal, new List<int> { 3, 1}),
            new Node(3, roomTypes.Hub, new List < int > { 4, 6, 2}),
            new Node(4, roomTypes.Normal, new List < int > { 3, 5}),
            new Node(5, roomTypes.Reward, new List < int > { 4, 1}),
            new Node(6, roomTypes.Normal, new List < int > { 7, 3}),
            new Node(7, roomTypes.Hub, new List < int > { 8, 10, 6}),
            new Node(8, roomTypes.Normal, new List < int > { 9, 7}),
            new Node(9, roomTypes.Shop, new List < int > { 8 }),
            new Node(10, roomTypes.Normal, new List < int > { 11, 7}),
            new Node(11, roomTypes.Normal, new List < int > { 10, 12}),
            new Node(12, roomTypes.Reward, new List < int > { 7, 11}),
        }
    };
}
