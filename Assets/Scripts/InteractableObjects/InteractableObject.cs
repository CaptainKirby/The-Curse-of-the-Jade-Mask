using UnityEngine;
using System.Collections;
public class InteractableObject : MonoBehaviour {
//	public string name = "Object";
	public enum InteractionType{Observe, ObserveZoom, ObserveUse, ObservePickup, ObservePickupZoom, ObserveLeave, ObserveOpen};
	public InteractionType interactionType;
	public string name = "Temp";
//	[HideInInspector]
//	public bool open;
	public GameObject investigateObj; 
	[HideInInspector]
	public GameObject area; 
	private Area areaComponent;

	[HideInInspector]
	public CharacterMovement cMovement;

	public GameObject characterStance = null;
	
	public SpriteRenderer spriteRendererToUpdate;
	public Sprite spriteToUpdateFrom;

	public Sprite spriteToUpdateTo;

	public SpriteRenderer spriteToActivate;

	public GameObject[] objsToEnable;

	public GameObject inventoryObject;
	public GameObject[] neededInventoryObjs;
	public bool disableOnPickup;
	public bool disableOnOpen;

	public AudioClip clipPlay;
	void Start()
	{
		area = this.transform.parent.gameObject;
		areaComponent = area.GetComponent<Area> ();
		cMovement = GameObject.FindObjectOfType<CharacterMovement> ();
	}
	void Update()
	{

		if(Input.GetMouseButtonUp(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hit.collider != null)
			{
				if(hit.collider.gameObject == this.gameObject)
				{
//					Debug.Log ("Clicked on: " + this.gameObject.name);
					if (GameController.instance.currentArea == area) 
					{

						//if game controller current area == this area && if chracter not moving
						if(!cMovement.moving)
						{
							OpenInteractiveMenu ();
						}
						
					}
					else
					{
						if(GameController.instance.playerState == GameController.PlayerState.OpenArea)
						{
							if(!cMovement.moving)
							{
								if(characterStance == null)
								{
									StartCoroutine(cMovement.FadeOut(areaComponent.standPosition, areaComponent.sortingLayerNr, areaComponent.characterStance, this));
								}
								else
								{
									StartCoroutine(cMovement.FadeOut(areaComponent.standPosition, areaComponent.sortingLayerNr, characterStance, this));
									
								}
								GameController.instance.currentArea = area;
							}
						}
						else
						{
							OpenInteractiveMenu ();
						}
					}
				}
			}
		}
	}
	public virtual void OnMouseUp()
	{
//		Debug.Log ("Clicked on: " + this.gameObject.name);
//		if (GameController.instance.currentArea.Equals (area)) 
//		{
//			//if game controller current area == this area && if chracter not moving
//			if(!cMovement.moving)
//				OpenInteractiveMenu ();
//
//		}
//		else
//		{
//			if(!cMovement.moving)
//			{
//				if(characterStance == null)
//				{
//					StartCoroutine(cMovement.FadeOut(areaComponent.standPosition, areaComponent.sortingLayerNr, areaComponent.characterStance));
//				}
//				else
//				{
//					StartCoroutine(cMovement.FadeOut(areaComponent.standPosition, areaComponent.sortingLayerNr, characterStance));
//				
//				}
//				GameController.instance.currentArea = area;
//			}
//		}
		//else run chracter  function to move fra position to area pos

	}

//	public void Update()
//	{
//
//		if(Input.GetMouseButtonDown(0))
//		{
//
//			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//			
//				
//				//				Debug.Log (EventSystem.current.currentSelectedGameObject);
//				if(hit.collider != null)
//				{
//
//					if(hit.collider.GetComponent<InteractableObject>() && !open)
//					{
//					open = true;
//						//CloseInteractiveMenu and open
////					Debug.Log ("GEGNI222");
//
//					OpenInteractiveMenu();
//		
//						
//						//						OpenInteractiveMenu(hit.collider.transform.position, hit.collider.GetComponent<InteractableObject>().interactionType, );
//					}
//
//				}
//
//			}
//			
//	}
	public void CloseInteractiveMenu()
	{
		if (GameController.instance.interactionMenu.open) 
		{
		GameController.instance.interactionMenu.CloseInteractiveMenu ();
		}
	}
	public virtual void OpenInteractiveMenu()
	{

//		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//		Debug.Log (hit.collider.name);
//		Debug.Log ("GEG");

		if (!GameController.instance.interactionMenu.open) 
		{
//			GameController.instance.interactionMenu.OpenInteractiveMenu (this.transform.position, interactionType, this.gameObject, name);
			GameController.instance.interactionMenu.OpenInteractiveMenu (this.transform.position, interactionType, this.gameObject, name);

		}
		else
		{

//			Debug.Log ("gnesug");
			//if hits another interactable object
//			GameController.instance.interactionMenu.CloseInteractiveMenu();
//			GameController.instance.interactionMenu.OpenInteractiveMenu (this.transform.position, interactionType, this.gameObject, name);
			//close?
		}
	}
}

