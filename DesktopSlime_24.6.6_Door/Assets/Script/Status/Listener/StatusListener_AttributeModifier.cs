using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusListener_AttributeModifier : MonoBehaviour
{
    [SerializeField] AttributeModifyer attributeModifyer;

    private StatusHolder statusManager;

    private Dictionary<StatusTypeSO, ModifyAttributeData[]> statusTypeModifyAttributeDataDic;

    private void Awake()
    {
        statusTypeModifyAttributeDataDic = new();

        statusManager = GetComponentInParent<StatusHolder>();

        statusManager.OnStatusChange += StatusManager_OnStatusChange;
    }

    private void StatusManager_OnStatusChange(StatusTypeSO statusType, int stack)
    {
        if (statusType.attributeModifyDataArray.Length <= 0)
            return;

        if (statusTypeModifyAttributeDataDic.ContainsKey(statusType))
            statusTypeModifyAttributeDataDic[statusType] = null;

        ModifyAttributeData[] modifyAttributeDataArray = new ModifyAttributeData[statusType.attributeModifyDataArray.Length];
        for (int i = 0; i < modifyAttributeDataArray.Length; i++)
        {
            AttributeModifyData attributeModifyData = statusType.attributeModifyDataArray[i];
            modifyAttributeDataArray[i] = new ModifyAttributeData(attributeModifyData.attributeType, attributeModifyData.isAdd, attributeModifyData.value * stack);
        }

        statusTypeModifyAttributeDataDic[statusType] = modifyAttributeDataArray;

        List<ModifyAttributeData> modifyAttributeDataList = new();
        foreach (KeyValuePair<StatusTypeSO, ModifyAttributeData[]> pair in statusTypeModifyAttributeDataDic)
        {
            foreach (ModifyAttributeData modifyAttributeData in pair.Value)
            {
                modifyAttributeDataList.Add(modifyAttributeData);
            }
        }

        attributeModifyer.UpdateModifyAttribute(this, modifyAttributeDataList);
    }
}
