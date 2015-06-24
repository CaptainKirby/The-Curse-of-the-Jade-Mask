using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenuButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler,IPointerExitHandler {

	public enum ButtonType{NewGame, Options};
	public ButtonType buttonType;

	private bool mouseDown;
	private Text text;
	private bool insideThis;
	void Start ()  
	{
		text = GetComponentInChildren<Text> ();
	}
	public void OnPointerEnter(PointerEventData data)
	{
		insideThis = true;

//		if(mouseDown)
//		{
			text.color = GameController.instance.gameSettings.hoverColor;
//		}
	}
	public void OnPointerExit(PointerEventData data)
	{
		insideThis = false;
//		if(mouseDown)
//		{
			text.color = GameController.instance.gameSettings.activeColor;
//		}
	}
	public void OnPointerDown(PointerEventData data)
	{
		Debug.Log ("GNEUGI");
		if(insideThis)
		{
			text.color = GameController.instance.gameSettings.mouseDownColor;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		if(insideThis)
		{
			StartCoroutine(Use());
		}
	}

	IEnumerator Use()
	{
		text.color = GameController.instance.gameSettings.usedColor;
		yield return new WaitForSeconds (0.1f);
		if(buttonType == ButtonType.NewGame)
		{
			Application.LoadLevel("Office");
		}
//		if(buttonType = ButtonType.Options)
		if (insideThis)
			text.color = GameController.instance.gameSettings.hoverColor;
		else
			text.color = GameController.instance.gameSettings.activeColor;
	}
	void Update () 
	{
		if(Input.GetMouseButtonDown(0) && !mouseDown)
			mouseDown = true;
		if (Input.GetMouseButtonUp (0) && mouseDown)
			mouseDown = false;
		
	}
}
