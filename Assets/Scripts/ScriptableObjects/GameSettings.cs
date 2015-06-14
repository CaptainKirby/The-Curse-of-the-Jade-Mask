using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
public class GameSettings : ScriptableObject {
	public GameObject uiCanvas;

	[Header("Interaction Menu")]
	public InteractionMenu uiInteractioneMenu;
	public GameObject interactiveMenuPrefab;
	public Image pickupIcon;
	public Image observeIcon;
	public Image investigateIcon;
	public Image talkIcon;
	public Image interactIcon;

	[Header("Dialogue")]
	public GameObject observeText;
	public GameObject continueButton;

	[Header("Audio")]
	public GameObject endButton;
	public GameObject musicSource;
//	[Space(10)]
	[Header("MainMenu Stuff")]
	[Tooltip("MM Background")]
	public GameObject mainMenuBg;
	public GameObject mainMenuButton;
	public Color hoverColor;
	public Color mouseDownColor;
	public Color activeColor;
	public Color usedColor;
}
