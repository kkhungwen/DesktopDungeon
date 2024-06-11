using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaHitbox : MonoBehaviour
{
    [SerializeField] private TagData tagData;
    [SerializeField] private LayerMask HurtBoxLayerMask;
    [SerializeField] private float hitBoxHeight;
    private ContactFilter2D areaContactFilter;

    private BoxCollider2D areaHitBox;
    private List<Collider2D> overlapColliderList = new List<Collider2D>();
    private List<HurtBox> hurtboxList = new List<HurtBox>();

    private void Awake()
    {
        areaHitBox = GetComponent<BoxCollider2D>();

        areaContactFilter.SetLayerMask(HurtBoxLayerMask);
        areaContactFilter.useLayerMask = true;
    }

    public List<HurtBox> GetHurtBoxList(bool isHostile, bool isFriendly, float radious)
    {
        areaHitBox.size = new Vector2(radious * 2, hitBoxHeight);

        hurtboxList.Clear();
        overlapColliderList.Clear();
        areaHitBox.OverlapCollider(areaContactFilter, overlapColliderList);

        foreach (Collider2D overlapCollider in overlapColliderList)
        {
            if (!overlapCollider.TryGetComponent<Tags>(out Tags tags))
            {
                Debug.Log("hurtbox collider should contains tags component");
                continue;
            }

            if (isHostile && tags.CompareTag(tagData.hostileTagList))
            {
                if (!overlapCollider.TryGetComponent<HurtBox>(out HurtBox hurtBox))
                {
                    Debug.Log("hurtbox collider should contains hurtbox compoent");
                    continue;
                }

                hurtboxList.Add(hurtBox);
                continue;
            }

            if (isFriendly && tags.CompareTag(tagData.friendlyTagList))
            {
                if (!overlapCollider.TryGetComponent<HurtBox>(out HurtBox hurtBox))
                {
                    Debug.Log("hurtbox collider should contains hurtbox compoent");
                    continue;
                }

                hurtboxList.Add(hurtBox);
                continue;
            }
        }

        return hurtboxList;
    }
}