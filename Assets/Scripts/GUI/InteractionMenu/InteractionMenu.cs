using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class InteractionMenu : MonoBehaviour
{
//	public GameObject menuBackground;
	[HideInInspector]
	public GameObject currentInteractiveMenu;
	private int numberOfMenuPoints;

	private Image[] circles = new Image[3];
	private string name;
	private Text nameText;
	private Image pickupIconActive;
	public Image observeIconActive;
	private Image zoomIconActive;
	private Image talkIconActive;
	private Image openIconActive;
	private Image leaveIconActive;
	private Image useIconActive;

	[HideInInspector]
	public bool open = false;

	public float screenRatioWidth;
	public float screenRatioHeight;

	public GameObject currectActiveObject;

	[HideInInspector]
	public CharacterMovement cMovement;

	public bool isTouching;
	public bool touchUp;
	public bool release;


	public void Update()
	{
//		Debug.Log (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
//		PointerEventData pe = new PointerEventData(EventSystem.current);
//		pe.position =  Input.mousePosition;

		//		CheckUp ();
		if(Input.GetMouseButtonUp(0))
		{


			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(open && !EventSystem.current.currentSelectedGameObject)
			{	
//				Debug.Log (EventSystem.current.currentSelectedGameObject);
				if(hit.collider != null)
				{
					Debug.Log (hit.collider);
					GameController.instance.dCon.obsTextObj.SetActive(false);
					GameController.instance.dCon.continueButton.SetActive (false);
					GameController.instance.dCon.endButton.SetActive (false);
					if(hit.collider.GetComponent<InteractableObject>())
					{

						//CloseInteractiveMenu and open
						if(currectActiveObject != hit.collider.gameObject)
						{
							if (GameController.instance.currentArea == currectActiveObject.GetComponent<InteractableObject>().area) 
							{


//							Debug.Log (currectActiveObject)
//							Debug.Log ("GENUG");
							CloseInteractiveMenu();
							hit.collider.GetComponent<InteractableObject>().OpenInteractiveMenu();
							}
							else
							{
								CloseInteractiveMenu();
							}

						}
					}
					else
					{

						CloseInteractiveMenu();

					}
				}
				else
				{
					GameController.instance.dCon.obsTextObj.SetActive(false);
					GameController.instance.dCon.continueButton.SetActive (false);
					CloseInteractiveMenu();
				}
			}
		}	
	}
	public void Start()
	{
		cMovement = GameObject.FindObjectOfType<CharacterMovement> ();
		if(GameController.instance.gameSettings.interactiveMenuPrefab != null && !open)
		{
			currentInteractiveMenu = Instantiate(GameController.instance.gameSettings.interactiveMenuPrefab, Vector3.zero, GameController.instance.gameSettings.interactiveMenuPrefab.transform.rotation) as GameObject;
			currentInteractiveMenu.transform.SetParent(GameController.instance.uiCanvas.transform);
			currentInteractiveMenu.transform.SetAsFirstSibling();
			currentInteractiveMenu.SetActive(false);
//			currentInteractiveMenu.GetComponent<RectTransform>().sizeDelta = currentInteractiveMenu.GetComponent<RectTransform>().sizeDelta * 2;
//			screenRatioWidth = Screen.width / 2048;
//			screenRatioHeight = Screen.height/1536;
			RectTransform prefab = GameController.instance.gameSettings.interactiveMenuPrefab.GetComponent<RectTransform>();
//			RectTransform newRectTransform = currentInteractiveMenu.transform as RectTransform;
			currentInteractiveMenu.GetComponent<RectTransform>().anchoredPosition = prefab.anchoredPosition;
			currentInteractiveMenu.GetComponent<RectTransform>().anchorMax = prefab.anchorMax;
			currentInteractiveMenu.GetComponent<RectTransform>().anchorMin = prefab.anchorMin;
			currentInteractiveMenu.GetComponent<RectTransform>().localRotation = prefab.localRotation;
			currentInteractiveMenu.GetComponent<RectTransform>().localScale = prefab.localScale;
			currentInteractiveMenu.GetComponent<RectTransform>().pivot = prefab.pivot;
			currentInteractiveMenu.GetComponent<RectTransform>().sizeDelta = prefab.sizeDelta;

		}
	}
	
	public virtual void OpenInteractiveMenu(Vector3 position, InteractableObject.InteractionType type, GameObject gObj, string name) 
	{
//		CloseInteractiveMenu ();
		currectActiveObject = gObj;
		open = true;
		currentInteractiveMenu.SetActive (true);
		CloseIcon(talkIconActive);
		CloseIcon(zoomIconActive);
		CloseIcon(observeIconActive);
		CloseIcon(pickupIconActive);
		CloseIcon (openIconActive);
		CloseIcon (leaveIconActive);
		CloseIcon (useIconActive);
		//set position on canvas
		currentInteractiveMenu.transform.position = WorldToGuiPoint (position);

		// get name and text component
		nameText = currentInteractiveMenu.GetComponent<InteractiveMenuConfig> ().nameText;
		name = gObj.GetComponent<InteractableObject> ().name;
//		Debug.Log (name);
		nameText.text = name.ToUpper();

		//get the circles on the current menu
		circles = currentInteractiveMenu.GetComponent<InteractiveMenuConfig> ().circles;
		foreach(Image i in circles)
		{
			i.gameObject.SetActive(false);
		}
		if(type == InteractableObject.InteractionType.Observe)
		{
			numberOfMenuPoints = 2;
//			ShowPickup (0,gObj);
			ShowObserve(1, gObj);	
			
		}
		if(type == InteractableObject.InteractionType.ObserveZoom)
		{
			ShowObserve(1,gObj);
			ShowZoom(0,gObj);
			
		}
		if(type == InteractableObject.InteractionType.ObserveUse)
		{
			ShowObserve(1,gObj);
			ShowUse(0,gObj);
		}
		if(type == InteractableObject.InteractionType.ObservePickup)
		{
			ShowObserve(1,gObj);
			ShowPickup(0,gObj);
		}
		if(type == InteractableObject.InteractionType.ObservePickupZoom)
		{
			ShowObserve(1,gObj);
			ShowZoom(0,gObj);
			ShowPickup(2,gObj);
		}
		if(type == InteractableObject.InteractionType.ObserveLeave)
		{
			ShowObserve(1,gObj);
			ShowLeave(0,gObj);
		}
		if(type == InteractableObject.InteractionType.ObserveOpen)
		{
			ShowObserve(1,gObj);
			ShowOpen(0,gObj);
		}



	}

	void CloseIcon(Image icon)
	{
		if(icon != null)
		{
//			icon.gameObject.SetActive(false);
			Destroy (icon.gameObject);
			icon = null;
		}
	}
	public void CloseInteractiveMenu()
	{

		//		Destroy (pickupIconActive);
//		Destroy (investigateIconActive);
		currentInteractiveMenu.SetActive (false);
		open = false;
	}
	public Vector3 WorldToGuiPoint(Vector3 position)
	{
		Vector3 guiPosition = Camera.main.WorldToScreenPoint(position);
//		guiPosition.y = Screen.height - guiPosition.y;
		
		return guiPosition;
	}


	public void ShowPickup(int pos, GameObject gObj)
	{

			circles [pos].gameObject.SetActive (true);
			//activate icon Set to seperate function?
			pickupIconActive = ActivateIcon (GameController.instance.gameSettings.pickupIcon, circles [pos].transform.position, gObj, true);

	}
	public void ShowZoom(int pos, GameObject gObj)
	{
			circles [pos].gameObject.SetActive (true);
			zoomIconActive = ActivateIcon (GameController.instance.gameSettings.zoomIcon, circles [pos].transform.position, gObj, true);
	}

	public void ShowObserve(int pos, GameObject gObj)
	{
			circles [pos].gameObject.SetActive (true);
			observeIconActive = ActivateIcon (GameController.instance.gameSettings.observeIcon, circles [pos].transform.position, gObj, true);
	}
	public void ShowUse(int pos, GameObject gObj)
	{
		circles [pos].gameObject.SetActive (true);
		useIconActive = ActivateIcon (GameController.instance.gameSettings.useIcon, circles [pos].transform.position, gObj, true);
	}	

	public void ShowLeave(int pos, GameObject gObj)
	{
		circles [pos].gameObject.SetActive (true);
		leaveIconActive = ActivateIcon (GameController.instance.gameSettings.leaveIcon, circles [pos].transform.position, gObj, true);
	}

	public void ShowOpen(int pos, GameObject gObj)
	{
		circles [pos].gameObject.SetActive (true);
		openIconActive = ActivateIcon (GameController.instance.gameSettings.openIcon, circles [pos].transform.position, gObj, true);
	}
	public void ShowTalk(int pos, GameObject gObj)
	{
//		if(talkIconActive == null)
//		{
			circles [pos].gameObject.SetActive (true);
			talkIconActive = ActivateIcon (GameController.instance.gameSettings.talkIcon, circles [pos].transform.position, gObj, true);
//		}
//		else 
//		{
//			circles [pos].gameObject.SetActive (true);
//			
//		}
	}

//	public void ShowZoom(int pos, GameObject gObj)
//	{
//		//		if(talkIconActive == null)
//		//		{
//		circles [pos].gameObject.SetActive (true);
//		interactIconActive = ActivateIcon (GameController.instance.gameSettings.interactIcon, circles [pos].transform.position, gObj, true);
//		//		}
//		//		else 
//		//		{
//		//			circles [pos].gameObject.SetActive (true);
//		//			
//		//		}
//	}
	public Image ActivateIcon(Image icon, Vector3 pos, GameObject gObj, bool init)
	{
//		Debug.Log (gObj + " wut");

		Image newIcon = null;
//		if(init)
//		{
			newIcon = Instantiate (icon, pos, Quaternion.identity) as Image;
			newIcon.transform.SetParent (currentInteractiveMenu.transform);
//		}
//		else
//		{
//			newIcon = icon;
//		}
//		newIcon.GetComponent<Button> ().onClick.RemoveListener (() => this.Clicked (newIcon, gObj));
//		newIcon.GetComponent<Button> ().onClick.RemoveAllListeners();

//		newIcon.gameObject.SetActive (true);
//		StartCoroutine(TouchDown(newIcon, gObj));
//		newIcon.GetComponent<RectTransform> ().sizeDelta = GameController.instance.gameSettings.talkIcon.GetComponent<RectTransform>().sizeDelta;
//		Debug.Log (newIcon.name);
		newIcon.GetComponent<Button> ().onClick.AddListener(() => this.Clicked(newIcon, gObj));
		return newIcon;
	}

	public void Clicked(Image img, GameObject gObj)
	{

//		Debug.Log (gObj + "lala");
//		Debug.Log ("GEGNI1");

		if(img == observeIconActive)
		{
//			if(DialoguerDialogues
//			if(
			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Observe" );
//			Debug.Log (diag.ToString());
//			if(diag.ToString() != gObj.name)
//			{
				Dialoguer.StartDialogue(diag); 
				GameController.instance.dCon.obsTextObj.SetActive(true);
//			}
		}
		if (img == pickupIconActive) 
		{
//			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Pickup" );

		}
		if (img == talkIconActive) 
		{
//			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Talk" );

		}
		if (img == zoomIconActive) 
		{
			if(gObj.GetComponent<InteractableObject>().investigateObj != null)
			{
				GameController.instance.playerState = GameController.PlayerState.Zoom;
				CloseInteractiveMenu();
				gObj.GetComponent<InteractableObject>().investigateObj.SetActive(true);
			}
//			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Investigate" );

		}
	}

//	IEnumerator TouchDown(Image newIcon, GameObject gObj)
//	{
//		yield return new WaitForSeconds (0.1f);
//		newIcon.GetComponent<Button> ().onClick.AddListener(() => this.Clicked(newIcon, gObj));

//		while(true)
//		{
//			yield return null;
//			if(Input.GetMouseButtonUp(0))
//			{
//				
////				Debug.Log ("EWOGEIG");
//				yield break;
//			}
//		}
//	}


}
