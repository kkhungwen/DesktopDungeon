using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TNRD;

[DisallowMultipleComponent]
public class Equipments_Humanoid : MonoBehaviour, IEquipments
{
    public event Action<OnEquipEventArgs> OnEquip;
    public event Action<OnUnEquipEventArgs> OnUnEquip;
    public event Action<List<AttributeValueData>> OnEquipmentsAttributeChange;
    public event Action<List<AffixTypeSO>> OnEquipmentsAffixChange;

    [SerializeField] private DropOn_Humanoid dropOn;

    private HumanoidEquipInventory inventory;


    private void Awake()
    {
        dropOn.OnDropOn += DropOn_OnDropOn;
    }

    private void OnDisable()
    {
        inventory.OnAdd -= Inventory_OnAdd;
        inventory.OnRemove -= Inventory_OnRemove;
    }

    public void SetUp(HumanoidData humanoidData)
    {
        inventory = humanoidData.equipInventory;

        inventory.OnAdd += Inventory_OnAdd;
        inventory.OnRemove += Inventory_OnRemove;


        if (inventory.TryGetEquipData(GameResources.Instance.weapon_Equip, out EquipData weaponEquip))
            Equip(weaponEquip);

        if (inventory.TryGetEquipData(GameResources.Instance.head_Equip, out EquipData headEquip))
            Equip(headEquip);

        if (inventory.TryGetEquipData(GameResources.Instance.weapon_Equip, out EquipData bodyEquip))
            Equip(bodyEquip);
    }

    private void Inventory_OnAdd(EquipData equipData)
    {
        Equip(equipData);
    }

    private void Inventory_OnRemove(EquipData equipData)
    {
        UnEquip(equipData);
    }

    private void DropOn_OnDropOn(OnDropOnEventArgs args)
    {
        if (args.equipData == null)
            return;

        EquipData unEquipData = null;

        if (args.equipData.GetEquipType() == GameResources.Instance.weapon_Equip)
        {
            if (inventory.TryGetEquipData(GameResources.Instance.weapon_Equip, out unEquipData))
            {
                inventory.TryRemoveEquipData(GameResources.Instance.weapon_Equip);
            }

            inventory.TryAddEquipData(args.equipData);
        }
        else if (args.equipData.GetEquipType() == GameResources.Instance.head_Equip)
        {
            if (inventory.TryGetEquipData(GameResources.Instance.head_Equip, out unEquipData))
            {
                inventory.TryRemoveEquipData(GameResources.Instance.head_Equip);
            }

            inventory.TryAddEquipData(args.equipData);
        }
        else if (args.equipData.GetEquipType() == GameResources.Instance.body_Equip)
        {
            if (inventory.TryGetEquipData(GameResources.Instance.body_Equip, out unEquipData))
            {
                inventory.TryRemoveEquipData(GameResources.Instance.body_Equip);
            }

            inventory.TryAddEquipData(args.equipData);
        }

        if (unEquipData != null)
            unEquipData.CreateInstantiatedObject(transform.position);
    }

    private void Equip(EquipData equipData)
    {
        OnEquip?.Invoke(new OnEquipEventArgs()
        {
            equipData = equipData
        });

        OnEquipmentsAttributeChange?.Invoke(GetEquipmentsAttributeList());
        OnEquipmentsAffixChange?.Invoke(GetEquipmentsAffixList());
    }

    private void UnEquip(EquipData equipData)
    {
        OnUnEquip?.Invoke(new OnUnEquipEventArgs()
        {
            equipData = equipData
        });

        OnEquipmentsAttributeChange?.Invoke(GetEquipmentsAttributeList());
        OnEquipmentsAffixChange?.Invoke(GetEquipmentsAffixList());
    }

    private List<AttributeValueData> GetEquipmentsAttributeList()
    {
        List<AttributeValueData> attributeValueDataList = new();

        if (inventory.TryGetEquipData(GameResources.Instance.weapon_Equip, out EquipData weaponData))
            foreach (AttributeValueData attributeValueData in weaponData.GetAttribute())
                AttributeValueData.AddAttributeDataToList(attributeValueDataList, attributeValueData);

        if (inventory.TryGetEquipData(GameResources.Instance.head_Equip, out EquipData headData))
            foreach (AttributeValueData attributeValueData in headData.GetAttribute())
                AttributeValueData.AddAttributeDataToList(attributeValueDataList, attributeValueData);

        if (inventory.TryGetEquipData(GameResources.Instance.body_Equip, out EquipData bodyData))
            foreach (AttributeValueData attributeValueData in bodyData.GetAttribute())
                AttributeValueData.AddAttributeDataToList(attributeValueDataList, attributeValueData);

        return attributeValueDataList;
    }

    private List<AffixTypeSO> GetEquipmentsAffixList()
    {
        List<AffixTypeSO> affixList = new();

        if (inventory.TryGetEquipData(GameResources.Instance.weapon_Equip, out EquipData weaponData))
            foreach (AffixTypeSO affixType in weaponData.GetAffix())
                affixList.Add(affixType);

        if (inventory.TryGetEquipData(GameResources.Instance.head_Equip, out EquipData headData))
            foreach (AffixTypeSO affixType in headData.GetAffix())
                affixList.Add(affixType);

        if (inventory.TryGetEquipData(GameResources.Instance.body_Equip, out EquipData bodyData))
            foreach (AffixTypeSO affixType in bodyData.GetAffix())
                affixList.Add(affixType);

        return affixList;
    }
}
