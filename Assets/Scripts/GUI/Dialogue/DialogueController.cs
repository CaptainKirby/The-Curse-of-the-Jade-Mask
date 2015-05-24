using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using DialoguerCore;
public class DialogueController : MonoBehaviour {
	//Dialoguer stuff
	private bool started;
	private string text;


	private GameController gCon;
	private GameObject obsTextPrefab;
	[HideInInspector]
	public GameObject obsText;
	private GameObject continueButtonPrefab;
	[HideInInspector]
	public GameObject continueButton;


	private string[] choices;


	void Awake()
	{

	}
	void Start () {
		Dialoguer.Initialize ();


		//observe text init
		gCon = GameController.instance;
		obsTextPrefab = gCon.gameSettings.observeText;
		obsText = Instantiate (obsTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		obsText.transform.SetParent (gCon.dialogueHolder.transform);
		obsText.GetComponent<RectTransform> ().localPosition = obsTextPrefab.transform.position;
		obsText.GetComponent<RectTransform> ().localScale = Vector3.one;
		obsText.SetActive (false);


		//continue button init
		continueButtonPrefab = gCon.gameSettings.continueButton;
		continueButton = Instantiate (continueButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		continueButton.transform.SetParent (gCon.dialogueHolder.transform);
		continueButton.GetComponent<RectTransform> ().localPosition = continueButtonPrefab.transform.position;
		continueButton.GetComponent<RectTransform> ().localScale = Vector3.one;
		continueButton.SetActive (false);
		continueButton.GetComponent<Button> ().onClick.AddListener(() => Dialoguer.ContinueDialogue());


		Dialoguer.events.onStarted += OnStarted ;
		Dialoguer.events.onEnded += OnEnded;
		Dialoguer.events.onTextPhase += OnTextPhase;

	}


	private void OnStarted()
	{

		started = true;
	}

	private void OnEnded()
	{
		started = false;
		obsText.SetActive (false);
		continueButton.SetActive (false);
	}

	private void OnTextPhase(DialoguerTextData data)
	{
		choices = data.choices;
		text = data.text;
		if(choices == null)
		{
			continueButton.SetActive(true);
		}
		obsText.GetComponent<Text> ().text = text;
	}

}
