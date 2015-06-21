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
	public GameObject highlight;
	void Start () 
	{
		highlight = Instantiate (GameController.instance.gameSettings.selectImage, Vector3.zero, Quaternion.identity) as GameObject;
		highlight.SetActive (false);
		highlight.transform.SetParent (this.transform);

	}
	
	void Update () 
	{
	
	}

	public void SelectDeselect(GameObject obj)
	{
//		Debug.Log ("NGUENU");
		if(selectedObj == obj)
		{
			selectedObj = null;
			highlight.SetActive(false);
		}
		else
		{
			selectedObj = obj;
			highlight.transform.position = obj.transform.position;
			highlight.SetActive(true);
			highlight.GetComponent<RectTransform>().localScale = GameController.instance.gameSettings.selectImage.GetComponent<RectTransform>().localScale;
			highlight.GetComponent<RectTransform>().pivot = GameController.instance.gameSettings.selectImage.GetComponent<RectTransform>().pivot;
			highlight.GetComponent<RectTransform>().sizeDelta = GameController.instance.gameSettings.selectImage.GetComponent<RectTransform>().sizeDelta;
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
		highlight.SetActive(false);
		selectedObj.SetActive (false);
		selectedObj = null;
	}
}
