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

	private Image pickupIconActive;
	public Image observeIconActive;
	private Image investigateIconActive;
	private Image talkIconActive;
	private Image interactIconActive;

	[HideInInspector]
	public bool open = false;

	public float screenRatioWidth;
	public float screenRatioHeight;

	public GameObject currectActiveObject;

	[HideInInspector]
	public CharacterMovement cMovement;

	public void Update()
	{
//		Debug.Log (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
//		PointerEventData pe = new PointerEventData(EventSystem.current);
//		pe.position =  Input.mousePosition;
		
//	
		if(Input.GetMouseButtonDown(0))
		{
//			Debug.Log ("GEGNI");

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
	
			if(open && !EventSystem.current.currentSelectedGameObject)
			{	
//				Debug.Log (EventSystem.current.currentSelectedGameObject);
				if(hit.collider != null)
				{
					GameController.instance.dCon.obsText.SetActive(false);
					GameController.instance.dCon.continueButton.SetActive (false);
					if(hit.collider.GetComponent<InteractableObject>())
					{
						//CloseInteractiveMenu and open
						if(currectActiveObject != hit.collider.gameObject)
						{
							if (GameController.instance.currentArea.Equals (currectActiveObject.GetComponent<InteractableObject>().area)) 
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
					GameController.instance.dCon.obsText.SetActive(false);
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
		CloseIcon(investigateIconActive);
		CloseIcon(observeIconActive);
		CloseIcon(pickupIconActive);
		//set position on canvas
		currentInteractiveMenu.transform.position = WorldToGuiPoint (position);

		//get the circles on the current menu
		circles = currentInteractiveMenu.GetComponent<InteractiveMenuConfig> ().circles;
		foreach(Image i in circles)
		{
			i.gameObject.SetActive(false);
		}
		if(type == InteractableObject.InteractionType.Pickup)
		{
			numberOfMenuPoints = 2;
			ShowPickup (0,gObj);
			ShowObserve(1, gObj);
//			ShowIcon (0,gObj, pickupIconActive, GameController.instance.gameSettings.pickupIcon);
//			ShowIcon (1,gObj, observeIconActive, GameController.instance.gameSettings.observeIcon);

			
			
		}
		if(type.Equals(InteractableObject.InteractionType.Interact))
		{

//			ShowIcon (0,gObj, investigateIconActive, GameController.instance.gameSettings.investigateIcon);
			ShowObserve(1,gObj);
			ShowInteract(0,gObj);
//			ShowIcon (1,gObj, observeIconActive, GameController.instance.gameSettings.observeIcon);
//			Debug.Log ("VNEU");
			
		}
		if(type.Equals(InteractableObject.InteractionType.Investigate))
		{
			ShowObserve(1,gObj);
			ShowInvestigate(0,gObj);
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

//	public void ShowIcon(int pos, GameObject gObj, Image icon, Image iconRef)
//	{
////		Debug.Log (iconRef);
//		if (icon == null) 
//		{
//			circles [pos].gameObject.SetActive (true);
//			icon = ActivateIcon (iconRef, circles [pos].transform.position, gObj, true);
////			Debug.Log (icon);
//		}
//
//	}
	public void ShowPickup(int pos, GameObject gObj)
	{
//		if (pickupIconActive == null) 
//		{
			//make circle visible
			circles [pos].gameObject.SetActive (true);
			//activate icon Set to seperate function?
			pickupIconActive = ActivateIcon (GameController.instance.gameSettings.pickupIcon, circles [pos].transform.position, gObj, true);
//		}
//		else 
//		{
//			circles [pos].gameObject.SetActive (true);
//			pickupIconActive = ActivateIcon (pickupIconActive, circles [pos].transform.position, gObj,false);
//		}
		//
	}
	public void ShowInvestigate(int pos, GameObject gObj)
	{
//		if (investigateIconActive == null) 
//		{
			circles [pos].gameObject.SetActive (true);
			investigateIconActive = ActivateIcon (GameController.instance.gameSettings.investigateIcon, circles [pos].transform.position, gObj, true);
//		}
//		else 
//		{
//			circles [pos].gameObject.SetActive (true);
//			investigateIconActive = ActivateIcon (investigateIconActive, circles [pos].transform.position, gObj, false);
//		}
	}

	public void ShowObserve(int pos, GameObject gObj)
	{
//		if(observeIconActive == null)
//		{
//		Debug.Log ("veugs");
			circles [pos].gameObject.SetActive (true);
			observeIconActive = ActivateIcon (GameController.instance.gameSettings.observeIcon, circles [pos].transform.position, gObj, true);
//		}
//		else 
//		{
//
//			circles [pos].gameObject.SetActive (true);
//			observeIconActive = ActivateIcon (observeIconActive, circles [pos].transform.position, gObj, false);
//
//		}

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

	public void ShowInteract(int pos, GameObject gObj)
	{
		//		if(talkIconActive == null)
		//		{
		circles [pos].gameObject.SetActive (true);
		interactIconActive = ActivateIcon (GameController.instance.gameSettings.interactIcon, circles [pos].transform.position, gObj, true);
		//		}
		//		else 
		//		{
		//			circles [pos].gameObject.SetActive (true);
		//			
		//		}
	}
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
				GameController.instance.dCon.obsText.SetActive(true);
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
		if (img == investigateIconActive) 
		{
			if(gObj.GetComponent<InteractableObject>().investigateObj != null)
			{
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
