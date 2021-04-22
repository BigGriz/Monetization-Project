using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUI : MonoBehaviour
{
    public GearSlot gearSlot;
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.addGear += AddGear;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.addGear -= AddGear;
    }
    #endregion Callbacks

    public void AddGear(Gear _gear)
    {
        gearSlot.EquipItem(_gear);
    }
}
