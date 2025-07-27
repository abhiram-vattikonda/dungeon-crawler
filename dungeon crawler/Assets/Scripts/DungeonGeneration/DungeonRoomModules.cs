using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomModules
{
    public enum roomTypes
    {
        Normal,
        Reward,
        Hub,
        Entrance,
        Shop,
        Boss
    }

    public enum roomSide
    {
        Bottom, Right, Top, Left
    }

    public class Node
    {
        public roomTypes room_type { get; }
        public int nodeID { get; }
        public List<int> connectingIDs { get; }
        public Node(int nodeNum, roomTypes roomtype, List<int> connections = null)
        {
            this.room_type = roomtype;
            this.nodeID = nodeNum;
            this.connectingIDs = connections ?? new List<int>();
            
        }
    }
}
