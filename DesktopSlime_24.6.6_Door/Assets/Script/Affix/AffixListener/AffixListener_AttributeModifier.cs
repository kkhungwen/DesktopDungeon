using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffixListener_AttributeModifier : MonoBehaviour
{
    [SerializeField] AttributeModifyer attributeModifyer;

    private AffixHolder affixHolder;

    private Dictionary<AffixTypeSO, ModifyAttributeData[]> affixTypeModifyAttributeDataAttayDic;

    private void Awake()
    {
        affixTypeModifyAttributeDataAttayDic = new();

        affixHolder = GetComponentInParent<AffixHolder>();

        affixHolder.OnAffixUpdate += AffixHolder_OnAffixUpdate;
    }

    private void AffixHolder_OnAffixUpdate(AffixTypeSO affixType, int count)
    {
        if (affixType.attributeModifyDataArray.Length <= 0)
            return;

        if (affixTypeModifyAttributeDataAttayDic.ContainsKey(affixType))
            affixTypeModifyAttributeDataAttayDic[affixType] = null;

        ModifyAttributeData[] modifyAttributeDataArray = new ModifyAttributeData[affixType.attributeModifyDataArray.Length];
        for (int i = 0; i < modifyAttributeDataArray.Length; i++)
        {
            AttributeModifyData attributeModifyData = affixType.attributeModifyDataArray[i];
            modifyAttributeDataArray[i] = new ModifyAttributeData(attributeModifyData.attributeType, attributeModifyData.isAdd, attributeModifyData.value * count);
        }

        affixTypeModifyAttributeDataAttayDic[affixType] = modifyAttributeDataArray;

        List<ModifyAttributeData> modifyAttributeDataList = new();
        foreach (KeyValuePair<AffixTypeSO, ModifyAttributeData[]> pair in affixTypeModifyAttributeDataAttayDic)
        {
            foreach (ModifyAttributeData modifyAttributeData in pair.Value)
            {
                modifyAttributeDataList.Add(modifyAttributeData);
            }
        }

        attributeModifyer.UpdateModifyAttribute(this, modifyAttributeDataList);
    }
}
