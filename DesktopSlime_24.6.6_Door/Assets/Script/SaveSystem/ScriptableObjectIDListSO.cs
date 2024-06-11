using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectIDListSO_",menuName ="Scriptable Objects/Scriptable Object ID List")]
public class ScriptableObjectIDListSO : ScriptableObject
{
    public List<ScriptableObjectID> scriptableObjectIDList = new List<ScriptableObjectID>();
}

[System.Serializable]
public class ScriptableObjectID
{
    public ScriptableObject scriptableObject;
    public int id;
}

