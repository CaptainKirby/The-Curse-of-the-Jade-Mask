using UnityEngine;
using System.Collections;

public class LoadOptions : MonoBehaviour {

	public GameObject optionsObject;
	void Start()
	{
		optionsObject = GameObject.Find ("OptionsCanvas");
//		optionsObject.SetActive (false);
	}
	public void OpenOptions()
	{
//		Debug.Log ("NGU#");
//		optionsObject.SetActive (true);
		optionsObject.GetComponent<Canvas> ().enabled = true;
	}
	public void CloseOptions()
	{
		optionsObject.GetComponent<Canvas> ().enabled = false;

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
