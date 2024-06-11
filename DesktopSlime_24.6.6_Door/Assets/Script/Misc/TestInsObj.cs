using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class TestInsObj : MonoBehaviour
{
    // test
    [SerializeField] private SerializableInterface<IEquipDetails> equipDetails;
    [SerializeField] private ChestDetailsSO chestDetails;
    [SerializeField] private HumanoidDetailsSO humanoidDetails;
    [SerializeField] private SlimeDetailsSO slimeDetails;
    private List<EquipDataSave> equipDataSaveList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipData equipData = new EquipData(equipDetails.Value);
            equipData.CreateInstantiatedObject(HelperUtils.GetMouseWorldPosition());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChestData chestData = new ChestData(chestDetails);
            chestData.CreateInstantiatedObject(HelperUtils.GetMouseWorldPosition());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DoorData doorData = new DoorData();
            doorData.CreateInstantiatedObject(HelperUtils.GetMouseWorldPosition());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HumanoidData humanoidData = new HumanoidData(humanoidDetails);
            humanoidData.CreateInstantiatedObject(HelperUtils.GetMouseWorldPosition());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EnemyData slimeData = new EnemyData(slimeDetails);
            slimeData.CreateInstantiatedObject(HelperUtils.GetMouseWorldPosition());
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SingletonReference.Instance.enemyManager.ClearEnemy();
        }
    }
}
