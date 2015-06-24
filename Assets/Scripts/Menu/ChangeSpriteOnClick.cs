using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChangeSpriteOnClick : MonoBehaviour {

	public Image spriteTo;
	public Image spriteFrom;

	public void Change()
	{
		if (spriteFrom.enabled)
		{
//			Debug.Log ("VNBEU");
			spriteTo.enabled = true;
			spriteFrom.enabled = false;
		}
		else if(spriteTo.enabled)
		{
			spriteTo.enabled = false;
			spriteFrom.enabled = true;
		}

	}
}
