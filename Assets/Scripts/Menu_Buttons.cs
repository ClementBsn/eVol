using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Buttons : MonoBehaviour {
	public GameObject MenuPanel;
	public GameObject LevelSelectPanel;
	public GameObject RulesPanel;
	public GameObject Game;
	public GameObject MenuCanvas;
	public GameObject MutationPanel;
	public GameObject mutationButtonTemplate;
	public GameObject ScoreText;
	public GameObject InformationPanel;
	public GameObject InformationMark;
	
	// Use this for initialization
	void Start () {
		ShowMenuPanel ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowLevelPanel(){
		InformationMark.SetActive (false);
		RulesPanel.SetActive (false);
		MenuPanel.SetActive (false);
		LevelSelectPanel.SetActive (true);
	}

	public void showMutationPanel(){
		InformationMark.SetActive (false);
		MutationPanel.SetActive(true);
		MenuPanel.SetActive (false);
	}

	public void ShowMenuPanel(){
		InformationMark.SetActive (false);
		MutationPanel.SetActive(false);
		RulesPanel.SetActive (false);
		InformationPanel.SetActive(false);
		MenuPanel.SetActive (true);
		LevelSelectPanel.SetActive (false);
		Game.SetActive(false);
		ScoreText.SetActive(false);
	}

	public void ShowRulesPanel(){
		InformationMark.SetActive (false);
		RulesPanel.SetActive (true);
		MenuPanel.SetActive (false);
		LevelSelectPanel.SetActive (false);
		Game.SetActive(false);

	}

	public void ShowInfoPanel(){

		MutationPanel.SetActive(false);
		RulesPanel.SetActive (false);
		MenuPanel.SetActive (false);
		InformationPanel.SetActive(true);
		LevelSelectPanel.SetActive (false);

	}

	public void startGame(){
		InformationMark.SetActive (true);
		//MenuCanvas.SetActive(false);
		MenuPanel.SetActive (false);
		LevelSelectPanel.SetActive (false);
		Game.SetActive(true);
		RulesPanel.SetActive (false);
		ScoreText.SetActive(true);
	}
}
