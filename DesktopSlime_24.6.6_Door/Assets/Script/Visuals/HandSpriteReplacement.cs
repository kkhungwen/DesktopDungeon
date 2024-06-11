using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class HandSpriteReplacement : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IEquipments> equipments;

    [SerializeField] private SpriteRenderer[] handSpriteArray;

    private Sprite baseHandSprite;

    private void Awake()
    {
        equipments.Value.OnEquip += Equipments_OnEquip;
        equipments.Value.OnUnEquip += Equipments_OnUnEquip;
    }

    private void Equipments_OnUnEquip(OnUnEquipEventArgs args)
    {
        if (args.equipData.GetEquipType() != GameResources.Instance.body_Equip)
            return;

        foreach (SpriteRenderer spriteRenderer in handSpriteArray)
        {
            spriteRenderer.sprite = baseHandSprite;
        }
    }

    private void Equipments_OnEquip(OnEquipEventArgs args)
    {
        if (args.equipData.GetEquipType() != GameResources.Instance.body_Equip)
            return;

        ArmorDetailsSO armorDetails = args.equipData.GetArmorDetails();

        if (armorDetails.isReplaceHandSprite)
            ReplaceHandSprite(armorDetails.handSprite);
    }

    public void SetUp(Sprite handSprite)
    {
        baseHandSprite = handSprite;

        ReplaceHandSprite(handSprite);
    }

    private void ReplaceHandSprite(Sprite handSprite)
    {
        foreach (SpriteRenderer spriteRenderer in handSpriteArray)
        {
            spriteRenderer.sprite = handSprite;
        }
    }
}
