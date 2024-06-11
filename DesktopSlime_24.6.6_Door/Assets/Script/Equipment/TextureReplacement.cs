using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

[DisallowMultipleComponent]
public class TextureReplacement : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IEquipments> equipments;
    [SerializeField] private Texture2D head_InputTexture;
    [SerializeField] private Texture2D body_InputTexture;


    private void Awake()
    {
        equipments.Value.OnEquip += Equipments_OnEquip;
        equipments.Value.OnUnEquip += Equipments_OnUnEquip;
    }

    private void Equipments_OnUnEquip(OnUnEquipEventArgs args)
    {

    }

    private void Equipments_OnEquip(OnEquipEventArgs args)
    {

    }

    private void ReplacePartTexture(EquipTypeSO equipType, Texture2D toReplaceTexture)
    {
        Debug.Log("replace");
        if (equipType == GameResources.Instance.head_Equip)
            ReplaceTexture(toReplaceTexture, head_InputTexture);

        if (equipType == GameResources.Instance.body_Equip)
            ReplaceTexture(toReplaceTexture, body_InputTexture);
    }

    private void ReplaceTexture(Texture2D toReplace, Texture2D beReplace)
    {
        // Debug
#if UNITY_EDITOR
        if (toReplace.width != beReplace.width || toReplace.height != beReplace.height)
            Debug.Log("ToReplace texture width and height should be the same with beReplace texture");
#endif 

        Color32[] fill = toReplace.GetPixels32();

        beReplace.SetPixels32(fill);

        beReplace.Apply();
    }
}
