using UnityEngine;
using System.Collections;

public class Area : MonoBehaviour {

	public Vector3 standPosition = Vector3.zero;
	public int sortingLayerNr = 2;
	public GameObject characterStance;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube (standPosition, new Vector3(0.3f,0.3f,1));
	}


}
