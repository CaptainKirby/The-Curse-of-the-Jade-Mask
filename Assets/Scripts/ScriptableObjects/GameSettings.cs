using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
public class GameSettings : ScriptableObject {
	public GameObject uiCanvas;
	public InteractionMenu uiInteractioneMenu;
	public GameObject interactiveMenuPrefab;
	public Image pickupIcon;
	public Image observeIcon;
	public Image investigateIcon;
	public Image talkIcon;
	public Image interactIcon;
	public GameObject observeText;
	public GameObject continueButton;
	public GameObject endButton;

}
