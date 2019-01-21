using UnityEngine;
using FYFY;
using UnityEditor;
using UnityEngine.UI; 
using System.Collections.Generic;
public class GameSystem : FSystem {
	
	private Family _scores = FamilyManager.getFamily(new AllOfComponents(typeof(Score)));
	private Family _dinoGO = FamilyManager.getFamily(new AllOfComponents(typeof(Dinosaure)));
	private Family _healthBars = FamilyManager.getFamily(new AllOfComponents(typeof(HealthBar)));
	private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
	
	private GameObject scoreGO,bestScoreGO;
	private GameObject env;
	private GameObject dinoGO=null;
	
	public GameSystem() {
		_dinoGO.addEntryCallback(onNewDinoGO);

		foreach (GameObject envi in _environment) {
			env=envi;
		}
		foreach (GameObject score in _scores) {
			if(score.GetComponent<Score>().actualScore){
				scoreGO=score; 
			}
			else{
				bestScoreGO=score;
			}
				
		}
    }
    /*Remove every obstacles in level*/
    void clearEnvironment(){
    	int bgLayerNb = LayerMask.NameToLayer("background");
    	foreach (Transform child in env.transform)
        {
            if (child.gameObject.layer!=bgLayerNb){
            	GameObjectManager.unbind (child.gameObject);
    			Object.Destroy(child.gameObject);

    		}
        }	
    }

    void generateFinishLine(int length, GameObject lineModel){
    	GameObject lineGO = GameObject.Instantiate(lineModel);
	    lineGO.transform.SetParent(env.GetComponent<Transform>(), false);
        lineGO.transform.position += new Vector3(length + 10f,0,0);
		GameObjectManager.bind(lineGO);

    }

    void onNewDinoGO(GameObject go){
    	foreach (GameObject dino in _dinoGO)
        {
        	dinoGO=dino;
        }
        clearEnvironment();
        Level levelSettings= env.GetComponent<Level>();
		levelSettings.initialized=false;
        generateLevel();
        Rigidbody2D rb = dinoGO.GetComponent<Rigidbody2D>();
        rb.mass += levelSettings.massBonus;
        rb.drag += levelSettings.dragBonus;
       
        
    }

    void generateLevel(){
		Level levelSettings= env.GetComponent<Level>();

		if(!levelSettings.initialized){
			int length = levelSettings.length;

    		GameObject lineModel = levelSettings.finishLinePrefab;

			generateFinishLine(length,lineModel);

			GameObject coinModel = levelSettings.coinPrefab;
			GameObject obstacleModel = levelSettings.obstaclePrefab;
			GameObject windModel = levelSettings.windPrefab;

			List<int> emptySpaceForWind=new List<int>();
			float widthWind = windModel.GetComponent<Renderer>().bounds.size.x;
			int spaceBWind = 20;
			for(int i=0; i < (int)length/(widthWind+spaceBWind); i++){
				emptySpaceForWind.Add(i);
			}

			//horizontal space between objects 
			//(we define this so that every coin is eatable and every obstacle avoidable)
			int spaceBCoins = 5;
			float coinMaxWidth = coinModel.GetComponent<Renderer>().bounds.size.x ;
			
			List<int> emptySpace = new List<int> ();
			Debug.Log((int)length/(coinMaxWidth+spaceBCoins));
			for(int i=0; i < (int)length/(coinMaxWidth+spaceBCoins); i++){
				emptySpace.Add(i);
			}

			int spaceBObs = 10;
			List<int> emptySpaceObstacles = new List<int> ();
			float widthObstacle = obstacleModel.GetComponent<Renderer>().bounds.size.x;
			for(int i=0; i < (int)length/(widthObstacle+spaceBObs); i++){
				emptySpaceObstacles.Add(i);
			}

			/******BUILDING GENERATION*************/

			for(int i=0; i < levelSettings.obstaclesNb; i++){
				int ran =Random.Range(0,emptySpaceObstacles.Count);
				int r = emptySpaceObstacles[ran];
				emptySpaceObstacles.Remove(r);
				GameObject obstacleGO = GameObject.Instantiate(obstacleModel);
	            obstacleGO.transform.SetParent(env.GetComponent<Transform>(), false);
	           
	            //obstacleGO.transform.position += new Vector3(r * spaceBObs,Random.Range(-2,10),0);
	            int scale = Random.Range(1,4);
	            obstacleGO.transform.localScale = new Vector3(1f, scale, 0);
	            obstacleGO.transform.position += new Vector3(r * spaceBObs,0,0);
				GameObjectManager.bind(obstacleGO);
				int lim0 = r - (int)(widthObstacle/2) ;
				int lim1 = r + (int)(widthObstacle/2) ;
				/*foreach (int j in emptySpace){
					if (j > lim0 && j<lim1) {
						emptySpace.Remove(j);
					}
				}*/
				
	           
			}

			/******COINS GENERATION*************/

			for(int i=0; i<levelSettings.coinsNb; i++){
				int ran =Random.Range(0,emptySpace.Count);
				if(emptySpace.Count>0){
					int r = emptySpace[ran];
					emptySpace.Remove(r);
					GameObject coinGO = GameObject.Instantiate(coinModel);
		            coinGO.transform.SetParent(env.GetComponent<Transform>(), false);
		            coinGO.transform.position += new Vector3(r * spaceBCoins,Random.Range(-4,3),0);
		            int alea = Random.Range(0,10);
		            if(alea < 3){
		            	//coinGO.transform.localScale += new Vector3(1,1,1);
		            	coinGO.tag = "special_coin";
		            	coinGO.GetComponent<SpriteRenderer>().color = Color.green;
		            	/*if(alea < 1){
		            		coinGO.transform.localScale += new Vector3(1,1,1);
		            	}*/
		            }
		            else{
		            	coinGO.tag = "normal_coin";
		            }
					GameObjectManager.bind(coinGO);
				}
			}

			/******WIND GENERATION*************/
			for(int i=0; i<levelSettings.windsNb; i++){
				int ran =Random.Range(0,emptySpaceForWind.Count);
				int r = emptySpaceForWind[ran];
				emptySpaceForWind.Remove(r);
				GameObject windGO = GameObject.Instantiate(windModel);

				// Set the wind direction (p=1/2)
				ran=Random.Range(0,2);
				if(ran==0){
					windGO.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
				}
	            windGO.transform.SetParent(env.GetComponent<Transform>(), false);
	            windGO.transform.position += new Vector3(r * spaceBWind,Random.Range(-4,3),0);
				GameObjectManager.bind(windGO);
			}


			levelSettings.initialized=true;
			bestScoreGO.GetComponent<UnityEngine.UI.Text>().text = "Record : " + env.GetComponent<Level>().bestScore; 

			
		}


		

	}
    


	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		if(dinoGO != null){
			scoreGO.GetComponent<UnityEngine.UI.Text>().text = "Score : "+dinoGO.GetComponent<Dinosaure>().score; 
			
			foreach (GameObject healthBar in _healthBars){
				Image hB = healthBar.GetComponent<Image>();
				hB.fillAmount = (float)(dinoGO.GetComponent<Dinosaure>().energy / 100);

			}
		}

	}
}