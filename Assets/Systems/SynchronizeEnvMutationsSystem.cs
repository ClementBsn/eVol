using UnityEngine;
using UnityEngine.UI;
using FYFY;
using System;

public class SynchronizeEnvMutationsSystem : FSystem
{
    private Family _mutation_buttonsGO;

    private Family _mutation_buttons_selectedGO;

    private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
    private GameObject env;

    public SynchronizeEnvMutationsSystem()
    {
        foreach (GameObject envi in _environment)
        {
            env = envi;
        }
    }

    protected override void onProcess(int familiesUpdateCount)
    {
        _mutation_buttons_selectedGO  = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button_selected"));
        _mutation_buttonsGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button"));
        Level l = env.GetComponent<Level>();

        
        foreach (GameObject go in _mutation_buttonsGO)
        {
            foreach (String s in l.components)
            {
                BonusAndMalus bn = go.GetComponent<BonusAndMalus>();
                if (bn.name == s)
                {
                    GameObjectManager.setGameObjectTag(go, "mutation_button_selected");
                    Button bt = go.GetComponent<Button>();
                    switchColorButton(bt);
                }
            }
        }
        
        foreach (GameObject go in _mutation_buttons_selectedGO)
        {
            bool selected = false;
            foreach(String s in l.components)
            {
                BonusAndMalus bn = go.GetComponent<BonusAndMalus>();
                if (bn.name == s)
                {
                    selected = true;
                    break;
                }
            }
            if (!selected)
            {
                GameObjectManager.setGameObjectTag(go, "mutation_button");
                Button bt = go.GetComponent<Button>();
                switchColorButton(bt);
            }
        }
    }

    void switchColorButton(Button bt)
    {
        var colors = bt.colors;
        Color c = colors.pressedColor;
        colors.pressedColor = colors.normalColor;
        colors.normalColor = c;
        bt.colors = colors;
    }
}
