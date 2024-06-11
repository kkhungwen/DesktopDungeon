using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    public bool isRight { get; private set; }

    [SerializeField] private Transform[] transformRotateYArray;

    public void ChangeFaceDirection(bool isRight)
    {
        this.isRight = isRight;

        if (isRight)
            foreach (Transform transform in transformRotateYArray)
                //transform.localScale = new Vector3(1, 1, 1);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);


        else
            foreach (Transform transform in transformRotateYArray)
                //transform.localScale = new Vector3(-1, 1, 1);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
    }
}
