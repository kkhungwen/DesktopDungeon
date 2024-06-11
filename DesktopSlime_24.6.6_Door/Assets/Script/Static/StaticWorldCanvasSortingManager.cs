using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticWorldCanvasSortingManager
{
    public static event Action OnResetSortingOrder;

    private static int topSortinOrder = 0;

    public static int GetTopSortingOrder()
    {
        if (topSortinOrder >= 30000)
        {
            OnResetSortingOrder?.Invoke();
            topSortinOrder = 0;
        }

        topSortinOrder++;
        return topSortinOrder;
    }
}
