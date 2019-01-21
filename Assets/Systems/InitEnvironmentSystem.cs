using UnityEngine;
using UnityEngine.UI;
using FYFY;
using System;
using System.Collections.Generic;

public class InitEnvironmentSystem : FSystem
{
    private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));

    private Family _levels = FamilyManager.getFamily(new AllOfComponents(typeof(Level)), new NoneOfComponents(typeof(ScrollingBG)));

    private GameObject env;

    private Level level;


    public InitEnvironmentSystem()
    {
        foreach (GameObject envi in _environment)
        {
            env = envi;
        }

        foreach (GameObject lev in _levels)
        {
            level = lev.GetComponent<Level>();
        }

        env.GetComponent<Level>().name = level.name;
        env.GetComponent<Level>().number = level.number;
        env.GetComponent<Level>().children = level.children;
        //env.GetComponent<Level>().components = level.components;

        env.GetComponent<Level>().components =new List<string>();
        foreach(string s in level.components){
            env.GetComponent<Level>().components.Add(s);
        }
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
    }
}
