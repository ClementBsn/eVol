using UnityEngine;
using System.Collections.Generic;


public class Level : MonoBehaviour {

	public int number;
	public List<int> children;
	public ReferenceDinosaure reference;
	public string name;
	public List<string> components = new List<string>();
	// or public List<Component> attributs;
	public int bestScore = 0;

	public int obstaclesNb;
	public int coinsNb;

	public int windsNb;
	public int length;
	public GameObject obstaclePrefab;
	public GameObject coinPrefab;
	public GameObject windPrefab;
	public GameObject finishLinePrefab;
	public bool initialized=false;
	public GameObject birdPrefab;
	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
	public float massBonus=0f,dragBonus=0f, energyBonus =0f, powerFlyBonus=0f, maxVelocityBonus=0f, agilityBonus=0f;

}