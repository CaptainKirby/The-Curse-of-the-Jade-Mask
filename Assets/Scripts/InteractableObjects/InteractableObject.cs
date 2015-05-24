using UnityEngine;
using System.Collections;
public class InteractableObject : MonoBehaviour {
//	public string name = "Object";
	public enum InteractionType{Pickup, Interact};
	public InteractionType interactionType;
	public string name = "Temp";
		public virtual void OnMouseDown()
	{
		Debug.Log ("Clicked on: " + this.gameObject.name);
		OpenInteractiveMenu ();
	}
	public virtual void OpenInteractiveMenu()
	{


//		Debug.Log (type);
		if (!GameController.instance.interactionMenu.open) 
		{
			GameController.instance.interactionMenu.OpenInteractiveMenu (this.transform.position, interactionType, this.gameObject, name);
		}
		else
		{
			GameController.instance.interactionMenu.CloseInteractiveMenu();
			GameController.instance.interactionMenu.OpenInteractiveMenu (this.transform.position, interactionType, this.gameObject, name);
			//close?
		}
	}
}

