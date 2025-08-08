using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomStateManager : MonoBehaviour
{

    private GameObject _currentRoom;
    private roomStatesEnum roomState;

    private UnvisitedRoomState unvisitedRoomState;
    private FightingRoomState fightingRoomState;
    private FinishedRoomState finishedRoomState;

    private enum roomStatesEnum
    {
        unvisited,
        fighting,
        finished
    }

    private void Awake()
    {
        unvisitedRoomState = new UnvisitedRoomState();
        fightingRoomState = new FightingRoomState();
        finishedRoomState = new FinishedRoomState();


        _currentRoom = this.gameObject;

        PlayerRoomObserver.Instance.OnEnteredANewRoom += RoomObserver_OnEnteredANewRoom;
        PlayerRoomObserver.Instance.OnExitedARoom += RoomObserver_OnExitedARoom;

        SetRoomState(roomStatesEnum.unvisited);
    }


    private void Update()
    {
        if (roomState == roomStatesEnum.unvisited)
            unvisitedRoomState.UpdateState(_currentRoom);
        if (roomState == roomStatesEnum.fighting)
            finishedRoomState.UpdateState(_currentRoom);
        if (roomState == roomStatesEnum.finished)
            finishedRoomState.UpdateState(_currentRoom);
        
    }


    private void SetRoomState(roomStatesEnum newRoomState)
    {
        roomStatesEnum currentState = roomState;
        roomState = newRoomState;

        switch (roomState)
        {
            case roomStatesEnum.unvisited:
                unvisitedRoomState.OnLeaveState(_currentRoom);  unvisitedRoomState.OnEnterState(_currentRoom); break;

            case roomStatesEnum.fighting:
                fightingRoomState.OnLeaveState(_currentRoom); fightingRoomState.OnEnterState(_currentRoom); break;

            case roomStatesEnum.finished:
                finishedRoomState.OnLeaveState(_currentRoom); finishedRoomState.OnEnterState(_currentRoom); break;

        }
    }


    private void RoomObserver_OnExitedARoom(GameObject obj)
    {
        if (obj != _currentRoom)
            return;

        SetRoomState(roomStatesEnum.finished);
    }

    private void RoomObserver_OnEnteredANewRoom(GameObject obj)
    {
        if (obj != _currentRoom)
            return;

        SetRoomState(roomStatesEnum.fighting);
    }

}
