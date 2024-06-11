using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class SpriteAnimationControl_Humanoid : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IEquipments> equipments;

    [SerializeField] private SpriteRenderer baseHeadSpriteRenderer;
    [SerializeField] private SpriteRenderer baseBodySpriteRenderer;

    [SerializeField] private SpriteRenderer headSpriteRenderer;
    [SerializeField] private SpriteRenderer bodySpriteRenderer;

    private SpriteArray[] baseHeadAnimationArray = null;
    private SpriteArray[] baseBodyAnimationArray = null;

    private SpriteArray[] headAnimationArray = null;
    private SpriteArray[] bodyAnimationArray = null;

    private int animationIndex;
    private int frameIndex;

    private void Awake()
    {
        equipments.Value.OnEquip += Equipments_OnEquip;
        equipments.Value.OnUnEquip += Equipments_OnUnEquip;
    }

    private void Equipments_OnUnEquip(OnUnEquipEventArgs args)
    {
        if (args.equipData.GetEquipType() == GameResources.Instance.head_Equip)
        {
            headAnimationArray = null;
            baseHeadSpriteRenderer.enabled = true;
            headSpriteRenderer.enabled = false;
            return;
        }

        if (args.equipData.GetEquipType() == GameResources.Instance.body_Equip)
        {
            bodyAnimationArray = null;
            baseBodySpriteRenderer.enabled = true;
            bodySpriteRenderer.enabled = false;
            return;
        }

        UpdateFrame(frameIndex);
    }

    private void Equipments_OnEquip(OnEquipEventArgs args)
    {
        if (args.equipData.GetEquipType() == GameResources.Instance.head_Equip)
        {
            ArmorDetailsSO armorDetails = args.equipData.GetArmorDetails(); 

            // Set sprite aniamtion
            headAnimationArray = armorDetails.spriteAnimationArray;

            // enalble sprite renderer
            headSpriteRenderer.enabled = true;

            // Disable base sprite renderer 
            if (!armorDetails.enableBaseSprite)
                baseHeadSpriteRenderer.enabled = false;

            return;
        }

        if (args.equipData.GetEquipType() == GameResources.Instance.body_Equip)
        {
            ArmorDetailsSO armorDetails = args.equipData.GetArmorDetails();

            // Set sprite aniamtion
            bodyAnimationArray = armorDetails.spriteAnimationArray;

            // enalble sprite renderer
            bodySpriteRenderer.enabled = true;

            // Disable base sprite renderer 
            if (!armorDetails.enableBaseSprite)
                baseBodySpriteRenderer.enabled = false;

            return;
        }

        UpdateFrame(frameIndex);
    }

    public void SetUp(SpriteArray[] headAnimationArray, SpriteArray[] bodyAnimationArray)
    {
        baseHeadAnimationArray = headAnimationArray;
        baseBodyAnimationArray = bodyAnimationArray;

        baseHeadSpriteRenderer.enabled = true;
        baseBodySpriteRenderer.enabled = true;
        headSpriteRenderer.enabled = false;
        bodySpriteRenderer.enabled = false;

        UpdateFrame(frameIndex);
    }

    // Called by animation clip
    public void UpdateAnimation(int animationIndex)
    {
        this.animationIndex = animationIndex;
        UpdateFrame(frameIndex);
    }

    // Called by animation clip
    public void UpdateFrame(int frameIndex)
    {
        if (baseHeadAnimationArray != null)
        {
            if (!IsAnimationPlayable(baseHeadAnimationArray, animationIndex, frameIndex))
                return;

            baseHeadSpriteRenderer.sprite = baseHeadAnimationArray[animationIndex].spriteArray[frameIndex];
        }

        if (baseBodyAnimationArray != null)
        {
            if (!IsAnimationPlayable(baseBodyAnimationArray, animationIndex, frameIndex))
                return;

            baseBodySpriteRenderer.sprite = baseBodyAnimationArray[animationIndex].spriteArray[frameIndex];
        }

        if (headAnimationArray != null)
        {
            if (!IsAnimationPlayable(headAnimationArray, animationIndex, frameIndex))
                return;

            headSpriteRenderer.sprite = headAnimationArray[animationIndex].spriteArray[frameIndex];
        }

        if (bodyAnimationArray != null)
        {
            if (!IsAnimationPlayable(bodyAnimationArray, animationIndex, frameIndex))
                return;

            bodySpriteRenderer.sprite = bodyAnimationArray[animationIndex].spriteArray[frameIndex];
        }

        this.frameIndex = frameIndex;
    }

    private bool IsAnimationPlayable(SpriteArray[] animationArray, int animationIndex, int frameIndex)
    {
        if (animationIndex > animationArray.Length - 1)
        {
            Debug.Log("animation out of index");
            return false;
        }

        if (frameIndex > animationArray[animationIndex].spriteArray.Length - 1)
        {
            Debug.Log("frame out of index");
            return false;
        }

        return true;
    }
}
