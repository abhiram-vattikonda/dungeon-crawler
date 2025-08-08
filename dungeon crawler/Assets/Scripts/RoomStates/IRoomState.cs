using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomState
{
    public void OnEnterState(GameObject obj);

    public void UpdateState(GameObject obj);
    public void OnLeaveState(GameObject obj);
}
