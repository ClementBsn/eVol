using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYFY;
using UnityEngine;
using UnityEngine.UI;

public class DisableMutationSystem : FSystem
{
    private Family _mutation_buttonsGO;

    private Family _mutation_buttons_selectedGO;

    private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
    private GameObject env;

    public DisableMutationSystem()
    {
        foreach (GameObject envi in _environment)
        {
            env = envi;
        }
    }

    protected override void onProcess(int familiesUpdateCount)
    {
        Level level = env.GetComponent<Level>();
        foreach (string s in level.components)
        {
            enableOrNot(s);
        }
    }

    void enableOrNot(string s)
    {
        _mutation_buttonsGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button"));
        _mutation_buttons_selectedGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button_selected"));

        switch (s)
        {
            case "Queue courte":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if(bt.GetComponent<BonusAndMalus>().name == "Queue longue")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Queue longue":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Queue courte")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Cou court":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Cou long")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Cou long":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Cou court")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Plumes longues":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Plumes courtes")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Plumes courtes":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Plumes longues")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Pas de sternum":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Sternum développé")
                    {
                        bt.interactable = false;
                    }
                }
                break;
            case "Sternum développé":
                foreach (GameObject go in _mutation_buttonsGO)
                {
                    Button bt = go.GetComponent<Button>();
                    if (bt.GetComponent<BonusAndMalus>().name == "Pas de sternum")
                    {
                        bt.interactable = false;
                    }
                }
                break;
        }
    }
}
