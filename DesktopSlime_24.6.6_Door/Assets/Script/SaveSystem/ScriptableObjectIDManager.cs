using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectIDManager : MonoBehaviour
{
    [SerializeField] private ScriptableObjectIDListSO scriptableObjectIdList;
    private Dictionary<int, ScriptableObject> idSoDic;
    private Dictionary<ScriptableObject, int> soIdDic;

    private void Awake()
    {
        CreateDic();
    }

    private void CreateDic()
    {
        idSoDic = new Dictionary<int, ScriptableObject>();

        foreach (ScriptableObjectID scriptableObjectID in scriptableObjectIdList.scriptableObjectIDList)
        {
            if (!idSoDic.TryAdd(scriptableObjectID.id, scriptableObjectID.scriptableObject))
                Debug.Log("duplicate id");
        }

        soIdDic = new Dictionary<ScriptableObject, int>();

        foreach (ScriptableObjectID scriptableObjectID in scriptableObjectIdList.scriptableObjectIDList)
        {
            if (!soIdDic.TryAdd(scriptableObjectID.scriptableObject, scriptableObjectID.id))
                Debug.Log("duplicate id");
        }
    }

    public T GetScriptableObject<T>(int id) where T : class
    {
        if (!idSoDic.TryGetValue(id, out ScriptableObject scriptableObject))
            Debug.Log("does not contain id " + id + " in dictionary");

        T obj = scriptableObject as T;
        if (obj == null)
            Debug.Log("Cannot cast " + scriptableObject + " to " + nameof(T));

        return obj;
    }

    public int GetScriptableObjectID(ScriptableObject scriptableObject)
    {
        if(!soIdDic.TryGetValue(scriptableObject,out int id))
            Debug.Log("does not contain scriptableobject " + scriptableObject + " in dictionary");

        return id;
    }
}
