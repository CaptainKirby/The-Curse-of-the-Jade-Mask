using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
	public List<string> inventoryObjects = new List<string>();
//	public GameObject addObj;
	public Transform notebookHolder;
	public Transform invHolderSmall;
	public Transform invHolderLarge;

	public GameObject selectedObj;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	public void SelectDeselect(GameObject obj)
	{
		Debug.Log ("NGUENU");
		if(selectedObj == obj)
		{
			selectedObj = null;
		}
		else
		{
			selectedObj = obj;
		}
	}
	public void AddToInventory(GameObject obj, bool small)
	{
		GameObject g = Instantiate (obj, Vector3.zero, Quaternion.identity) as GameObject;
		GameController.instance.inventoryController.inventoryObjects.Add (obj.ToString());
//		selectedObj = obj;
		g.name = obj.name;
		g.GetComponent<Button>().onClick.AddListener(() => SelectDeselect(g));
		if(small)
		{
			g.transform.SetParent (invHolderSmall.transform);
		}
		else
		{
			
			g.transform.SetParent (invHolderLarge.transform);
		}
//		g.transform.SetParent (GameController.instance.uiCanvas.transform);

		RectTransform rT = g.GetComponent<RectTransform>();
//		
		rT.anchoredPosition = obj.GetComponent<RectTransform>().anchoredPosition;
		rT.localScale = obj.GetComponent<RectTransform>().localScale;
		rT.sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

		//Sort according to position
	}

	public void RemoveFromInventory()
	{

		selectedObj.SetActive (false);
		selectedObj = null;
	}
}
