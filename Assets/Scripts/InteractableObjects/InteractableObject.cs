using UnityEngine;
using System.Collections;
public class InteractableObject : MonoBehaviour {
//	public string name = "Object";
	public enum InteractionType{Pickup, Interact, Investigate};
	public InteractionType interactionType;
	public string name = "Temp";
//	[HideInInspector]
//	public bool open;
	public GameObject investigateObj; 

	public virtual void OnMouseDown()
	{
		Debug.Log ("Clicked on: " + this.gameObject.name);
		OpenInteractiveMenu ();
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

