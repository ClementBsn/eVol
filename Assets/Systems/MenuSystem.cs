using UnityEngine;
using FYFY;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuSystem : FSystem {
	private Family _levelButtons = FamilyManager.getFamily(
        new AllOfComponents(typeof(Level)), new NoneOfComponents(typeof(ScrollingBG)));
	private Family _backgrounds =FamilyManager.getFamily(
        new AllOfComponents(typeof(Background)));
	private Family _panels = FamilyManager.getFamily(new AllOfComponents(typeof(Panel)));
	private Family _dinos = FamilyManager.getFamily(new AllOfComponents(typeof(Dinosaure)));
	private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
	private Family _menuButtons = FamilyManager.getFamily(new AllOfComponents(typeof(Button)), 
		new NoneOfTags("mutation_button","mutation_button_selected"));

	private GameObject menuPanel=null, levelPanel=null, rulesPanel=null, gamePanel=null, 
		mutationPanel=null, gamePropertyPanel=null, lostPanel=null, wonPanel=null, levelDetailsPanel=null,eggAnimationPanel=null,startOnSpacePanel=null;

	private GameObject menuBG=null, levelBG=null,gameBG=null;
	private GameObject env;
	private Vector3 initialPosEnv;
	private GameObject backButton = null;

	private int branchLength = 30;
    private int buttonNb = 1; 
    private int childrenLimit = 2;


	public MenuSystem() {
		setBackgrounds();
		foreach (GameObject go in _levelButtons)
        {
            setLevelButton(go);
            
        }

		foreach (GameObject envGO in _environment){env=envGO;}
		initialPosEnv=env.transform.position;

		setPanels();
		showMenuPanel();

		foreach(GameObject go in _menuButtons){
            Button bt = go.GetComponent<Button>();
            bt.onClick.AddListener(delegate { changePanel(go); });
		}
	}
	void setBackgrounds (){
		
		foreach (GameObject bg in _backgrounds){
			switch(bg.GetComponent<Background>().bgName){
			case "GameBackground":
			gameBG=bg;
			break;
			case "LevelBackground":
			levelBG=bg;
			break;
			case "MenuBackground":
			menuBG=bg;
			break;
			}

		}
	}
	

	void setPanels (){
		foreach (GameObject b in _menuButtons){
			if(b.tag == "back_button"){
				backButton=b;
			}
			if(backButton != null){
				break;
			}
		}
		foreach (GameObject panel in _panels){
			switch(panel.GetComponent<Panel>().panelName){
			case "GamePropertyPanel":
			gamePropertyPanel=panel;
			break;
			case "GamePanel":
			gamePanel=panel;
			break;
			case "LevelPanel":
			levelPanel=panel;
			break;
			case "MutationPanel":
			mutationPanel=panel;
			break;
			case "RulesPanel":
			rulesPanel=panel;
			break;
			case "MenuPanel":
			menuPanel=panel;
			break;
			case "LostPanel":
			lostPanel=panel;
			break;
			case "WonPanel":
			wonPanel=panel;
			break;
			case "EggAnimationPanel":
			eggAnimationPanel=panel;
			break;
			case "StartOnSpacePanel":
			startOnSpacePanel=panel;
			break;
			}

		}
	}

	void createNewLevel(string name,List<string> attributs,  Image img) {
		int actualLevelNb = env.GetComponent<Level>().number;
		GameObject actualLevelButton = null;
		foreach (GameObject go in _levelButtons)
        {
            if(go.GetComponent<Level>().number == actualLevelNb){
            	actualLevelButton=go;
            }
            if(actualLevelButton !=null){
            	break;
            }

        }
        addNewChild(actualLevelButton, name, attributs);

	}
	void saveLevel(){
		bool saved=false;
		foreach (GameObject go in _levelButtons)
        {
        	Level level=go.GetComponent<Level>();

            if(level.number == env.GetComponent<Level>().number){

            	level.bestScore=env.GetComponent<Level>().bestScore;
            	
				
	            GameObject details = go.transform.GetChild(1).gameObject;
	            setLevelButtonDetails(details);
				saved=true;
            }

            if(saved){
				break;
			}


        }
	}
	void reinitialiseEnvironment(){
		foreach (GameObject dinoGO in _dinos){
			GameObjectManager.unbind(dinoGO);
			Object.Destroy(dinoGO);
		}
		env.transform.position=initialPosEnv;

	}
	void showMenuPanel(){
		//reinitialiseEnvironment();
		saveLevel();
		gamePropertyPanel.SetActive(false);
		mutationPanel.SetActive(false);
		rulesPanel.SetActive (false);
		menuPanel.SetActive (true);
		levelPanel.SetActive (false);
		gamePanel.SetActive(false);
		lostPanel.SetActive(false);
		wonPanel.SetActive(false);
		backButton.SetActive(false);
		gameBG.SetActive(false);
		levelBG.SetActive(false);
		menuBG.SetActive(true);
	}
	void showEggAnim(){
		eggAnimationPanel.SetActive(true);
		startOnSpacePanel.SetActive(true);
	}
	void startGame(){

		closeAllDetailPanel();
		reinitialiseEnvironment();
		GameObject dino = GameObject.Instantiate(env.GetComponent<Level>().birdPrefab);
		dino.transform.parent=env.transform.parent;
		GameObjectManager.bind(dino);
		gamePropertyPanel.SetActive(true);
		rulesPanel.SetActive(false);
		menuPanel.SetActive(false);
		levelPanel.SetActive(false);
		gamePanel.SetActive(true);
		lostPanel.SetActive(false);
		wonPanel.SetActive(false);
		backButton.SetActive(true);
		gameBG.SetActive(true);
		levelBG.SetActive(false);
		menuBG.SetActive(false);
	}
	void showLevelPanel(){
		gamePanel.SetActive(false);
		rulesPanel.SetActive(false);
		menuPanel.SetActive(false);
		levelPanel.SetActive(true);
		lostPanel.SetActive(false);
		wonPanel.SetActive(false);
		mutationPanel.SetActive(false);
		gamePropertyPanel.SetActive(false);
		backButton.SetActive(true);
		gameBG.SetActive(false);
		levelBG.SetActive(true);
		menuBG.SetActive(false);

	}
	void showMutationPanel(){
		saveLevel();
		mutationPanel.SetActive(true);
		menuPanel.SetActive(false);
		lostPanel.SetActive(false);
		wonPanel.SetActive(false);
		backButton.SetActive(true);
		gamePanel.SetActive(false);
		gamePropertyPanel.SetActive(false);
		levelPanel.SetActive(false);

		gameBG.SetActive(true);
		levelBG.SetActive(false);
		menuBG.SetActive(false);
	}

	void showRulesPanel(){
		rulesPanel.SetActive(true);
		menuPanel.SetActive(false);
		levelPanel.SetActive(false);
		gamePanel.SetActive(false);
		lostPanel.SetActive(false);
		wonPanel.SetActive(false);
		backButton.SetActive(true);

		gameBG.SetActive(true);
		levelBG.SetActive(false);
		menuBG.SetActive(false);
	}
	void changePanel(GameObject go){
		switch(go.tag){
			case "play_button":
			showEggAnim();
			//startGame();
			break;
			case "level_button":
			showLevelPanel();
			break;
			case "mutations_button":
			showMutationPanel();
			break;
			case "rules_button":
			showRulesPanel();
			break;
			case "back_button":
			showMenuPanel();
			break;
			case "replay_button":
			showMenuPanel();
			//showEggAnim();
			startGame();
			break;
			case "next_button":
			showMutationPanel();
			break;
			case "create_dino_button":
			createNewLevel(env.GetComponent<Level>().name,env.GetComponent<Level>().components,null);
			break;
		}
	}

	void setLevel(GameObject go){
		Level level = go.GetComponent<Level>();

		env.GetComponent<Level>().name=level.name;
		env.GetComponent<Level>().number=level.number;
		env.GetComponent<Level>().children=level.children;
        env.GetComponent<Level>().components = new List<string>();
        foreach (string s in level.components)
        {
            env.GetComponent<Level>().components.Add(s);
        }
        env.GetComponent<Level>().bestScore=level.bestScore;
		env.GetComponent<Level>().obstaclesNb=level.obstaclesNb;
		env.GetComponent<Level>().coinsNb=level.coinsNb;
		env.GetComponent<Level>().length=level.length;

		env.GetComponent<Level>().massBonus=level.massBonus;
		env.GetComponent<Level>().dragBonus=level.dragBonus;
		env.GetComponent<Level>().energyBonus=level.energyBonus;
		env.GetComponent<Level>().powerFlyBonus=level.powerFlyBonus;
		env.GetComponent<Level>().maxVelocityBonus=level.maxVelocityBonus;
		env.GetComponent<Level>().agilityBonus=level.agilityBonus;
		env.GetComponent<Level>().initialized=false;

	}
	void startLevel(GameObject go){
		saveLevel();
		setLevel(go);
		showEggAnim();
		//startGame();

	}

	//arguments : GO = parent button, name of the new level, list of attributs
    void addNewChild(GameObject GO, string name, List<string> attributs){
        Level level = env.GetComponent<Level>();
        int childCount = GO.GetComponent<Level>().children.Count;
        // We have a limit of creation of levels possibles (if not tree will be too big)
        if(childCount<childrenLimit){
            buttonNb ++ ;
            //Creating new button
            GameObject newGO = Object.Instantiate<GameObject>(GO);
            GO.GetComponent<Level>().children.Add(buttonNb);

            //Setting level details
            newGO.GetComponent<Level>().name = name;
            newGO.GetComponent<Level>().components = new List<string>();
            foreach (string s in attributs)
            {
                newGO.GetComponent<Level>().components.Add(s);
            }
            newGO.GetComponent<Level>().bestScore = 0;
            newGO.GetComponent<Level>().massBonus = level.massBonus;
            newGO.GetComponent<Level>().dragBonus = level.dragBonus;
            newGO.GetComponent<Level>().energyBonus = level.energyBonus;
            newGO.GetComponent<Level>().powerFlyBonus = level.powerFlyBonus;
            newGO.GetComponent<Level>().maxVelocityBonus = level.maxVelocityBonus;
            newGO.GetComponent<Level>().agilityBonus = level.agilityBonus;


            //Place it left or right.
            newGO.transform.position += new Vector3((float)(branchLength * System.Math.Pow(-1,childCount)), branchLength,0);
            newGO.transform.SetParent(GO.GetComponent<Transform>().parent, false);
            newGO.GetComponent<Level>().number = buttonNb;

            newGO.GetComponent<Level>().children = new List<int>();
            LineRenderer childsLR =newGO.GetComponent<LineRenderer>();
            if (childsLR != null){
                Object.Destroy(childsLR);
            }
            
            setLevelButton(newGO);
            
            if (childCount < 1){
                LineRenderer lineRenderer = GO.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.SetPosition(1,new Vector3( branchLength - 4,branchLength - 4,-1));
                lineRenderer.SetPosition(0,new Vector3( 4,4,0));
                lineRenderer.startWidth=0.1f;
                lineRenderer.endWidth=0.1f;
                lineRenderer.useWorldSpace=false;
                Color c =GO.GetComponent<Image>().color;
                c.a=1;
                lineRenderer.SetColors(c, c);
            }
            else {
                LineRenderer lineRenderer = GO.GetComponent<LineRenderer>();
                int initialPosCount = lineRenderer.positionCount;
                lineRenderer.positionCount += 2;
                lineRenderer.SetPosition(initialPosCount, new Vector3(4,4,0));
                lineRenderer.SetPosition(initialPosCount + 1,new Vector3((float)((branchLength - 4) * System.Math.Pow(-1,childCount)),branchLength - 4,-1));
                
            }
            GameObjectManager.bind (newGO);

            showLevelPanel();

            

         }
        
    }
    void closeAllDetailPanel(){
    	foreach (GameObject but in _levelButtons)
	    {
	        but.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void displayPanel(GameObject go){
    	GameObject child = go.transform.GetChild(1).gameObject;
    	if(child.activeSelf){
			child.SetActive(false);
    	}
    	else {

	    	setLevel(go);
	    	closeAllDetailPanel();
	        child.SetActive(true);
        }

        
        
    }

    //Setting text & add listener
    void setLevelButton(GameObject go){
    	Level level=go.GetComponent<Level>();

    	go.GetComponentInChildren<Text>().text = go.GetComponent<Level>().number.ToString();
    	Button bt = go.GetComponent<Button>();
        //bt.onClick.AddListener(delegate { addNewChild(go); });
        bt.onClick.AddListener(delegate { displayPanel(go); });
        setLevelButtonDetails(go.transform.GetChild(1).gameObject);
        GameObject details = go.transform.GetChild(1).gameObject;

        foreach (Transform child in details.transform)
		{
  			if(child.gameObject.tag == "choose_level_button"){
				child.gameObject.GetComponent<Button>().onClick.AddListener(delegate {startLevel(go);});

  			}
		}
    }

    void setLevelButtonDetails(GameObject details){
    	//GameObject details = go.transform.GetChild(1).gameObject;
    	Transform details_tr=details.transform;
    	Level level=details_tr.parent.GetComponent<Level>();

        foreach (Transform child in details_tr)
		{
  			switch(child.gameObject.tag){
  				case "attributs_liste":
  				string attributs ="";
  				foreach(string s in level.components){
  					attributs += s+"\n";
  				}
  				child.gameObject.GetComponentInChildren<Text>().text = attributs;
  				break;
  				case "dino_image":
  				break;
  				case "best_score":
  				child.gameObject.GetComponentInChildren<Text>().text = "Record : " + level.bestScore + " points";
  				break;
  				case "name_input":
  				child.gameObject.GetComponentInChildren<Text>().text = level.name ;
  				break;
  				case "next_button":
  				child.gameObject.GetComponent<Button>().interactable= (level.bestScore >0);
				break;
				

  			}
		}


    }
    // Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		if ((Input.GetKey (KeyCode.Space) == true) && (eggAnimationPanel.activeSelf)) {
			startOnSpacePanel.SetActive(false);
			eggAnimationPanel.SetActive(false);
			startGame();
		}
	}

    
    

	
}