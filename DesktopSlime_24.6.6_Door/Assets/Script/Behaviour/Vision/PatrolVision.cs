using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PatrolVision : MonoBehaviour
{
    public float patrolTargetX { get; private set; }

    // Debug
    [SerializeField] private bool isDebug;

    [Space(10f)]
    [Header("REQUIRED REFERENCE")]
    [SerializeField] private Vision vision;
    [SerializeField] private TagData tagData;
    [SerializeField] private BoxCollider2D collisionCollider;

    private float collisionSoftRange = 0.01f;
    private float moveToSoftRange = 0.1f;

    private float min_XpatrolRange = 0;
    private float max_XpatrolRange = 0;
    private float self_HalfColliderSize => collisionCollider.size.x / 2 + collisionSoftRange;

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
        UpdatePatrolDecisions(eventArgs.right_VisibleObjectList, eventArgs.left_VisibleObjectList, eventArgs.visionDistance);
    }

    private void UpdatePatrolDecisions(List<VisibleObject> right_VisibleObjectList, List<VisibleObject> left_VisibleObjectList, float visionDistance)
    {
        // Loop through right raycast hit
        for (int i = 0; i <= right_VisibleObjectList.Count; i++)
        {
            if (i == right_VisibleObjectList.Count)
            {
                max_XpatrolRange = transform.position.x + visionDistance - self_HalfColliderSize;
                break;
            }

            // If raycast hit enemy or obstacle, Set unpassable and enemy, break
            if (right_VisibleObjectList[i].tags.CompareTag(tagData.unpassableTagList))
            {
                max_XpatrolRange = right_VisibleObjectList[i].point.x - self_HalfColliderSize;

                break;
            }
        }

        // Loop through left raycast hit
        for (int i = 0; i <= left_VisibleObjectList.Count; i++)
        {
            if (i == left_VisibleObjectList.Count)
            {
                min_XpatrolRange = transform.position.x - visionDistance + self_HalfColliderSize;
                break;
            }

            // If raycast hit enemy or obstacle, Set unpassable and enemy, break
            if (left_VisibleObjectList[i].tags.CompareTag(tagData.unpassableTagList))
            {
                min_XpatrolRange = left_VisibleObjectList[i].point.x + self_HalfColliderSize;

                break;
            }
        }

        if (min_XpatrolRange > max_XpatrolRange)
        {
            min_XpatrolRange = transform.position.x;
            max_XpatrolRange = transform.position.x;
        }

        if (min_XpatrolRange > transform.position.x)
            min_XpatrolRange = transform.position.x;

        if (max_XpatrolRange < transform.position.x)
            max_XpatrolRange = transform.position.x;

        // Reset patrol target if current patrol target is not in range
        if (patrolTargetX < min_XpatrolRange || patrolTargetX > max_XpatrolRange)
            SetRandomPatrolTargetX();

        Debug_DrawMinMaxXPosition(min_XpatrolRange, max_XpatrolRange);
        Debug_DrawTargetXPosition(patrolTargetX);
    }

    public void SetRandomPatrolTargetX()
    {
        patrolTargetX = Random.Range(min_XpatrolRange, max_XpatrolRange);
    }

    public bool IsArrivePatrolTarget(float xPosition)
    {
        if (Mathf.Abs(xPosition - patrolTargetX) < moveToSoftRange)
            return true;

        return false;
    }

    private void Debug_DrawMinMaxXPosition(float min_XPosition, float max_XPosition)
    {
        if (isDebug)
            Debug.DrawLine(new Vector2(min_XPosition, transform.position.y + collisionCollider.size.y / 2), new Vector2(max_XPosition, transform.position.y + collisionCollider.size.y / 2), Color.green, 0.5f);
    }
    private void Debug_DrawTargetXPosition(float xPoition)
    {
        if (isDebug)
            Debug.DrawLine(new Vector2(xPoition, transform.position.y), new Vector2(xPoition, transform.position.y + collisionCollider.size.y), Color.red, 0.5f);
    }
}
