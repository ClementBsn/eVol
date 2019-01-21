using UnityEngine;

public class Velocity : MonoBehaviour {
	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).

	public float speed=2.5f;
	public float min=2f;
	public float max=15f;
	public float acceleration = 0.1f;
}