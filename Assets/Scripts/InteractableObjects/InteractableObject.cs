using UnityEngine;
using System.Collections;
public class InteractableObject : MonoBehaviour {
//	public string name = "Object";
	public enum InteractionType{Pickup, Interact};
	public InteractionType interactionType;
		public virtual void OnMouseDown()
	{
		Debug.Log ("Clicked on: " + this.gameObject.name);
		OpenInteractiveMenu ();
	}
	public virtual void OpenInteractiveMenu()
	{


//		Debug.Log (type);
		GameController.instance.interactionMenu.OpenInteractiveMenu (this.transform.position, interactionType.ToString(), this.gameObject);
	}
}
