using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagData : MonoBehaviour
{
    [field: SerializeField] public List<TagSO> selfTagList { get; private set; }

    [field: SerializeField] public List<TagSO> friendlyTagList { get; private set; }

    [field: SerializeField] public List<TagSO> hostileTagList { get; private set; }

    [field: SerializeField] public List<TagSO> unpassableTagList { get; private set; }

    public void AddSelfTag(TagSO tag)
    {
        if (selfTagList.Contains(tag))
        {
            Debug.Log(selfTagList + " already contains " + tag);
            return;
        }

        selfTagList.Add(tag);
    }

    public void AddFriendlyTag(TagSO tag)
    {
        if (friendlyTagList.Contains(tag))
        {
            Debug.Log(friendlyTagList + " already contains " + tag);
            return;
        }

        friendlyTagList.Add(tag);
    }

    public void AddHostileTag(TagSO tag)
    {
        if (hostileTagList.Contains(tag))
        {
            Debug.Log(hostileTagList + " already contains " + tag);
            return;
        }

        hostileTagList.Add(tag);
    }

    public void RemoveSelfTag(TagSO tag)
    {
        if (!selfTagList.Contains(tag))
        {
            Debug.Log(selfTagList + " doesn't contains " + tag);
            return;
        }

        selfTagList.Remove(tag);
    }
}
