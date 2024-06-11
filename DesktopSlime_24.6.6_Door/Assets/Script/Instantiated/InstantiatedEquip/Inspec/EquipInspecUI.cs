using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EquipInspecUI : InspecUI
{
    [SerializeField] private RectTransform backGroundTransform;
    // required for transparent window to interact with ui
    [SerializeField] private BoxCollider2D clickCollider;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI equipAttributeText;
    [SerializeField] private TextMeshProUGUI bonusAttributeText;
    [SerializeField] private TextMeshProUGUI affixText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private LayoutElement nameLayout;
    [SerializeField] private LayoutElement equipLayout;
    [SerializeField] private LayoutElement bonusAttributeLayout;
    [SerializeField] private LayoutElement affixLayout;
    [SerializeField] private LayoutElement descriptionLayout;

    [SerializeField] private Image gemImage;
    [SerializeField] private Sprite[] gemSpriteArray;
    [SerializeField] private float inspecBoxWidth;

    public void UpdateUI(EquipData equipData)
    {
        nameLayout.preferredWidth = inspecBoxWidth;
        equipLayout.preferredWidth = inspecBoxWidth;
        bonusAttributeLayout.preferredWidth = inspecBoxWidth;
        descriptionLayout.preferredWidth = inspecBoxWidth;

        string text = equipData.equipDetails.GetName() + " +" + equipData.itemLevel;
        nameText.text = text;

        AttributeValueData equipAttributeData = equipData.GetEquipAttribute();
        text = System.Math.Round(equipAttributeData.value, 1) + "  " + equipAttributeData.attributeType.attributeName;
        equipAttributeText.text = text;

        List<AttributeValueData> bonusAttributeValueDataList = equipData.GetBonusAttributeList();
        text = "";
        foreach (AttributeValueData attributeValueData in bonusAttributeValueDataList)
        {
            text += " + " + attributeValueData.value + " " + attributeValueData.attributeType.attributeName + "\n";
        }
        bonusAttributeText.text = text;

        text = "";
        foreach (AffixTypeSO affix in equipData.GetAffix())
        {
            text += affix.name + "\n";
        }
        affixText.text = text;


        text = equipData.equipDetails.GetDescription();
        descriptionText.text = text;

        // need to force update because click collider size is dependent to the contantsizefitter component
        LayoutRebuilder.ForceRebuildLayoutImmediate(nameText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(equipAttributeText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(bonusAttributeText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(affixText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(backGroundTransform);

        clickCollider.size = backGroundTransform.sizeDelta;
        clickCollider.offset = new Vector2(0, clickCollider.size.y / 2);

        gemImage.sprite = gemSpriteArray[equipData.forgeLevel];
    }
}
