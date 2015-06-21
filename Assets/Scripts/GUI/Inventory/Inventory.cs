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
	public List<GameObject> objsInSmall;
	public List<GameObject> objsInLarge;

	void Start () 
	{
		highlight = Instantiate (GameController.instance.gameSettings.selectImage, Vector3.zero, Quaternion.identity) as GameObject;
		highlight.SetActive (false);
		highlight.transform.SetParent (this.transform);
		invHolderLarge.gameObject.SetActive (false);
		invHolderSmall.gameObject.SetActive (false);
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
			if(GameController.instance.interactionMenu.openIconActive.IsActive())
			{
				//				Debug.Log ("CHANGE ICON");
				GameController.instance.interactionMenu.ChangeIcon(GameController.instance.gameSettings.keyIcon.GetComponent<Image>().sprite, GameController.instance.interactionMenu.openIconActive.GetComponent<Image>()); 
			}
		}
		else
		{
			if(GameController.instance.interactionMenu.openIconActive.IsActive())
			{
//				Debug.Log ("CHANGE ICON");
				GameController.instance.interactionMenu.ChangeIcon(GameController.instance.gameSettings.keyIcon.GetComponent<Image>().sprite, GameController.instance.interactionMenu.openIconActive.GetComponent<Image>()); 
			}
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
			if(invHolderSmall.gameObject.activeSelf == false)
				invHolderSmall.gameObject.SetActive(true);

			objsInSmall.Add(g);


			g.transform.SetParent (invHolderSmall.transform);
		}
		else
		{
			if(invHolderLarge.gameObject.activeSelf == false)
				invHolderLarge.gameObject.SetActive(true);

			objsInLarge.Add(g);



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
		if(objsInSmall.Contains(selectedObj))
			objsInSmall.Remove(selectedObj);


		if (objsInLarge.Contains (selectedObj))
			objsInLarge.Remove (selectedObj);

		if (objsInLarge.Count < 1 && invHolderLarge.gameObject.activeSelf == true)
			invHolderLarge.gameObject.SetActive (false);

		if (objsInSmall.Count < 1 && invHolderSmall.gameObject.activeSelf == true)
			invHolderSmall.gameObject.SetActive (false);



		highlight.SetActive(false);
		selectedObj.SetActive (false);
		selectedObj = null;

	}
}
