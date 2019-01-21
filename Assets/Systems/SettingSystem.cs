using UnityEngine;
using FYFY;

public class SettingSystem : FSystem {
	/*private Family _selectedGO = FamilyManager.getFamily(
		new AllOfComponents(typeof(Selected)));
	private GameObject dinosaureGO=FamilyManager.getFamily(
		new AllOfComponents(typeof(Dinosaure)))[0];
	public SettingSystem(){
		foreach (GameObject go in _selectedGO) {
			dinosaureGO.GetComponent<Mass> ().mass = dinosaureGO.GetComponent<Mass> ().mass+  go.GetComponent<Mass> ().mass;
			dinosaureGO.GetComponent<Velocity> ().speed = dinosaureGO.GetComponent<Velocity> ().speed+ go.GetComponent<Velocity> ().speed;
			dinosaureGO.GetComponent<Velocity> ().acceleration = dinosaureGO.GetComponent<Velocity> ().acceleration+ go.GetComponent<Velocity> ().acceleration;
		}
	}*/
	// Use this to update member variables when system pause. 
	// Advice: avoid to update your families inside this function.
	protected override void onPause(int currentFrame) {
	}

	// Use this to update member variables when system resume.
	// Advice: avoid to update your families inside this function.
	protected override void onResume(int currentFrame){
	}
}