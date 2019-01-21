using UnityEngine;
using UnityEngine.UI;
using FYFY;
using System;

/**Class MutationMenuSystem
 * Allows the player to select the mutations to add to its Dinosaure
**/
public class MutationMenuSystem : FSystem
{
    private Family _mutation_buttonsGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Button)),
        new AnyOfTags("mutation_button"));

    private Family _mutation_buttons_selectedGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Button)),
        new AnyOfTags("mutation_button_selected"));

    private Family _inputnameGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(InputField)),
        new AnyOfTags("mutation_input_name"));
    private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
    private GameObject env;
    
   // private Family _dinoGO = FamilyManager.getFamily(new AllOfComponents(typeof(Dinosaure)));


    public MutationMenuSystem()
    {
        foreach (GameObject go in _inputnameGO)
        {
            InputField field = go.GetComponent<InputField>();
            field.onEndEdit.AddListener(delegate {ChangeLevelName(field); });
        }

        foreach (GameObject go in _mutation_buttons_selectedGO)
        {
            Button bt = go.GetComponent<Button>();
            bt.onClick.AddListener(delegate { SubFunction(go, bt); });
        }

        foreach (GameObject go in _mutation_buttonsGO)
        {
            Button bt = go.GetComponent<Button>();
            bt.onClick.AddListener(delegate { AddFunction(go, bt); });
        }

        _mutation_buttonsGO.addEntryCallback(onNewMutationButton);
        _mutation_buttons_selectedGO.addEntryCallback(onNewMutationButtonSelected);
        foreach (GameObject envi in _environment) {
            env=envi;
        }

    }

    void ChangeLevelName(InputField field){
        env.GetComponent<Level>().name=field.text;
    }

    void onNewMutationButton(GameObject go)
    {
        Button bt = go.GetComponent<Button>();
        bt.onClick.RemoveAllListeners();
        bt.onClick.AddListener(delegate { AddFunction(go, bt); });
    }

    void onNewMutationButtonSelected(GameObject go)
    {
        Button bt = go.GetComponent<Button>();
        bt.onClick.RemoveAllListeners();
        bt.onClick.AddListener(delegate { SubFunction(go, bt); });
    }

    void AddFunction(GameObject GO, Button bt)
    {
        Debug.Log("Switch ON : " + GO.name);

        addBonusAndMalus(GO);

        switchMutation(GO.name);
        //enableButtons();
    }

    void SubFunction(GameObject GO, Button bt)
    {
        if (canBeUnselected(GO.name))
        {
            Debug.Log("Click OFF : " + GO.name);

            removeBonusAndMalus(GO);

            //enableButtons();
        }
    }

    void enableButtons()
    {
        foreach(GameObject go in _mutation_buttonsGO)
        {
            Button bt = go.GetComponent<Button>();
            bt.interactable = false;
        }
        foreach (GameObject go in _mutation_buttons_selectedGO)
        {
            Button bt = go.GetComponent<Button>();
            bt.interactable = false;
        }
    }


    void addBonusAndMalus(GameObject GO)
    {
        //GameObjectManager.setGameObjectTag(GO, "mutation_button_selected");
        BonusAndMalus bm = GO.GetComponent<BonusAndMalus>();
        env.GetComponent<Level>().massBonus+=bm.mass;
        env.GetComponent<Level>().dragBonus+=bm.drag;
        env.GetComponent<Level>().energyBonus += bm.energy;
        env.GetComponent<Level>().maxVelocityBonus += bm.maxVelocityFly;
        env.GetComponent<Level>().agilityBonus += bm.agility;
        env.GetComponent<Level>().powerFlyBonus += bm.powerFly;
        env.GetComponent<Level>().components.Add(bm.name);
        /*        foreach (GameObject dino in _dinoGO)
                {


                    Rigidbody2D rb = dino.GetComponent<Rigidbody2D>();
                    rb.mass += bm.mass;
                    rb.drag += bm.drag;
                }*/
    }

    void removeBonusAndMalus(GameObject GO)
    {
        //GameObjectManager.setGameObjectTag(GO, "mutation_button");
        BonusAndMalus bm = GO.GetComponent<BonusAndMalus>();
        env.GetComponent<Level>().massBonus-=bm.mass;
        env.GetComponent<Level>().dragBonus-=bm.drag;
        env.GetComponent<Level>().energyBonus -= bm.energy;
        env.GetComponent<Level>().maxVelocityBonus -= bm.maxVelocityFly;
        env.GetComponent<Level>().agilityBonus -= bm.agility;
        env.GetComponent<Level>().powerFlyBonus -= bm.powerFly;
        env.GetComponent<Level>().components.Remove(bm.name);

        /*foreach (GameObject dino in _dinoGO)
        {
            
           
            /*Rigidbody2D rb = dino.GetComponent<Rigidbody2D>();
            rb.mass -= bm.mass;
            rb.drag -= bm.drag;

            
        }*/
    }

    bool canBeUnselected(string name)
    {
        if(name == "GriffesSurAiles" || name == "OsCreux" || name == "PlumesAsym" ||
            name == "Molaires" || name == "Criniere" ||
            name == "PointedTeeth" || name == "CouNu" ||
            name == "Crete")
        {
            return true;
        }
        return false;
    }

    void switchMutation(string name)
    {
        if(name == "ShortTail" || name == "MiddleTail" || name == "LongTail")
        {
            foreach(GameObject GO in _mutation_buttons_selectedGO)
            {
                if((GO.name == "ShortTail" || GO.name == "MiddleTail" || GO.name == "LongTail") && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "ShortFeather" || name == "MiddleFeather" || name == "LongFeather")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if ((GO.name == "ShortFeather" || GO.name == "MiddleFeather" || GO.name == "LongFeather") && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "ShortNeck" || name == "MiddleNeck" || name == "LongNeck")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if ((GO.name == "ShortNeck" || GO.name == "MiddleNeck" || GO.name == "LongNeck") && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "NoSternum" || name == "FewDevSternum" || name == "DevSternum")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if ((GO.name == "NoSternum" || GO.name == "FewDevSternum" || GO.name == "DevSternum") && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "NoTeeth")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if (GO.name == "Molaires" || GO.name == "PointedTeeth")
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "Molaires" || name == "PointedTeeth")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if (GO.name == "NoTeeth")
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "Criniere" || name == "CouNu")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if ((GO.name == "Criniere" || GO.name == "CouNu") && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "GrandAllong" || name == "AvecInterstices" ||
            name == "Elliptique" || name == "GrandeVitesse")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if ((GO.name == "GrandAllong" || GO.name == "AvecInterstices" ||
                    GO.name == "Elliptique" || GO.name == "GrandeVitesse") 
                    && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }

        if (name == "Museau" || name == "BecCrochu" ||
            name == "BecPlat" || name == "BecNormal" ||
            name == "BecPointu")
        {
            foreach (GameObject GO in _mutation_buttons_selectedGO)
            {
                if ((GO.name == "Museau" || GO.name == "BecCrochu" ||
                    GO.name == "BecPlat" || GO.name == "BecNormal" ||
                    GO.name == "BecPointu")
                    && (GO.name != name))
                {
                    Button bt = GO.GetComponent<Button>();
                    Debug.Log("SWITCH OFF : " + GO.name);

                    removeBonusAndMalus(GO);
                }
            }
        }
    }

}