using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TNRD;

public class Formula : MonoBehaviour
{
    public event Action<AttributeValueChangeEventArgs> OnAttributeValueChange;

    [SerializeField] bool isDebug = false;
    [SerializeField] private AttributeHolder attributeManager;
    [SerializeField] private AttributeModifyer attributeModifyer;

    private void Awake()
    {
        attributeModifyer.OnAttributeModify += AttributeModifyer_OnAttributeModify;
    }

    private void AttributeModifyer_OnAttributeModify(AttributeModifyEventArgs attributeModifyEventArgs)
    {
        float value = GetAttributeValueModified(attributeModifyEventArgs.attributeType);

        // Debug.Log(attributeType + " : " + GetAttributeValueModified(attributeType));
        OnAttributeValueChange?.Invoke(new AttributeValueChangeEventArgs(attributeModifyEventArgs.attributeType, value));
    }

    public float GetAttributeValueModified(AttributeTypeSO attributeType)
    {
        float value = 0;

        // Get attibute raw from attribute manager
        if (!attributeManager.GetAttributeValueRaw(attributeType, out value))
        {
            Debug.Log("Cannot get value raw from attribute manager");
            return value;
        }

        // Modify attribute with attribute modifier
        if (attributeModifyer.TryGetAttrubuteModifyValue(attributeType, out float addValue, out float multiplyValue))
        {
            value += addValue;
            value *= 1 + multiplyValue;
        }

        return value;
    }

    public float GetDamageOutput(DamageRatio damageRatio)
    {
        float attributeMultiplied = 0f;

        // Get attribute multiply
        foreach (AttributeRatio attributeRatio in damageRatio.attributeRatioArray)
        {
            float attributeValue = GetAttributeValueModified(attributeRatio.attributeType);

            attributeMultiplied += attributeValue * attributeRatio.ratio;
        }

        // Multiply attack by dammage output
        float damageOutput = GetAttributeValueModified(GameResources.Instance.damageOutput_Attribute);
        float damage = attributeMultiplied * damageOutput;

        // Debug
        if (isDebug)
            Debug.Log(transform.parent.name + " _ DamageOutput: " + damage + " =( Attibute multiplied " + attributeMultiplied + " )* DmgOutput: " + damageOutput);
        return damage;
    }


    public float GetDamageTaken(float damageInput)
    {
        // Get deffence attribute
        float defence = GetAttributeValueModified(GameResources.Instance.defence_Attribute);

        // Formula
        float damage = damageInput * (1 - defence / (Settings.absorbConstant + defence));

        // Multiply damage by damagetaken
        float damageTaken = GetAttributeValueModified(GameResources.Instance.damageTaken_Attribute);
        damage *= damageTaken;

        // Debug
        if (isDebug)
            Debug.Log(transform.parent.name + " _ DamageTaken: " + damage + " =( DmgIn: " + damageInput + " * DmgIn: " + damageInput + " /( DmgIn: " + damageInput + " + DF: " + defence + " ))* DmgTaken: " + damageTaken);

        return damage;
    }

    public float GetModifiedAttackTime(float defaultAttackTime)
    {
        float baseAS = 0.5f;
        float ATConstant = 10f;
        float modifiedAS = GetAttributeValueModified(GameResources.Instance.attackSpeed_Attribute);
        float modifiedAttckTime = defaultAttackTime * (baseAS + ATConstant) / (modifiedAS + ATConstant);

        return modifiedAttckTime;
    }

    public float GetPoisonDamage(int poisonStack)
    {
        float damage = poisonStack * Settings.poisonDamage * GetAttributeValueModified(GameResources.Instance.health_Attibute);

        return damage;
    }

    public float GetAttackCooldownTime(float attackSpeedRatio)
    {
        float attackSpeedAttribute = GetAttributeValueModified(GameResources.Instance.attackSpeed_Attribute);
        attackSpeedAttribute *= attackSpeedRatio;
        float attackCooldown = 1 / attackSpeedAttribute;

        return attackCooldown;
    }
}

public class AttributeValueChangeEventArgs
{
    public AttributeValueChangeEventArgs(AttributeTypeSO attributeType, float value)
    {
        this.attributeType = attributeType;
        this.value = value;
    }

    public AttributeTypeSO attributeType { get; private set; }
    public float value { get; private set; }
}
