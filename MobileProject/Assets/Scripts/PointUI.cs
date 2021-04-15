using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        CallbackHandler.instance.updateTalentPoints += UpdateTalentPoints;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.updateTalentPoints -= UpdateTalentPoints;
    }

    public void UpdateTalentPoints(int _points)
    {
        text.SetText("POINTS: " + _points.ToString());
    }
}
