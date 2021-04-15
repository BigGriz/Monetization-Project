using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("Setup Fields")]
    public List<Gear> gear;
    public GameObject shopSlotPrefab;
    public GameObject layoutGroup;

    // Local Variables
    Animator anim;
    bool show;

    #region Setup
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        foreach (Gear n in gear)
        {
            ShopSlot temp = Instantiate(shopSlotPrefab, layoutGroup.transform).GetComponent<ShopSlot>();
            temp.EquipItem(n);
        }
    }
    #endregion Setup

    private void Update()
    {
        // Need some sort of menu manager
        if (Input.GetKeyDown(KeyCode.B))
        {
            show = !show;
            anim.SetBool("Show", show);
        }
    }
}
