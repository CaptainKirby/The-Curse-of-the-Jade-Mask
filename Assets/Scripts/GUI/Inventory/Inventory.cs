using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Inventory : MonoBehaviour {
	public List<string> inventoryObjects = new List<string>();
//	public GameObject addObj;
	public Transform notebookHolder;
	public Transform invHolderSmall;
	public Transform invHolderLarge;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	public void AddToInventory(GameObject obj)
	{
		GameObject g = Instantiate (obj, Vector3.zero, Quaternion.identity) as GameObject;
		GameController.instance.inventoryController.inventoryObjects.Add (obj.ToString());
		g.transform.SetParent (GameController.instance.uiCanvas.transform);

		RectTransform rT = g.GetComponent<RectTransform>();
		
		rT.anchoredPosition = obj.GetComponent<RectTransform>().anchoredPosition;
		rT.localScale = obj.GetComponent<RectTransform>().localScale;
		rT.sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

		//Sort according to position
	}
}
