using UnityEngine;
using FYFY;
using FYFY_plugins.TriggerManager;

public class EatingSystem : FSystem {
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.

	//Construction d'une famille incluant tous les go contenant
	//le composant triggered2D (en collision)
	private Family _triggeredGO = FamilyManager.getFamily(new AllOfComponents(typeof(Triggered2D),typeof(Dinosaure)));

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
				if (target.GetComponent<Obstacle> () == null) {
					int weight = 1;
					if(target.tag == "special_coin"){
						weight = 2;
					}


					GameObjectManager.unbind (target);
					Object.Destroy (target);

					go.GetComponent<Dinosaure>().score+=1 * weight;
					go.GetComponent<Dinosaure>().energy+=5 *weight;
				}
			}
		}
	}
}