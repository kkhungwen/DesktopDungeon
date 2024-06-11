using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInspec_Hover_UI : InspecUI
{
    [SerializeField] private RectTransform prograssTransform;
    [SerializeField] private TextMeshProUGUI progressText;

    private float prograssOriginalWidth;
    private float prograssOriginalHeight;


    private void Awake()
    {
        prograssOriginalWidth = prograssTransform.sizeDelta.x;
        prograssOriginalHeight = prograssTransform.sizeDelta.y;
    }

    public void UpdateDoorUI(int requiredAmount, int currentAmount)
    {
        prograssTransform.sizeDelta = new Vector2(prograssOriginalWidth * currentAmount / requiredAmount, prograssOriginalHeight);
        progressText.text = currentAmount + " / " + requiredAmount;
    }
}
