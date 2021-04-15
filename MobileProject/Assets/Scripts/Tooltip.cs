using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Tooltip exists!");
            Destroy(this);
        }
        instance = this;
        image = GetComponent<UnityEngine.UI.Image>();
    }

    new public TMPro.TextMeshProUGUI name;
    public TMPro.TextMeshProUGUI desc;
    UnityEngine.UI.Image image;
    float width, height;

    private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        width = rect.rect.width / 2.0f;
        height = rect.rect.height / 2.0f;
        SetEmpty();
    }

    public RectTransform container;

    // Add in types here
    public void SetText(TalentSO _talent)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, Input.mousePosition, Camera.main, out localPoint);
        transform.localPosition = new Vector3(localPoint.x + width, localPoint.y - height, 1);

        image.enabled = true;
        name.SetText(_talent.name);
        string description = "";

        if (_talent.addedDamageFlat != 0)
            description += "\nAdded Flat Damage: " + _talent.addedFlat;
        if (_talent.dmgMulti != 0)
            description += "\nDamage Multiplier: " + _talent.dmgMulti;
        if (_talent.addedHealth != 0)
            description += "\nAdded Flat Health: " + _talent.addedHealth;
        if (_talent.healthMultiplier != 0)
            description += "\nHealth Multiplier: " + _talent.healthMultiplier;


        if (_talent.level == _talent.maxLevel)
        {
            desc.SetText(description);
            return;
        }

        description += "\n\nNext Level: ";
        if (_talent.GetNextLevel().addedDamageFlat != 0)
            description += "\nAdded Flat Damage: " + _talent.GetNextLevel().addedFlat;
        if (_talent.GetNextLevel().dmgMulti != 0)
            description += "\nDamage Multiplier: " + _talent.GetNextLevel().dmgMulti;
        if (_talent.GetNextLevel().addedHealth != 0)
            description += "\nAdded Flat Health: " + _talent.GetNextLevel().addedHealth;
        if (_talent.GetNextLevel().healthMultiplier != 0)
            description += "\nHealth Multiplier: " + _talent.GetNextLevel().healthMultiplier;



        desc.SetText(description);
    }

    public void SetEmpty()
    {
        image.enabled = false;
        name.SetText("");
        desc.SetText("");
    }
}
