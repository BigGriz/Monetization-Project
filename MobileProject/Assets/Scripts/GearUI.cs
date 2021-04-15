using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUI : MonoBehaviour
{
    #region Setup
    List<GearSlot> gearSlots;
    private void Awake()
    {
        gearSlots = new List<GearSlot>();
        foreach(GearSlot n in GetComponentsInChildren<GearSlot>())
        {
            gearSlots.Add(n);
        }
    }
    #endregion Setup
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
        for (int i = 0; i < gearSlots.Count; i++)
        {
            if (!gearSlots[i].HasGear())
            {
                gearSlots[i].EquipItem(_gear);
                return;
            }
        }
    }
}
