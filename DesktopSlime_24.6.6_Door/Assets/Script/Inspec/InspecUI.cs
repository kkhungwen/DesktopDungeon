using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspecUI : MonoBehaviour
{
    [field:SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        StaticWorldCanvasSortingManager.OnResetSortingOrder += StaticWorldCanvasSortingManager_OnResetSortingOrder;
    }

    private void OnDisable()
    {
        StaticWorldCanvasSortingManager.OnResetSortingOrder -= StaticWorldCanvasSortingManager_OnResetSortingOrder;
    }

    private void StaticWorldCanvasSortingManager_OnResetSortingOrder()
    {
        canvas.sortingOrder = 0;
    }

    public void SetSortingOrderToTop()
    {
        int sortingOrder = StaticWorldCanvasSortingManager.GetTopSortingOrder();
        canvas.overrideSorting = true;
        canvas.sortingOrder = sortingOrder;
    }
}
