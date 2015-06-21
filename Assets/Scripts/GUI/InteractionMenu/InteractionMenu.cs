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

	[HideInInspector]
	public GameObject backButton;

	public void Update()
	{

		if(Input.GetMouseButtonUp(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null)
			{

			if(open && !EventSystem.current.currentSelectedGameObject)
			{	
//				Debug.Log (EventSystem.current.currentSelectedGameObject);

					Debug.Log ("GNEU");
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
//							hit.collider.GetComponent<InteractableObject>().OpenInteractiveMenu();
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

//					CloseInteractiveMenu();
				}

			}
			else
			{
				CloseInteractiveMenu();
				GameController.instance.dCon.obsTextObj.SetActive(false);
				GameController.instance.dCon.continueButton.SetActive (false);
				GameController.instance.dCon.endButton.SetActive (false);

			}

		}	
//		else if(Input.GetMouseButtonUp(0) && GameController.instance.inventoryController.selectedObj != null)
//		{
//			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//			if(open && !EventSystem.current.currentSelectedGameObject)
//			{	
//				//				Debug.Log (EventSystem.current.currentSelectedGameObject);
//				if(hit.collider != null)
//				{
//					Debug.Log (hit.collider);
//					GameController.instance.dCon.obsTextObj.SetActive(false);
//					GameController.instance.dCon.continueButton.SetActive (false);
//					GameController.instance.dCon.endButton.SetActive (false);
//					if(hit.collider.GetComponent<InteractableObject>())
//					{
//						if(hit.collider.GetComponent<InteractableObject>().clickInventoryItemNeeded != null)
//						{
//							if(hit.collider.GetComponent<InteractableObject>().clickInventoryItemNeeded.ToString() == GameController.instance.inventoryController.selectedObj.ToString())
//							{
//
//								OpenStuff(hit.collider.GetComponent<InteractableObject>(), hit.collider.gameObject);
//								CloseInteractiveMenu();
//								GameController.instance.inventoryController.RemoveFromInventory();
//							}
//							else
//							{
//								CloseInteractiveMenu();
//								Dialoguer.StartDialogue(DialoguerDialogues.Cant); 
//								GameController.instance.dCon.obsTextObj.SetActive(true);
//							}
//						}
//					}
//
//				}
//
//			}
//		}
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
		currentInteractiveMenu.gameObject.GetComponent<Animator> ().Play ("MenuPopup");
		GameController.instance.audioClipSource.GetComponent<AudioSource> ().clip = GameController.instance.gameSettings.clickSound3;
		GameController.instance.audioClipSource.GetComponent<AudioSource> ().pitch = Random.Range (0.85f, 1.15f);
		GameController.instance.audioClipSource.GetComponent<AudioSource> ().Play ();
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


		InteractableObject iO = gObj.GetComponent<InteractableObject>();
		if(img == observeIconActive)
		{
			PlaySound(GameController.instance.gameSettings.clickSound1);


			CCircle(img);
//			if(DialoguerDialogues
//			if(

			DialoguerDialogues diag = DialoguerDialogues.None;
			try{
				diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Observe" );
			}
			catch
			{
				Debug.Log ("Dialogue needs to be set!");
			}
			
			if(diag != DialoguerDialogues.None)
			{
				Dialoguer.StartDialogue(diag); 
				GameController.instance.dCon.obsTextObj.SetActive(true);
				
				
			}
//			}
		}
		if (img == pickupIconActive) 
		{
			PlaySound(GameController.instance.gameSettings.clickSound2);


			CCircle(img);
			if(gObj.GetComponent<InteractableObject>().inventoryObject != null)
			{
//				Debug.Log(gObj.GetComponent<InteractableObject>().investigateObj);

//				GameController.instance.inventoryController.addObj = gObj.GetComponent<InteractableObject>().investigateObj;
				GameController.instance.inventoryController.AddToInventory(gObj.GetComponent<InteractableObject>().inventoryObject, gObj.GetComponent<InteractableObject>().smallObj);
			}

//			if(System.Enum.IsDefined(typeof(DialoguerDialogues), (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Pickup" )))
//			{
//
//			}
//			Debug.Log ();

			DialoguerDialogues diag = DialoguerDialogues.None;
			try{
				diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Pickup" );
			}
			catch
			{
				Debug.Log ("Dialogue needs to be set!");
			}

			if(diag != DialoguerDialogues.None)
			{
				Dialoguer.StartDialogue(diag); 
				GameController.instance.dCon.obsTextObj.SetActive(true);

				
			}


			if(gObj.GetComponent<InteractableObject>().disableOnPickup)
			{
				gObj.SetActive(false);
			}

//			Debug.Log("GBNEU");
			gObj.SetActive(false);
			CloseInteractiveMenu();
			open = true;


		}
		if (img == talkIconActive) 
		{
//			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Talk" );

		}
		if (img == zoomIconActive) 
		{
			PlaySound(GameController.instance.gameSettings.clickSound1);


			if(gObj.GetComponent<InteractableObject>().investigateObj != null)
			{
				GameController.instance.playerState = GameController.PlayerState.Zoom;
				CloseInteractiveMenu();
				gObj.GetComponent<InteractableObject>().investigateObj.SetActive(true);

				if(GameController.instance.gameSettings.zoomIcon != null)
				{
					if(backButton == null)
					{
						backButton = Instantiate(GameController.instance.gameSettings.zoomBackButton, Vector3.zero, Quaternion.identity) as GameObject;
						backButton.transform.SetParent(GameController.instance.uiCanvas.transform);
						RectTransform rT = backButton.GetComponent<RectTransform>();

						rT.anchoredPosition = GameController.instance.gameSettings.zoomBackButton.GetComponent<RectTransform>().anchoredPosition;
						rT.localScale = GameController.instance.gameSettings.zoomBackButton.GetComponent<RectTransform>().localScale;
						rT.sizeDelta = GameController.instance.gameSettings.zoomBackButton.GetComponent<RectTransform>().sizeDelta;
					}
					else
					{
						backButton.SetActive(true);
					}

					backButton.GetComponent<Button>().onClick.AddListener(() => GoBackFromZoom(gObj.GetComponent<InteractableObject>()));
				}
			}
//			DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Investigate" );

		}
		if (img == useIconActive) 
		{
			PlaySound(GameController.instance.gameSettings.clickSound1);
			DialoguerDialogues diag = DialoguerDialogues.None;
			try{
				diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Use" );
			}
			catch
			{
				Debug.Log ("Dialogue needs to be set!");
			}
			
			if(diag != DialoguerDialogues.None)
			{
				Dialoguer.StartDialogue(diag); 
				GameController.instance.dCon.obsTextObj.SetActive(true);
				
				
			}
		}
		if(img == openIconActive)
		{


			PlaySound(GameController.instance.gameSettings.clickSound1);


			CloseInteractiveMenu();
			CCircle(img);
			if(iO.clickInventoryItemNeeded == null)
			{
//				Debug.Log ("BGNEU");
				OpenStuff(iO, gObj);
//				if(iO.spriteToActivate != null)
//				{
//					iO.spriteToActivate.gameObject.SetActive(true);
//				}
//				if(iO.spriteRendererToUpdate != null)
//				{
//					SpriteRenderer r = iO.spriteRendererToUpdate;
//					if(r.sprite == iO.spriteToUpdateFrom)
//						r.sprite = iO.spriteToUpdateTo;
//					else
//						r.sprite = iO.spriteToUpdateFrom;
//				}
//				if(iO.objsToEnable.Length > 0)
//				{
//					for(int i = 0; i < iO.objsToEnable.Length; i++)
//				    {
//						iO.objsToEnable[i].SetActive(true);
//					}
//
//				}
//				if(gObj.GetComponent<InteractableObject>().disableOnOpen)
//				{
//					gObj.SetActive(false);
//				}
//
//				if(iO.clipPlay != null)
//				{
//					GameController.instance.audioClipSource.GetComponent<AudioSource>().clip = iO.clipPlay;
//					GameController.instance.audioClipSource.GetComponent<AudioSource>().Play();
//				}
			}
			else
			{
				DialoguerDialogues diag = DialoguerDialogues.None;
				try{
					diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Open" );
				}
				catch
				{
					Debug.Log ("Dialogue needs to be set!");
				}
				
				if(diag != DialoguerDialogues.None)
				{
					Dialoguer.StartDialogue(diag); 
					GameController.instance.dCon.obsTextObj.SetActive(true);
					
					
				}
			}

		}
		if(img == leaveIconActive)
		{
			PlaySound(GameController.instance.gameSettings.clickSound1);

			CCircle(img);
			int itemsNeeded = 0;
			if(gObj.GetComponent<InteractableObject>().neededInventoryObjs.Length > 0)
			{
				foreach(GameObject g in gObj.GetComponent<InteractableObject>().neededInventoryObjs)
				{
					if(GameController.instance.inventoryController.inventoryObjects.Contains(g.ToString()))
					{
						itemsNeeded++;
					}
				}
				if(gObj.GetComponent<InteractableObject>().neededInventoryObjs.Length == itemsNeeded)
				{
					DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Leave_True" );
					Dialoguer.StartDialogue(diag); 
					GameController.instance.dCon.obsTextObj.SetActive(true);
				}

				else
				{
					DialoguerDialogues diag = (DialoguerDialogues) System.Enum.Parse( typeof( DialoguerDialogues ), gObj.name + "_Leave_False" );
					Dialoguer.StartDialogue(diag); 
					GameController.instance.dCon.obsTextObj.SetActive(true);
				}
			}
		}
	}
	public void GoBackFromZoom(InteractableObject intObj)
	{
		intObj.investigateObj.SetActive (false);
		CloseInteractiveMenu ();
		backButton.SetActive (false);
		GameController.instance.playerState = GameController.PlayerState.OpenArea;
		GameController.instance.dCon.obsTextObj.SetActive(false);
		GameController.instance.dCon.continueButton.SetActive (false);
		GameController.instance.dCon.endButton.SetActive (false);
	}
	
	void CCircle(Image img)
	{
		if(GameController.instance.gameSettings.clickCircle != null)
		{
			GameObject c = Instantiate(GameController.instance.gameSettings.clickCircle, Vector3.zero, Quaternion.identity) as GameObject;
			c.transform.SetParent(GameController.instance.uiCanvas.transform);
			c.transform.position = img.transform.position;
//			Dialoguer.EndDialogue();
			Destroy (c, 1);
		}
	}

	void PlaySound(AudioClip c)
	{
		GameController.instance.audioClipSource.GetComponent<AudioSource>().clip = c;
		GameController.instance.audioClipSource.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
		GameController.instance.audioClipSource.GetComponent<AudioSource>().Play ();
	}

	public void OpenStuff(InteractableObject iO, GameObject gObj)
	{

		//open stuff
		PlaySound(GameController.instance.gameSettings.clickSound1);
		
		
		CloseInteractiveMenu();
//		

			if(iO.spriteToActivate != null)
			{
				iO.spriteToActivate.gameObject.SetActive(true);
			}
			if(iO.spriteRendererToUpdate != null)
			{
				SpriteRenderer r = iO.spriteRendererToUpdate;
				if(r.sprite == iO.spriteToUpdateFrom)
					r.sprite = iO.spriteToUpdateTo;
				else
					r.sprite = iO.spriteToUpdateFrom;
			}
			if(iO.objsToEnable.Length > 0)
			{
				for(int i = 0; i < iO.objsToEnable.Length; i++)
				{
					iO.objsToEnable[i].SetActive(true);
				}
				
			}
			if(gObj.GetComponent<InteractableObject>().disableOnOpen)
			{
				gObj.SetActive(false);
			}
			
			if(iO.clipPlay != null)
			{
				GameController.instance.audioClipSource.GetComponent<AudioSource>().clip = iO.clipPlay;
				GameController.instance.audioClipSource.GetComponent<AudioSource>().Play();
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
