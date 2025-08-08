using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnvisitedRoomState : MonoBehaviour, IRoomState
{

    public void OnEnterState(GameObject obj)
    {
        Color a = obj.GetComponent<Renderer>().material.color;
        obj.GetComponent<Renderer>().material.color = new Color(a.r, a.g, a.b, 0.3f);
    }

    public void OnLeaveState(GameObject obj)
    {

    }

    public void UpdateState(GameObject obj)
    {
        
    }

}
