using UnityEngine;

//Class that describes the state of the dino.
public class Dinosaure : MonoBehaviour {
	public bool isDead = false;
	public bool reachedUp = false;
	public bool levelDone = false;
	public int score=0;
	public float energy = 1f;
	public Vector3 initialPos=new Vector3(0,0,0);
    

    public float angle = 0.0f;
}