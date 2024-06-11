using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

[DisallowMultipleComponent]
public class BattleVision : MonoBehaviour
{
    // Ouput data
    public bool isEnemyInVision { get; private set; }
    public bool isTargetEnemyRight { get; private set; }

    // Debug
    [SerializeField] private bool isDebug;

    // Required reference
    [Space(10f)]
    [Header("REQUIRED REFERENCE")]
    [SerializeField] private TagData tagData;
    [SerializeField] private Vision vision;
    [SerializeField] private SerializableInterface<IWeapon> weapon;
    [SerializeField] private BoxCollider2D collisionCollider;


    // Private configurables
    private float moveToSoftRange = 0.1f;
    private float collisionSoftRange = 0.01f;

    // Privte parameters
    private VisibleObject currentTargetEnemy;
    private float self_HalfColliderSize => collisionCollider.size.x / 2 + collisionSoftRange;
    private float min_XpreferredPosition = 0;
    private float max_XpreferredPosition = 0;

    private void OnEnable()
    {
        vision.OnUpdateVision += Vision_OnUpdateVision;
    }

    private void OnDisable()
    {
        vision.OnUpdateVision -= Vision_OnUpdateVision;
    }

    private void Vision_OnUpdateVision(UpdateVisionEventArgs eventArgs)
    {
        UpdateBattleDecisions(eventArgs.right_VisibleObjectList, eventArgs.left_VisibleObjectList);
    }

