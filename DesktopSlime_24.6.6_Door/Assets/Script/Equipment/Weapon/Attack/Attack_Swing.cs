using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Attack_Swing : MonoBehaviour, IAttack
{
    [Space(10f)]
    [Header("REQUIRED REFERENCE")]
    [SerializeField] private GameObject animated_Hand;
    [SerializeField] private Transform armAnchorPoint;
    [SerializeField] private Transform swing_Sprite_Hand;
    [SerializeField] private SpriteRenderer swing_Sprite_Weapon;
    [SerializeField] private BoxCollider2D hitBoxCollider;
    [SerializeField] private Hitbox_OnTriggerEnterOnce hitbox;

    private IWeapon weapon;
    private Animator anima;

    [Space(10f)]
    [Header("CONFIGURABLES")]
    [SerializeField] private float defaultAttackTime;
    [SerializeField] private Vector2 armAnchorPointPosition;
    [SerializeField] private Vector2 handleSwingPointOffset;
    [SerializeField] private float from_SwingAngle;
    [SerializeField] private float to_SwingAngle;

    private float modifiedAttackTime;
    private float attackTimeCount = 0;

    private void Awake()
    {
        anima = GetComponentInParent<Animator>();
        weapon = GetComponent<IWeapon>();
    }

    public void WeaponUpdate()
    {
        attackTimeCount += Time.deltaTime;

        float swingAngle = Mathf.Lerp(from_SwingAngle, to_SwingAngle, attackTimeCount / modifiedAttackTime);

        armAnchorPoint.localEulerAngles = new Vector3(0, 0, swingAngle);

        if (attackTimeCount > modifiedAttackTime)
            EndAttack();
    }

    public void SetUpAttack(WeaponDetailsSO weaponDetails)
    {
        // Activeate used object
        armAnchorPoint.gameObject.SetActive(false);

        // Set position
        armAnchorPoint.localPosition = armAnchorPointPosition;
        swing_Sprite_Weapon.transform.localPosition = handleSwingPointOffset;
        swing_Sprite_Hand.localPosition = handleSwingPointOffset;

        // Set collider 
        hitBoxCollider.size = new Vector2(weaponDetails.width_HitBox, weaponDetails.height_HitBox);
        hitBoxCollider.offset = handleSwingPointOffset + new Vector2(0, weaponDetails.height_HitBox / 2);
        hitBoxCollider.enabled = false;

        // Set sprite
        swing_Sprite_Weapon.sprite = weaponDetails.activeSprite;

        EndAttack();
    }

    public void SetDownAttack()
    {
        EndAttack();

        // Activeate used object
        armAnchorPoint.gameObject.SetActive(false);

        // Set position
        armAnchorPoint.localPosition = Vector2.zero;
        swing_Sprite_Weapon.transform.localPosition = Vector2.zero;
        swing_Sprite_Hand.localPosition = Vector2.zero;

        // Set collider 
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

        // Reset parameters
        modifiedAttackTime = weapon.GetModifiedAttackTime(defaultAttackTime);
        attackTimeCount = 0;
        armAnchorPoint.localEulerAngles = new Vector3(0, 0, from_SwingAngle);

        // Set animation
        anima.Play("Attack_Swing");
    }

    public void EndAttack()
    {
        // Activeate used object
        animated_Hand.SetActive(true);
        armAnchorPoint.gameObject.SetActive(false);
        hitbox.SetDownHitBox();
        hitBoxCollider.enabled = false;

        // Reset angle
        armAnchorPoint.localEulerAngles = Vector3.zero;

        // Notify weapon to end attack
        weapon.AttackEnd();
    }
}
