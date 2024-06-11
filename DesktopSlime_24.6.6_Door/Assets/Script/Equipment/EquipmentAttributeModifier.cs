using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class EquipmentAttributeModifier : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IEquipments> equipments;
    [SerializeField] private AttributeModifyer attributeModifyer;

    private void Awake()
    {
        equipments.Value.OnEquipmentsAttributeChange += Equipments_OnEquipmentsAttributeChange;
    }

    private void Equipments_OnEquipmentsAttributeChange(List<AttributeValueData> attributeValueDataList)
    {
        List<ModifyAttributeData> modifyAttributeDataList = new();

        foreach (AttributeValueData attributeValueData in attributeValueDataList)
        {
            ModifyAttributeData modifyAttributeData = new ModifyAttributeData(attributeValueData.attributeType, true, attributeValueData.value);
            modifyAttributeDataList.Add(modifyAttributeData);
        }

        attributeModifyer.UpdateModifyAttribute(this, modifyAttributeDataList);
    }
}
