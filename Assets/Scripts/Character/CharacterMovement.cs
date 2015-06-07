using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	[HideInInspector]
	public bool moving;
	private SpriteRenderer sRenderer;
	private Color startColor;
	private Color endColor = new Color(1,1,1,0);
	//move to area position function
	public float fadeSpeed = 2;

	void Start()
	{
		sRenderer = GetComponent<SpriteRenderer> ();

		startColor = sRenderer.color;
	}
	public IEnumerator FadeOut(Vector2 pos)
	{
		moving = true;
		float mTime = 0;
		bool onOff = true;
		while (onOff) 
		{
			if(mTime < 1)
			{
				mTime += Time.deltaTime * fadeSpeed;
				sRenderer.color = Color.Lerp(startColor, endColor, mTime);
			}
			else
			{
				SetPosition(pos); 
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}

	void SetPosition(Vector2 pos)
	{
		this.transform.position = pos;
		StartCoroutine (FadeIn ());
	}
	public IEnumerator FadeIn()
	{
		float mTime = 0;
		bool onOff = true;
		while (onOff) 
		{
			if(mTime < 1)
			{
				mTime += Time.deltaTime * fadeSpeed;
				sRenderer.color = Color.Lerp(endColor, startColor, mTime);
			}
			else
			{
//				SetArea();
				moving = false;
				onOff = false;
				yield break;
			}
			yield return null;
		}
	}
//	void SetArea()
//	{
//		moving = true;
//	}
}