    private void UpdateBattleDecisions(List<VisibleObject> rightVisibleObjectList, List<VisibleObject> leftVisibleObjectList)
    {
        isEnemyInVision = false;
        isTargetEnemyRight = false;

        VisibleObject rightEnemy = null;
        VisibleObject leftEnemy = null;
        VisibleObject rightUnpassable = null;
        VisibleObject leftUnpassable = null;

        // Loop through right raycast hit
        foreach (VisibleObject visibleObject in rightVisibleObjectList)
        {
            // If raycast hit enemy, Set unpassable and enemy, break
            if (visibleObject.tags.CompareTag(tagData.hostileTagList) && !visibleObject.tags.CompareTag(GameResources.Instance.dead_Tag))
            {
                rightEnemy = visibleObject;
                rightUnpassable = visibleObject;

                break;
            }

            // If raycast hit obstacle , Set unpassable, break
            if (visibleObject.tags.CompareTag(tagData.unpassableTagList) && !visibleObject.tags.CompareTag(GameResources.Instance.dead_Tag))
            {
                rightUnpassable = visibleObject;

                break;
            }
        }

        // Loop through left raycast hit
        foreach (VisibleObject visibleObject in leftVisibleObjectList)
        {
            // If raycast hit enemy, Set unpassable and enemy, break
            if (visibleObject.tags.CompareTag(tagData.hostileTagList) && !visibleObject.tags.CompareTag(GameResources.Instance.dead_Tag))
            {
                leftEnemy = visibleObject;
                leftUnpassable = visibleObject;

                break;
            }

            // If raycast hit obstacle , Set unpassable, break
            if (visibleObject.tags.CompareTag(tagData.unpassableTagList) && !visibleObject.tags.CompareTag(GameResources.Instance.dead_Tag))
            {
                leftUnpassable = visibleObject;

                break;
            }
        }


        // If no enemy detected
        if (rightEnemy == null && leftEnemy == null)
        {
            isEnemyInVision = false;

            currentTargetEnemy = null;

            if (isDebug)
                Debug.Log("no enemy");
        }

        // If right enemy detected and no left unpassable collider detected
        else if (rightEnemy != null && leftUnpassable == null)
        {
            isEnemyInVision = true;
            isTargetEnemyRight = true;

            currentTargetEnemy = rightEnemy;

            min_XpreferredPosition = rightEnemy.point.x - self_HalfColliderSize - weapon.Value.GetWeaponDetails().preferredDistance;
            max_XpreferredPosition = min_XpreferredPosition + weapon.Value.GetWeaponDetails().preferrdDistanceRange;


            if (isDebug)
                Debug.Log("right enemy no left unpassable");
        }

        // If left enemy detected and no right unpassable collider detected
        else if (leftEnemy != null && rightUnpassable == null)
        {
            isEnemyInVision = true;
            isTargetEnemyRight = false;

            currentTargetEnemy = leftEnemy;

            max_XpreferredPosition = leftEnemy.point.x + self_HalfColliderSize + weapon.Value.GetWeaponDetails().preferredDistance;
            min_XpreferredPosition = max_XpreferredPosition - weapon.Value.GetWeaponDetails().preferrdDistanceRange;

            if (isDebug)
                Debug.Log("left enemy no right unpassable");
        }

        // If right enemy detected and left unpassable collider detected
        else if (rightEnemy != null && leftUnpassable != null && leftEnemy == null)
        {
            isEnemyInVision = true;
            isTargetEnemyRight = true;

            currentTargetEnemy = rightEnemy;

            min_XpreferredPosition = rightEnemy.point.x - self_HalfColliderSize - weapon.Value.GetWeaponDetails().preferredDistance;

            if (min_XpreferredPosition < leftUnpassable.point.x + self_HalfColliderSize)
                min_XpreferredPosition = leftUnpassable.point.x + self_HalfColliderSize;

            max_XpreferredPosition = min_XpreferredPosition + weapon.Value.GetWeaponDetails().preferrdDistanceRange;

            if (max_XpreferredPosition > rightEnemy.point.x - self_HalfColliderSize)
                max_XpreferredPosition = rightEnemy.point.x - self_HalfColliderSize;

            if (min_XpreferredPosition > max_XpreferredPosition)
            {
                min_XpreferredPosition = transform.position.x;
                max_XpreferredPosition = transform.position.x;
            }
        }

        // If left enemy detected and right unpassable collider detected
        else if (leftEnemy != null && rightUnpassable != null && rightEnemy == null)
        {
            isEnemyInVision = true;
            isTargetEnemyRight = false;

            currentTargetEnemy = leftEnemy;

            max_XpreferredPosition = leftEnemy.point.x + self_HalfColliderSize + weapon.Value.GetWeaponDetails().preferredDistance;

            if (max_XpreferredPosition > rightUnpassable.point.x - self_HalfColliderSize)
                max_XpreferredPosition = rightUnpassable.point.x - self_HalfColliderSize;

            min_XpreferredPosition = max_XpreferredPosition - weapon.Value.GetWeaponDetails().preferrdDistanceRange;

            if (min_XpreferredPosition < leftEnemy.point.x + self_HalfColliderSize)
                min_XpreferredPosition = leftEnemy.point.x + self_HalfColliderSize;

            if (min_XpreferredPosition > max_XpreferredPosition)
            {
                min_XpreferredPosition = transform.position.x;
                max_XpreferredPosition = transform.position.x;
            }
        }

        // If right and left enemy detected
        else if (rightEnemy != null && leftEnemy != null)
        {
            // If current target is null, Set the closest enemy as target
            if (currentTargetEnemy == null)
            {
                if (rightEnemy.distance <= leftEnemy.distance)
                    currentTargetEnemy = rightEnemy;
                else
                    currentTargetEnemy = leftEnemy;
            }
            // If current target is not right or left enemy, Set the closest enemy as target
            else if (currentTargetEnemy.transform != rightEnemy.transform && currentTargetEnemy.transform != leftEnemy.transform)
            {
                if (rightEnemy.distance <= leftEnemy.distance)
                    currentTargetEnemy = rightEnemy;
                else
                    currentTargetEnemy = leftEnemy;
            }

            // If current target enemy is right enemy
            else if (currentTargetEnemy.transform == rightEnemy.transform)
            {
                isEnemyInVision = true;
                isTargetEnemyRight = true;

                currentTargetEnemy = rightEnemy;

                min_XpreferredPosition = rightEnemy.point.x - self_HalfColliderSize - weapon.Value.GetWeaponDetails().preferredDistance;
                float rightLeftEnemyCenterPoint = (rightEnemy.point.x + leftEnemy.point.x) / 2;

                if (min_XpreferredPosition < rightLeftEnemyCenterPoint)
                    min_XpreferredPosition = rightLeftEnemyCenterPoint;

                max_XpreferredPosition = min_XpreferredPosition + weapon.Value.GetWeaponDetails().preferrdDistanceRange;

                if (max_XpreferredPosition > rightEnemy.point.x - self_HalfColliderSize)
                    max_XpreferredPosition = rightEnemy.point.x - self_HalfColliderSize;

                if (min_XpreferredPosition > max_XpreferredPosition)
                {
                    min_XpreferredPosition = transform.position.x;
                    max_XpreferredPosition = transform.position.x;
                }
            }

            // If current target enemy is left enemy
            else if (currentTargetEnemy.transform == leftEnemy.transform)
            {
                isEnemyInVision = true;
                isTargetEnemyRight = false;

                currentTargetEnemy = leftEnemy;

                max_XpreferredPosition = leftEnemy.point.x + self_HalfColliderSize + weapon.Value.GetWeaponDetails().preferredDistance;
                float rightLeftEnemyCenterPoint = (rightEnemy.point.x + leftEnemy.point.x) / 2;

                if (max_XpreferredPosition > rightLeftEnemyCenterPoint)
                    max_XpreferredPosition = rightLeftEnemyCenterPoint;

                min_XpreferredPosition = max_XpreferredPosition - weapon.Value.GetWeaponDetails().attackRange;

                if (min_XpreferredPosition < leftEnemy.point.x + self_HalfColliderSize)
                    min_XpreferredPosition = leftEnemy.point.x + self_HalfColliderSize;

                if (min_XpreferredPosition > max_XpreferredPosition)
                {
                    min_XpreferredPosition = transform.position.x;
                    max_XpreferredPosition = transform.position.x;
                }
            }
        }

        Debug_DrawMinMaxXPosition(min_XpreferredPosition, max_XpreferredPosition, collisionCollider.size.y / 2);
        Debug_DrawTargetXPosition((min_XpreferredPosition + max_XpreferredPosition) / 2);
    }

