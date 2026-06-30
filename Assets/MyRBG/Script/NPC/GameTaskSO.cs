using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameTaskState
{
    Waiting,
    Executing,
    Completed,
    End
}
[CreateAssetMenu()]
public class GameTaskSO :ScriptableObject
{
    public GameTaskState state;

    public string[] diague;

    public ItemScriptObject startReward;
    public ItemScriptObject endReward;
}
