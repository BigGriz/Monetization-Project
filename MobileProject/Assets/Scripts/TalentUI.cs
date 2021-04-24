using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentUI : MonoBehaviour
{
    [Header("Setup Fields")]
    public GameObject talentPrefab;
    public GameObject talentContainer;
    List<TalentSO> talents;
    [Header("Talent Totals")]
    public TalentSO talentModifiers;
    [Header("Talent Options")]
    public List<TalentSO> talentOptions;

    #region Setup
    // Local Variables
    Animator anim;
    MENUOPTION option = MENUOPTION.TALENT;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        talents = new List<TalentSO>();
        // Seed RNG
        Random.InitState(Mathf.RoundToInt(Time.time));
    }

    private void Start()
    {
        SetupTalents();
        GetTotals();

        CallbackHandler.instance.updateTalents += GetTotals;
        CallbackHandler.instance.changeMenu += ChangeMenu; 
        CallbackHandler.instance.addTalent += AddTalent;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.updateTalents -= GetTotals;
        CallbackHandler.instance.changeMenu -= ChangeMenu;
        CallbackHandler.instance.addTalent -= AddTalent;
    }
    #endregion Setup

    void SetupTalents()
    {
        for (int i = 0; i < talentOptions.Count; i++)
        {
            talentOptions[i] = Instantiate(talentOptions[i]);
        }
    }

    /// <summary>
    /// Adds talent to the total.
    /// </summary>
    /// <param name="_talent"></param>
    public void AddTalent(TalentSO _talent)
    {
        if (!talents.Contains(_talent))
            talents.Add(_talent);

        GetTotals();
    }

    public List<GameObject> talentObjects;
    public void CleanUp()
    {
        foreach(GameObject n in talentObjects)
        {
            Destroy(n);
        }
        talentObjects.Clear();
    }


    /// <summary>
    /// Gets 3 Random Talents from list of options
    /// </summary>
    public void GetTalents()
    {
        CleanUp();

        List<int> temp = new List<int>();
        for (int i = 0; i < talentOptions.Count; i++)
        {
            int rand = Random.Range(0, talentOptions.Count);

            while (temp.Contains(rand))
            {
                rand = Random.Range(0, talentOptions.Count);
            }

            temp.Add(rand);
        }

        foreach(int n in temp)
        {
            GameObject talent = Instantiate(talentPrefab, talentContainer.transform);
            talent.GetComponent<Talent>().SetupTalent(talentOptions[n]);
            talentObjects.Add(talent);
        }
    }

    /// <summary>
    ///  Updates Talent Total Modifiers
    /// </summary>
    public void GetTotals()
    {
        talentModifiers.ResetTotals();

        foreach(TalentSO n in talents)
        {
            talentModifiers.AddTalent(n);
        }
    }

    #region Utility
    [HideInInspector] public bool clickable = false;
    public void SetClickable()
    {
        clickable = true;
    }
    public void SetUnClickable()
    {
        clickable = false;
    }
    public void ChangeMenu(MENUOPTION _option)
    {
        anim.SetBool("Show", option == _option ? !anim.GetBool("Show") : false);

        if (option == _option)
            GetTalents();
    }
    #endregion Utility
}
