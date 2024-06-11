using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField] private TagData tagData;

    public bool CompareTag(TagSO tagToCompare)
    {
        for (int i = 0; i < tagData.selfTagList.Count; i++)
        {
            if (tagToCompare == tagData.selfTagList[i])
                return true;
        }

        return false;
    }

    public bool CompareTag(List<TagSO> tagToCompairList)
    {
        for (int i = 0; i < tagData.selfTagList.Count; i++)
        {
            for (int j = 0; j < tagToCompairList.Count; j++)
            {
                if (tagToCompairList[j] == tagData.selfTagList[i])
                    return true;
            }
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtils.ValidateCheckNullValue(this, nameof(tagData), tagData);
    }
#endif
}
