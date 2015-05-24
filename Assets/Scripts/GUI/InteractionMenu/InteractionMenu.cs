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
	private Image observeIconActive;
	private Image investigateIconActive;
	private Image talkIconActive;
	[HideInInspector]
	public bool open = false;

	public float screenRatioWidth;
	public float screenRatioHeight;

	public void Update()
	{
//		Debug.Log (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
//		PointerEventData pe = new PointerEventData(EventSystem.current);
//		pe.position =  Input.mousePosition;
		
//	
				if(Input.GetMouseButtonDown(0))
				{
		
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
	
			if(open && !EventSystem.current.currentSelectedGameObject)
			{	
				if(hit.collider != null)
				{
					if(hit.collider.GetComponent<InteractableObject>())
					{
						//CloseInteractiveMenu and open
//						CloseInteractiveMenu();
//						hit.collider.GetComponent<InteractableObject>().OpenInteractiveMenu();
					}
					else
					{
						CloseInteractiveMenu();

					}
				}
				else
				{
					CloseInteractiveMenu();
				}
			}
		}	
	}
	public void Start()
	{
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
		open = true;
		currentInteractiveMenu.SetActive (true);

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
		}
		if(type.Equals(InteractableObject.InteractionType.Interact))
		{
			ShowInvestigate (0,gObj);
			ShowObserve(1,gObj);
		}

	}
	public void CloseInteractiveMenu()
	{
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
		if (pickupIconActive == null) 
		{
			//make circle visible
			circles [pos].gameObject.SetActive (true);
			//activate icon Set to seperate function?
			pickupIconActive = ActivateIcon (GameController.instance.gameSettings.pickupIcon, circles [pos].transform.position, gObj, true);
		}
		else 
		{
			circles [pos].gameObject.SetActive (true);

		}
		//
	}
	public void ShowInvestigate(int pos, GameObject gObj)
	{
		if (investigateIconActive == null) 
		{
			circles [pos].gameObject.SetActive (true);
			investigateIconActive = ActivateIcon (GameController.instance.gameSettings.investigateIcon, circles [pos].transform.position, gObj, true);
		}
		else 
		{
			circles [pos].gameObject.SetActive (true);
			
		}
	}

	public void ShowObserve(int pos, GameObject gObj)
	{
		if(observeIconActive == null)
		{
			circles [pos].gameObject.SetActive (true);
			observeIconActive = ActivateIcon (GameController.instance.gameSettings.observeIcon, circles [pos].transform.position, gObj, true);
		}
		else 
		{

			circles [pos].gameObject.SetActive (true);
			observeIconActive = ActivateIcon (observeIconActive, circles [pos].transform.position, gObj, false);

		}

	}

	public void ShowTalk(int pos, GameObject gObj)
	{
		if(talkIconActive == null)
		{
			circles [pos].gameObject.SetActive (true);
			talkIconActive = ActivateIcon (GameController.instance.gameSettings.talkIcon, circles [pos].transform.position, gObj, true);
		}
		else 
		{
			circles [pos].gameObject.SetActive (true);
			
		}
	}

	public Image ActivateIcon(Image icon, Vector3 pos, GameObject gObj, bool init)
	{
//		Debug.Log (gObj);

		Image newIcon = null;
		if(init)
		{
			newIcon = Instantiate (icon, pos, Quaternion.identity) as Image;
			newIcon.transform.SetParent (currentInteractiveMenu.transform);
		}
		else
		{
			newIcon = icon;
		}
//		newIcon.GetComponent<RectTransform> ().sizeDelta = GameController.instance.gameSettings.talkIcon.GetComponent<RectTransform>().sizeDelta;
		newIcon.GetComponent<Button> ().onClick.AddListener(() => this.Clicked(newIcon, gObj));
		return newIcon;
	}

	public void Clicked(Image img, GameObject gObj)
	{

//		Debug.Log (gObj + "lala");
		if(img == observeIconActive)
		{
			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Observe" );
			Dialoguer.StartDialogue(diag); 
			GameController.instance.dCon.obsText.SetActive(true);
		}
		if (img == pickupIconActive) 
		{
			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Pickup" );

		}
		if (img == talkIconActive) 
		{
			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Talk" );

		}
		if (img == investigateIconActive) 
		{
			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Investigate" );

		}
	}

}
