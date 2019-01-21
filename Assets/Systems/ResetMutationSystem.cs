using UnityEngine;
using UnityEngine.UI;
using FYFY;
using System;
using System.Collections.Generic;

public class ResetMutationSystem : FSystem
{
    private Family _resetGO = FamilyManager.getFamily(
        new AllOfComponents(typeof(Button)),
        new AnyOfTags("reset_button"));

    private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));

    private Family _levels = FamilyManager.getFamily(new AllOfComponents(typeof(Level)), new NoneOfComponents(typeof(ScrollingBG)));

    private Family _mutation_buttonsGO;

    private Family _create_dino_buttonGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("create_dino_button"));

    private GameObject _create_dino;

    private GameObject env;

    public ResetMutationSystem()
    {
        foreach (GameObject go in _resetGO)
        {
            Button bt = go.GetComponent<Button>();
            bt.onClick.AddListener(delegate { reset(go, bt); });
        }

        foreach (GameObject envi in _environment)
        {
            env = envi;
        }

        foreach (GameObject cdb in _create_dino_buttonGO)
        {
            _create_dino = cdb;

            Button bt = _create_dino.GetComponent<Button>();
            bt.onClick.AddListener(delegate { activateButtons(); });
        }


    }

    void reset(GameObject GO, Button bt)
    {
        //Debug.Log("Reset");
        foreach (GameObject go in _levels)
        {
            Level level = go.GetComponent<Level>();
            if (env.GetComponent<Level>().number == level.number)
            {
                env.GetComponent<Level>().name = level.name;
                env.GetComponent<Level>().number = level.number;
                env.GetComponent<Level>().children = level.children;
                env.GetComponent<Level>().components = new List<string>();
                foreach (string s in level.components)
                {
                    env.GetComponent<Level>().components.Add(s);
                }
                //env.GetComponent<Level>().components = level.components;
                env.GetComponent<Level>().bestScore = level.bestScore;
                env.GetComponent<Level>().obstaclesNb = level.obstaclesNb;
                env.GetComponent<Level>().coinsNb = level.coinsNb;
                env.GetComponent<Level>().length = level.length;

                env.GetComponent<Level>().massBonus = level.massBonus;
                env.GetComponent<Level>().dragBonus = level.dragBonus;
                env.GetComponent<Level>().energyBonus = level.energyBonus;
                env.GetComponent<Level>().powerFlyBonus = level.powerFlyBonus;
                env.GetComponent<Level>().maxVelocityBonus = level.maxVelocityBonus;
                env.GetComponent<Level>().agilityBonus = level.agilityBonus;
                env.GetComponent<Level>().initialized = level.initialized;

                break;
            }
        }

        activateButtons();
    }

    void activateButtons()
    {
        _mutation_buttonsGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button"));

        foreach (GameObject mutGO in _mutation_buttonsGO)
        {
            Button mutBT = mutGO.GetComponent<Button>();
            mutBT.interactable = true;
        }

        _mutation_buttonsGO = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), new AnyOfTags("mutation_button_selected"));

        foreach (GameObject mutGO in _mutation_buttonsGO)
        {
            Button mutBT = mutGO.GetComponent<Button>();
            mutBT.interactable = true;
        }
    }
}
