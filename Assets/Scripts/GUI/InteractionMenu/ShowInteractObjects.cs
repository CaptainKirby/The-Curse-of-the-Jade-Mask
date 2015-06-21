using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShowInteractObjects : MonoBehaviour {

	// Use this for initialization
	private bool down;
	public float waitBeforeShow;

	public List<InteractableObject> intObjects;
	public List<GameObject> intObjGraphics;
	public GameObject graphic;
	private float cd;
	private bool clickCircleShow;
	void Start () 
	{
		foreach(InteractableObject iO in GameObject.FindObjectsOfType<InteractableObject> ())
		{
			intObjects.Add(iO);
//			Debug.Log ("GBNUE");
			intObjGraphics.Add(Instantiate(graphic, iO.transform.position, Quaternion.identity) as GameObject);

		}
		for(int i = 0; i< intObjGraphics.Count; i++)
		{
			RectTransform prefab = graphic.GetComponent<RectTransform>();
			RectTransform i2 = intObjGraphics[i].GetComponent<RectTransform>();
			i2.transform.SetParent(GameController.instance.uiCanvas.transform);
			i2.GetComponent<RectTransform>().anchoredPosition = prefab.anchoredPosition;
			i2.GetComponent<RectTransform>().anchorMax = prefab.anchorMax;
			i2.GetComponent<RectTransform>().anchorMin = prefab.anchorMin;
			i2.GetComponent<RectTransform>().localRotation = prefab.localRotation;
			i2.GetComponent<RectTransform>().localScale = prefab.localScale;
			i2.GetComponent<RectTransform>().pivot = prefab.pivot;
			i2.GetComponent<RectTransform>().sizeDelta = prefab.sizeDelta;
			i2.transform.position = Camera.main.WorldToScreenPoint(intObjects[i].transform.position);
			intObjGraphics[i].SetActive(false);
		}

	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0) && GameController.instance.playerState != GameController.PlayerState.Zoom)
		{
			if(GameController.instance.gameSettings.clickCircle != null && !clickCircleShow)
			{
				GameObject c = Instantiate(GameController.instance.gameSettings.clickCircle, Vector3.zero, Quaternion.identity) as GameObject;
				c.transform.SetParent(GameController.instance.uiCanvas.transform);
				Vector3 guiPosition = Input.mousePosition;
				c.transform.position = guiPosition;
				Destroy (c, 1);
				StartCoroutine(Cooldown());
			}

			StartCoroutine (Counter ());
			down = true;
		}
		if(Input.GetMouseButtonUp(0))
		{
			down = false;
			ToggleGraphics(false);
		}
	}
//	public void OnMouseDown()
//	{
//		Debug.Log ("BVEU");
//		StartCoroutine (Counter ());
//		down = true;
//	}
//
//	public void OnMouseUp()
//	{
//		down = false;
//		ToggleGraphics(false);
//	}
	IEnumerator Cooldown()
	{
		clickCircleShow = true;
		yield return new WaitForSeconds (0.4f);
		clickCircleShow = false;
	}
	IEnumerator Counter()
	{
		yield return new WaitForSeconds(waitBeforeShow);
		if (down) 
		{
			ToggleGraphics(true);
		}
	}

	void ToggleGraphics(bool show)
	{

		for(int i = 0; i< intObjGraphics.Count; i++)
		{
			intObjGraphics[i].SetActive(show);
		}
	}
}
