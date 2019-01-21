using UnityEngine;
using ObstacleType;

public class Obstacle : MonoBehaviour {

	// Advice: FYFY component aims to contain only public members (according to Entity-Component-System paradigm).
	public bool fatal=true;

	public float slowRatio=0.01f;

	public obstacleType type=obstacleType.Wind;

}