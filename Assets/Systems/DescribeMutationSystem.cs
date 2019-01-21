using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYFY;
using FYFY_plugins.PointerManager;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescribeMutationSystem : FSystem
{
    private Family _mutation_buttons_pointedGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Button), typeof(PointerOver)),
        new AnyOfTags("mutation_button", "mutation_button_selected"));

    private Family _mutation_buttons_notpointedGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Button)),
        new NoneOfComponents(typeof(PointerOver)),
        new AnyOfTags("mutation_button", "mutation_button_selected"));

    private Family _descriptionText = FamilyManager.getFamily(new AllOfComponents(typeof(Text)), new AnyOfTags("describe_mutation"));
    GameObject describeText;

    private Family _descriptionBoard = FamilyManager.getFamily(new AllOfComponents(typeof(VerticalLayoutGroup)), new AnyOfTags("describe_mutation"));
    GameObject describeBoard;

    public DescribeMutationSystem()
    {
        foreach (GameObject go in _descriptionText)
        {
            describeText = go;
        }

        foreach (GameObject go in _descriptionBoard)
        {
            describeBoard = go;
        }

        _mutation_buttons_pointedGO.addEntryCallback(setDescription);

        _mutation_buttons_notpointedGO.addEntryCallback(resetDescription);
    }

    void setDescription(GameObject GO)
    {
        var c = describeBoard.GetComponent<Image>().color;
        c = new Color32(255, 255, 255, 100);
        describeBoard.GetComponent<Image>().color = c;

        describeText.GetComponent<Text>().text = getText(GO);
    }

    void resetDescription(GameObject GO)
    {
        var c = describeBoard.GetComponent<Image>().color;
        c = new Color32(255, 255, 255, 0);
        describeBoard.GetComponent<Image>().color = c;

        describeText.GetComponent<Text>().text = "";
    }

    string getText(GameObject GO)
    {
        BonusAndMalus bm = GO.GetComponent<BonusAndMalus>();
        string s = "Nom : " + bm.name +"\n\n";
        s += bm.description;
        return s;
    }
}