    public bool IsInPreferredPosition(float xPosition)
    {
        if (min_XpreferredPosition > max_XpreferredPosition)
            return true;

        if (xPosition > max_XpreferredPosition)
            return false;

        if (xPosition < min_XpreferredPosition)
            return false;

        if (xPosition >= min_XpreferredPosition && xPosition <= max_XpreferredPosition)
            return true;

        return true;
    }

    public bool IsInPreferredMoveToPosition(float posiionX, out float preferredPositionX)
    {
        preferredPositionX = posiionX;

        if (min_XpreferredPosition > max_XpreferredPosition)
            return true;

        preferredPositionX = (min_XpreferredPosition + max_XpreferredPosition) / 2;

        if (Mathf.Abs(posiionX - preferredPositionX) > moveToSoftRange)
            return false;

        return true;
    }

    public bool IsInAttackRange(float xPosition)
    {
        if (currentTargetEnemy == null)
            return false;

        if (Mathf.Abs(currentTargetEnemy.point.x - xPosition) - self_HalfColliderSize <= weapon.Value.GetWeaponDetails().attackRange)
            return true;

        return false;
    }

    public Vector2 GetCurrentTargetEnemyPoint()
    {
        if (currentTargetEnemy != null)
            return currentTargetEnemy.point;

        else return Vector2.zero;
    }

    private void Debug_DrawMinMaxXPosition(float min_XPosition, float max_XPosition, float yOffset)
    {
        if (isDebug)
            Debug.DrawLine(new Vector2(min_XPosition, transform.position.y + yOffset), new Vector2(max_XPosition, transform.position.y + yOffset), Color.green, 0.5f);
    }
    private void Debug_DrawTargetXPosition(float xPoition)
    {
        if (isDebug)
            Debug.DrawLine(new Vector2(xPoition, transform.position.y), new Vector2(xPoition, transform.position.y + collisionCollider.size.y), Color.red, 0.5f);
    }
}
