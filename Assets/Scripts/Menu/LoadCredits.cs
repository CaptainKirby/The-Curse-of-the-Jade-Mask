using UnityEngine;
using System.Collections;

public class LoadCredits
: MonoBehaviour {

	public GameObject creditsObject;
	void Start()
	{
		creditsObject = GameObject.Find ("Credits");
//		optionsObject.SetActive (false);
	}
	public void OpenCredits()
	{
//		Debug.Log ("NGU#");
//		optionsObject.SetActive (true);
		creditsObject.GetComponent<Canvas> ().enabled = true;
	}
	public void CloseCredits()
	{
		creditsObject.GetComponent<Canvas> ().enabled = false;

	}

//	public void OpenOptionsScene()
//	{
//		Application.LoadLevel ("Options");
//	}
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
