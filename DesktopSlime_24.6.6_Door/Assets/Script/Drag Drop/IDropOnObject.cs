using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropOnObject 
{
    public bool DragOn(object dragObject);
    public bool DropOn(object dragObject);
}
