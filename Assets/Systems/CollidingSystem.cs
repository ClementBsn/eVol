using UnityEngine;
using FYFY;

using ObstacleType;
using FYFY_plugins.TriggerManager;

/*
 * Possibilité de faire une Callback lorsqu'un object rentre ou sort d'une famille
 * 
 * Mieux faire oiseau qui est trigerred car ainsi on a l'info de combien d'oiseau rentrent en collision avec l'object
 */

public class CollidingSystem : FSystem {
	private Family _panels = FamilyManager.getFamily(new AllOfComponents(typeof(Panel)));
	private Family _triggeredGO = FamilyManager.getFamily(new AllOfComponents(typeof(Obstacle),typeof(Triggered2D)));
	private Family _obstaclesGO = FamilyManager.getFamily(new AllOfComponents(typeof(Obstacle)));
	private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
	
	private GameObject env;
	private GameObject lostPanel=null,
		wonPanel=null;
	//Will be affected at the first collision with the up limit
	//private int limitUpInstanceId = 0;

	//Add listener to know when collision with limit up is over  (to re enable the movement upward)
	public CollidingSystem() {
		foreach (GameObject environment in _environment) {
			env=environment;
		}
		//_triggeredGO.addExitCallback(triggerListener);
		foreach (GameObject panel in _panels){
			if(panel.GetComponent<Panel>().panelName == "LostPanel"){
				lostPanel=panel;
			}
			if(panel.GetComponent<Panel>().panelName == "WonPanel"){
				wonPanel=panel;
			}
		}
	}

	void triggerListener(int goInstanceId){
		/*if (limitUpInstanceId == goInstanceId){
			foreach (GameObject dino in _dinoGO) {
				dino.GetComponent<Dinosaure>().reachedUp=false;
			}
		}*/


	}

	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	
	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {

		foreach (GameObject go in _triggeredGO) {

			Triggered2D t2d = go.GetComponent<Triggered2D> ();
			foreach (GameObject target in t2d.Targets) {
				//While level is not finished we handle
				if(!target.GetComponent<Dinosaure>().levelDone){

					//Check if the obstacle is fatal, then die
					if(go.GetComponent<Obstacle>().fatal ){

						//target.GetComponent<Transform>().Rotate(new Vector3 (5,0,0));
						//Dino is dead
						target.GetComponent<Dinosaure>().isDead=true;
						//Make it fall
						target.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

						//Remove the trigger
						target.GetComponent<CapsuleCollider2D>().isTrigger=false;
						//Stop the animation
						target.GetComponent<Animator> ().enabled=true;
						target.GetComponent<Animator> ().SetBool("died",true);
						
						//Stop scrolling
						env.GetComponent<ScrollingBG>().GetComponent<Rigidbody2D>().velocity =new Vector2(0,0);
						//Remove wind (or there can be collision between windGO and bird)
				    	foreach (GameObject obstacleGO in _obstaclesGO)
				        {
				            if (obstacleGO.GetComponent<Obstacle>().type == obstacleType.Wind){
				            	GameObjectManager.unbind (obstacleGO);
				    			Object.Destroy(obstacleGO);

				    		}
				        }

				        //Display Loser Panel
						lostPanel.SetActive(true);
		
						
					}

					//If it's not check the type of obstacle
					else{

						switch(go.GetComponent<Obstacle>().type){
							case obstacleType.Wind:
								target.GetComponent<Velocity>().speed-=go.GetComponent<Obstacle>().slowRatio;
								break;
							case obstacleType.Level:
								target.GetComponent<Animator> ().enabled=false;
								target.GetComponent<Dinosaure>().levelDone=true;
								//target.GetComponent<Rigidbody2D>().mass= 1;
								//target.GetComponent<Rigidbody2D>().drag= 1;
								env.GetComponent<ScrollingBG>().GetComponent<Rigidbody2D>().velocity =new Vector2(0,0);
								int score =target.GetComponent<Dinosaure>().score;
								if(env.GetComponent<Level>().bestScore < score){
									env.GetComponent<Level>().bestScore =score;
								}
								wonPanel.SetActive(true);
								//NextLevel()
								break;
							case obstacleType.LimitUp:
								//if(limitUpInstanceId == 0)
								//	limitUpInstanceId = go.GetInstanceID();
								//target.GetComponent<Dinosaure>().reachedUp=true;
								break;
							default : 
								
								break;


						}
						
					}
				}

				
			}
		}
	}
}