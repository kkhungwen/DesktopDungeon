using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshPro))]
[DisallowMultipleComponent]
public class PopUpText : MonoBehaviour
{
    public event Action OnFinishPop;

    private TextMeshPro textMeshPro;
    private float textSize = 0;
    private float randomLifeTime = 0;
    private float timeCount = 0;
    bool isPop = false;
    private Vector3 startPosition;
    private Color color;
    private float randomMoveUpDistance;

    [Header("CONDIGURATIONS")]
    [SerializeField] private float spreadRadious;
    [SerializeField] private float lifeTimeRandomRange;

    [Space(10f)]
    [Header("TEXT SIZE CURVE")]
    [SerializeField] private AnimationCurve textSizeCurve;
    [SerializeField] private float sizeRandomRange;

    [Space(10f)]
    [Header("MOVE UP EFFECT")]
    [SerializeField] private AnimationCurve moveUpCurve;
    [SerializeField] private float moveUpDistance;
    [SerializeField] private float moveUpRandomRange;

    [Space(10f)]
    [Header("FADE EFFECT")]
    [SerializeField] private AnimationCurve fadeCurve;



    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();

        textMeshPro.text = "";
    }

    private void Update()
    {
        if (!isPop)
            return;

        timeCount += Time.deltaTime;

        ChangeTextSize();

        MoveUp();

        Fade();

        if (timeCount >= randomLifeTime)
            FinishPop();
    }

    public void PopText(Vector3 position, float lifeTime, string text, float textSize, Color color)
    {
        Vector3 randomPosition = position + (Vector3)UnityEngine.Random.insideUnitCircle * spreadRadious;

        transform.position = randomPosition;

        startPosition = randomPosition;

        randomLifeTime = lifeTime + UnityEngine.Random.Range(-lifeTimeRandomRange, lifeTimeRandomRange);

        textMeshPro.text = text;

        this.textSize = textSize + UnityEngine.Random.Range(-sizeRandomRange, sizeRandomRange);

        textMeshPro.color = color;

        this.color = color;

        randomMoveUpDistance = moveUpDistance + UnityEngine.Random.Range(-moveUpRandomRange, moveUpRandomRange);

        timeCount = 0;

        ChangeTextSize();

        isPop = true;
    }

    private void ChangeTextSize()
    {
        textMeshPro.fontSize = textSize * textSizeCurve.Evaluate(timeCount / randomLifeTime);
    }

    private void MoveUp()
    {
        transform.position = startPosition + new Vector3(0, randomMoveUpDistance * moveUpCurve.Evaluate(timeCount / randomLifeTime));
    }

    private void Fade()
    {
        textMeshPro.color = new Color(color.r, color.g, color.b, fadeCurve.Evaluate(timeCount / randomLifeTime));
    }

    private void FinishPop()
    {
        textMeshPro.text = "";
        timeCount = 0;
        isPop = false;

        OnFinishPop?.Invoke();
    }
}
