using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionMenu_Pickup : InteractionMenu {

	public override void OpenInteractiveMenu(Vector3 position, string k, GameObject gObj) 
	{
		base.OpenInteractiveMenu (position, k, gObj);
		ShowPickup ();
	}
	public override void ShowPickup()
	{
		Debug.Log ("PICKUP");
	}
	public override void ShowObserve()
	{

	}




}
