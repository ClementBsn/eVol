using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYFY;
using FYFY_plugins.PointerManager;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescribeReferencesSystem : FSystem
{
    private Family references = FamilyManager.getFamily(
        new AllOfComponents(typeof(ReferenceDinosaure)));

    private Family _level_buttons_pointedGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Button), typeof(PointerOver), typeof(Level)),
        new AnyOfTags("level_button"));

    private Family _descriptionText = FamilyManager.getFamily(new AllOfComponents(typeof(Text)), new AnyOfTags("describe_reference"));
    GameObject describeText;

    private Family _descriptionBoard = FamilyManager.getFamily(new AllOfComponents(typeof(VerticalLayoutGroup)), new AnyOfTags("describe_reference"));
    GameObject describeBoard;

    private Family _descriptionImage = FamilyManager.getFamily(new AllOfComponents(typeof(Image)), new AnyOfTags("describe_reference"),
        new NoneOfComponents(typeof(VerticalLayoutGroup)));
    GameObject describeImage;

    public DescribeReferencesSystem()
    {
        foreach (GameObject go in _descriptionText)
        {
            describeText = go;
        }

        foreach (GameObject go in _descriptionBoard)
        {
            describeBoard = go;
        }

        foreach (GameObject go in _descriptionImage)
        {
            describeImage = go;
        }

        _level_buttons_pointedGO.addEntryCallback(setDescription);
        _level_buttons_pointedGO.addExitCallback(resetDescription);
    }

    void setDescription(GameObject GO)
    {
        foreach(GameObject r in references)
        {
            if (isClosedRoRef(GO, r))
            {
                var c = describeBoard.GetComponent<Image>().color;
                c = new Color32(255, 255, 255, 100);
                describeBoard.GetComponent<Image>().color = c;

                describeText.GetComponent<Text>().text = getText(r);

                describeImage.GetComponent<Image>().sprite = getImage(r);
            }
        }
    }

    void resetDescription(int gameObjectInstanceId)
    {
        var c = describeBoard.GetComponent<Image>().color;
        c = new Color32(255, 255, 255, 0);
        describeBoard.GetComponent<Image>().color = c;

        describeText.GetComponent<Text>().text = "";
        describeImage.GetComponent<Image>().sprite = null;
    }

    bool isClosedRoRef(GameObject GO, GameObject r)
    {
        Level levelGO = GO.GetComponent<Level>();
        ReferenceDinosaure levelRef = r.GetComponent<ReferenceDinosaure>();

        foreach(string s in levelGO.components)
        {
            if (!levelRef.components.Contains(s))
            {
                return false;
            }
        }

        return true;
    }

    string getText(GameObject GO)
    {
        ReferenceDinosaure rd = GO.GetComponent<ReferenceDinosaure>();
        string s = "Cet oiseau ressemble à ce dinosaure:\n\nNom : "+rd.refDinoName+"\n\n";
        s += "De l'ère : " + rd.era + "\n";
        s += rd.description + "\n";
        s += rd.particularity + "\n";
        s += "Poids : " + rd.weight + "\n";
        s += "Taille : " + rd.height + "\n";
        return s;
    }

    Sprite getImage(GameObject GO)
    {
        Image i = GO.GetComponent<Image>();
        Debug.Log(i);
        return i.sprite;
    }
}