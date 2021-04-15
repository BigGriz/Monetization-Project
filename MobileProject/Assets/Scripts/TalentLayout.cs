using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentLayout : MonoBehaviour
{
    List<ColumnUI> columns;

    private void Awake()
    {
        columns = new List<ColumnUI>();

        foreach(ColumnUI n in GetComponentsInChildren<ColumnUI>())
        {
            columns.Add(n);
            n.text.SetText("");
            n.image.enabled = false;
        }

        for (int i = 0; i < columns.Count; i++)
        {
            if ((i + 1) % 5 == 0)
            {
                columns[i].text.SetText((i + 1).ToString());
                columns[i].image.enabled = true;
            }
        }
    }
}
