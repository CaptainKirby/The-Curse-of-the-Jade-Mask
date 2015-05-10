using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour {
	public GameObject menuBackground;
	[HideInInspector]
	public GameObject currentInteractiveMenu;

//	public InteractableObject 
	private int numberOfMenuPoints;
	public void Start()
	{
		if(menuBackground != null)
		{
			currentInteractiveMenu = Instantiate(menuBackground, Vector3.zero, menuBackground.transform.rotation) as GameObject;
			currentInteractiveMenu.transform.SetParent(GameController.instance.uiCanvas.transform);
			currentInteractiveMenu.SetActive(false);
		}
	}

	public virtual void OpenInteractiveMenu(Vector3 position, string k, GameObject gObj) 
	{
		currentInteractiveMenu.SetActive (true);
		currentInteractiveMenu.transform.position = WorldToGuiPoint (position);
//		InteractableObject type = obj.GetComponent<InteractableObject> ();
		Debug.Log (k);
		ShowPickup ();

	}
	public Vector3 WorldToGuiPoint(Vector3 position)
	{
		Vector3 guiPosition = Camera.main.WorldToScreenPoint(position);
//		guiPosition.y = Screen.height - guiPosition.y;
		
		return guiPosition;
	}

	public virtual void ShowPickup()
	{

	}
	public virtual void ShowInvestigate()
	{
		
	}

	public virtual void ShowObserve()
	{
		
	}

	public virtual void ShowTalk()
	{
		
	}


}
