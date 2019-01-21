using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYFY;
using UnityEngine;
using UnityEngine.UI;

public class ColorMutButtonSystem : FSystem
{
    private Family _mutation_buttonsGO;

    private Family _mutation_buttons_selectedGO;

    private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
    private GameObject env;

    public ColorMutButtonSystem()
    {
        foreach (GameObject envi in _environment)
        {
            env = envi;
        }
    }

    protected override void onProcess(int familiesUpdateCount)
    {
        _mutation_buttonsGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button"));
        _mutation_buttons_selectedGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button_selected"));

        foreach (GameObject mutGO in _mutation_buttonsGO)
        {
            Button bt = mutGO.GetComponent<Button>();
            if(bt.interactable)
            {
                var colors = bt.colors;
                colors.normalColor = new Color32(24,82,99,255);
                bt.colors = colors;
            }
            else
            {
                var colors = bt.colors;
                colors.disabledColor = new Color32(24, 82, 99, 140);
                bt.colors = colors;
            }
        }

        foreach (GameObject mutGO in _mutation_buttons_selectedGO)
        {
            Button bt = mutGO.GetComponent<Button>();
            if (bt.interactable)
            {
                var colors = bt.colors;
                colors.normalColor = new Color32(102, 233, 216, 255);
                bt.colors = colors;
            }
            else
            {
                var colors = bt.colors;
                colors.disabledColor = new Color32(102, 233, 216, 140);
                bt.colors = colors;
            }
        }
    }
}
