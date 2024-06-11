using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Attack_Stab : MonoBehaviour, IAttack
{
    private Animator anima;
    private IWeapon weapon;

    [Space(10f)]
    [Header("REQUIRED REFERENCE")]
    [SerializeField] private GameObject animated_Hand;
    [SerializeField] private Transform armAnchorPoint;
    [SerializeField] private SpriteRenderer swing_Sprite_Weapon;
    [SerializeField] private BoxCollider2D hitBoxCollider;
    [SerializeField] private Hitbox_OnTriggerEnterOnce hitbox;

    [Space(10f)]
    [Header("CONFIGURABLES")]
    [SerializeField] private float defaultAttackTime;
    [SerializeField] private Vector2 armAnchorPointPosition;
    [SerializeField] float startPosition_X;
    [SerializeField] float endPosition_X;

    private float modifiedAttckTime;
    private float attackTimeCount = 0;

    private void Awake()
    {
        anima = GetComponentInParent<Animator>();
        weapon = GetComponent<IWeapon>();
    }

    public void WeaponUpdate()
    {
        attackTimeCount += Time.deltaTime;

        float stabPositionX = Mathf.Lerp(startPosition_X, endPosition_X, attackTimeCount / modifiedAttckTime);

        armAnchorPoint.localPosition = new Vector2(armAnchorPointPosition.x + stabPositionX, armAnchorPointPosition.y);

        if (attackTimeCount > modifiedAttckTime)
            EndAttack();
    }

    public void SetUpAttack(WeaponDetailsSO weaponDetails)
    {
        // Activeate used object
        armAnchorPoint.gameObject.SetActive(false);

        // Set position
        armAnchorPoint.localPosition = armAnchorPointPosition;

        // Set collider size
        hitBoxCollider.size = new Vector2(weaponDetails.width_HitBox, weaponDetails.height_HitBox);
        hitBoxCollider.offset = new Vector2(weaponDetails.width_HitBox / 2, 0);
        hitBoxCollider.enabled = false;

        // Set sprite
        swing_Sprite_Weapon.sprite = weaponDetails.attackSprite;

        EndAttack();
    }

    public void SetDownAttack()
    {
        EndAttack();

        // Activeate used object
        armAnchorPoint.gameObject.SetActive(false);

        // Set position
        armAnchorPoint.localPosition = Vector2.zero;

        // Set collider size
        hitBoxCollider.size = Vector2.zero;
        hitBoxCollider.offset = Vector2.zero;
        hitBoxCollider.enabled = false;

        // Set sprite
        swing_Sprite_Weapon.sprite = null;
    }

    public void Attack()
    {
        // Activeate used object
        animated_Hand.SetActive(false);
        armAnchorPoint.gameObject.SetActive(true);
        hitbox.SetUpHitBox(weapon.GetDamageData(), weapon.GetEnemyTagList());
        hitBoxCollider.enabled = true;

        // Set position
        armAnchorPoint.localPosition = armAnchorPointPosition;

        // Reset parameters
        modifiedAttckTime = weapon.GetModifiedAttackTime(defaultAttackTime);
        attackTimeCount = 0;

        // Set animation
        anima.Play("Attack_Stab");
    }

    public void EndAttack()
    {
        // Activeate used object
        animated_Hand.SetActive(true);
        armAnchorPoint.gameObject.SetActive(false);
        hitbox.SetDownHitBox();
        hitBoxCollider.enabled = false;

        // Set position
        armAnchorPoint.localPosition = Vector2.zero;

        weapon.AttackEnd();
    }
}
