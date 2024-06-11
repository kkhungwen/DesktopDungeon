using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopUpText : MonoBehaviour
{
    [SerializeField] private TakeDamage takeDamage;
    [SerializeField] private TakeHeal takeHeal;
    [SerializeField] private GameObject popUpTextKey;

    [SerializeField] private Color directColor;
    [SerializeField] private Color poisonColor;
    [SerializeField] private Color healColor;
    [SerializeField] private float textSize;
    [SerializeField] private float duration;

    private void Awake()
    {
        takeDamage.OnTakeDamage += TakeDamage_OnTakeDamage;
        takeHeal.OnHeal += TakeHeal_OnHeal;
    }

    private void TakeHeal_OnHeal(float amount)
    {
        PopNumbers(amount, healColor);
    }

    private void TakeDamage_OnTakeDamage(TakedamageEventArgs eventArgs)
    {
        Color color;
        if (eventArgs.isDirect)
            color = directColor;
        else
            color = poisonColor;

        PopNumbers(eventArgs.damage, color);
    }

    private void PopNumbers(float amount, Color color)
    {
        int numbers = Mathf.RoundToInt(amount);

        if (numbers <= 0)
            return;

        Vector3 popPosition = transform.position + new Vector3(0, 0.5f);

        PoolObject_PopUpText popUpText = SingletonReference.Instance.objectPoolManager.GetComponentFromPool(popUpTextKey) as PoolObject_PopUpText;

        if (popUpText == null)
            Debug.Log("cant cast component to popUpText");

        popUpText.PopText(popPosition, duration, numbers.ToString(), textSize, color, popUpTextKey, SingletonReference.Instance.objectPoolManager);
    }
}
