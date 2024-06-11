using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInspecable
{
    public bool CreatMouseOverInspec(Transform transform, Vector2 localPosition, out IInspec inspec);

    public bool CreateClickInspec(Transform transform, Vector2 localPosition, out IInspec inspec);
}
