﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	//Here is a private reference only this class can access
	private static GameController _instance;

	private bool loadedScene;
//	[HideInInspector]
	public GameObject uiCanvas = null;
	public bool showBlackBars = true;
//	[HideInInspector]
	public InteractionMenu interactionMenu;

	public GameSettings gameSettings;


	[HideInInspector]
	public GameObject dialogueHolder;

	[HideInInspector]
	public DialogueController dCon;

	[HideInInspector]
	public Inventory inventoryController;

	[HideInInspector]
	public GameObject currentArea;

	[HideInInspector]
	public enum PlayerState{OpenArea, Zoom};
	public PlayerState playerState = PlayerState.OpenArea;

	public GameObject audioClipSource;

	public GameObject voiceSource;

	public GameObject outroImage;
	//This is the public reference that other classes will use
	public static GameController instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<GameController>();
			return _instance;
		}
	}


	void Awake ()  
	{

		outroImage = GameObject.Find ("OutroImage");
		if(outroImage != null)
			outroImage.SetActive (false);

		if (Application.loadedLevelName != "MainMenu") {
			if (gameSettings.uiCanvas != null) {
				//instantiate and declare
				this.uiCanvas = Instantiate (gameSettings.uiCanvas, Vector3.zero, Quaternion.identity) as GameObject;
				uiCanvas.gameObject.name = "UiCanvas";
				if (!showBlackBars) {
					uiCanvas.transform.FindChild ("Blackbars").gameObject.SetActive (false);
				}
			} else {
				Debug.Log ("No UI Canvas assigned in Game Settings!");
			}
		}
		if (Application.loadedLevelName != "MainMenu") {
			if (gameSettings.uiInteractioneMenu != null) {
				interactionMenu = Instantiate (gameSettings.uiInteractioneMenu, Vector3.zero, Quaternion.identity) as InteractionMenu;
				interactionMenu.gameObject.name = "InteractionMenu";
				interactionMenu.gameObject.transform.SetParent (uiCanvas.transform);

			} else {
				Debug.Log ("No Interaction Menu assigned in Game Settings!");
			}
		}
		if (Application.loadedLevelName != "MainMenu") 
		{
			inventoryController = uiCanvas.GetComponentInChildren<Inventory> ();
		}


		if(gameSettings.audioClipSource != null)
		{
			audioClipSource = Instantiate(gameSettings.audioClipSource, Vector3.zero, Quaternion.identity) as GameObject;
			audioClipSource.gameObject.name = "AudioClipSource";
			audioClipSource.gameObject.transform.SetParent(GameObject.Find("AUDIO").transform);
			
		}
		else
		{
			Debug.Log ("No Interaction Menu assigned in Game Settings!");
		}

		if(gameSettings.voiceSource != null)
		{
			voiceSource = Instantiate(gameSettings.voiceSource, Vector3.zero, Quaternion.identity) as GameObject;
			voiceSource.gameObject.name = "VoiceSource";
			voiceSource.gameObject.transform.SetParent(GameObject.Find("AUDIO").transform);
			
		}
		else
		{
			Debug.Log ("No VS assigned in Game Settings!");
		}
		if (Application.loadedLevelName != "MainMenu") {
			if (dialogueHolder == null) {
				dialogueHolder = uiCanvas.transform.FindChild ("Dialogue").gameObject;

			}
			if (dCon == null) {
				dCon = GetComponent<DialogueController> ();
			}
		}

	}


	


}
