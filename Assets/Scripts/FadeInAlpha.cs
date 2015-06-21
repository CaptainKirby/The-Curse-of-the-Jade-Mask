using UnityEngine;
using System.Collections;

public class FadeInAlpha : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeIn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator FadeIn()
	{
		bool on = true;
		float mTime = 0;
		Color sColor = GetComponent<Renderer> ().material.GetColor ("_TintColor");
		while(on)
		{
			if(mTime < 1)
			{
				mTime += Time.deltaTime * 0.3f;
				GetComponent<Renderer>().material.SetColor("_TintColor", Color.Lerp(new Color(sColor.r, sColor.g, sColor.b, 0), new Color(sColor.r, sColor.g, sColor.b, 0.5f), mTime));
			}
			else
				on = false;
			yield return null;

		}
	}
}
