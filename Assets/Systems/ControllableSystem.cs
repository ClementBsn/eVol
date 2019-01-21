using UnityEngine;
using FYFY;
using ObstacleType;


public class ControllableSystem : FSystem {

	

	private Family _obstacles = FamilyManager.getFamily(new AllOfComponents(typeof(Obstacle)));
	private Family _controllableGO = FamilyManager.getFamily(
		new AllOfComponents(typeof(Dinosaure)));
	private Family _environment = FamilyManager.getFamily(new AllOfComponents(typeof(ScrollingBG)));
	private float limitUp;

	public ControllableSystem(){
		float heightDino = 0;
		foreach (GameObject go in _controllableGO){
			Bounds boundsDino = go.GetComponent<Renderer>().bounds;
			heightDino = (boundsDino.size.y / 2);}

		foreach (GameObject go in _obstacles){
			if (go.GetComponent<Obstacle>().type == obstacleType.Background){
				Bounds boundsBG = go.GetComponent<Renderer>().bounds;
				limitUp = boundsBG.center.y + (boundsBG.size.y / 2) - heightDino;
			}
		}
	}
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}

	// Use to process your families.
	protected override void onProcess(int familiesUpdateCount) {
		//Parcours des GameObjects inclus dans la famille
		foreach (GameObject go in _controllableGO) {
			
			go.GetComponent<Animator> ().enabled=false;
			//récup des composants du GO courant
			Transform tr = go.GetComponent<Transform> ();
			Velocity mv = go.GetComponent<Velocity> ();

			Vector3 movement = Vector3.zero;

			if(go.GetComponent<Dinosaure>().levelDone){
				go.transform.position += new Vector3(1,1,0) * Time.deltaTime * 5;

				//go.GetComponent<Rigidbody2D>().AddForce((Vector3.up * 250)+(Vector3.right * 10));
			}

			if( !go.GetComponent<Dinosaure>().isDead &&  !go.GetComponent<Dinosaure>().levelDone ){
				//Détermination du vecteur de déplacement en fonction de la touche pressée
				if ((Input.GetKey (KeyCode.Space) == true) && (!go.GetComponent<Dinosaure>().reachedUp)) {
					if (tr.position.y < limitUp){
						go.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 250);
						tr.rotation = Quaternion.Euler(0, 0, 0);

					}
					go.GetComponent<Animator> ().enabled=true;
                }

				if (Input.GetKey (KeyCode.DownArrow) == true) {
					movement += Vector3.down; 
					go.GetComponent<Dinosaure>().reachedUp=false;
					go.GetComponent<Animator> ().enabled=false;

					//A Vérifier TODO
					tr.rotation =(Quaternion.Euler(0, 0, -1 * mv.speed * 6));
				}

				//To avoid the reversal of the movement vector when the speed is negative
				if(mv.speed<mv.min){
					mv.speed = mv.min;
					//movement *= -1;
				}

				if (Input.GetKey (KeyCode.RightArrow) == true) {
					mv.speed += mv.acceleration;
					if(mv.speed>mv.max){
						mv.speed=mv.max;
					}
				}
				if (Input.GetKey (KeyCode.LeftArrow) == true) {
					mv.speed -= mv.acceleration;

					if(mv.speed<mv.min){
						mv.speed=mv.min;
					}
					
				}
				tr.position += movement * mv.speed * Time.deltaTime ;

				if (tr.position.y >= limitUp){
					// Effet de rebondissement quand on touche le haut.
					//go.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 25);

					//Bloage du mouvement
					tr.position = new Vector3(tr.position.x,limitUp,tr.position.z);
				}

				foreach (GameObject envGO in _environment){
					float scrollspeed=-1*mv.speed;
	
					envGO.GetComponent<ScrollingBG>().GetComponent<Rigidbody2D>().velocity =new Vector2(scrollspeed,0);
				}
				// TODO : Coefficient à déterminer
				go.GetComponent<Dinosaure>().energy -= mv.speed * Time.deltaTime * 3;

				if(go.GetComponent<Dinosaure>().energy<=0){
					go.GetComponent<Dinosaure>().isDead=true;
				}
			}

		}

	}
}