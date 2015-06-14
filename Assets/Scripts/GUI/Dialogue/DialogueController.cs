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
	public GameObject obsTextObj;
	public Text obsTextC;
	private GameObject continueButtonPrefab;
	[HideInInspector]
	public GameObject continueButton;

	private GameObject endButtonPrefab;
	private GameObject endButton;

	private string[] choices;


	void Awake()
	{

	}
	void Start () {
		Dialoguer.Initialize ();


		//observe text init
		gCon = GameController.instance;
		obsTextPrefab = gCon.gameSettings.observeText;
		obsTextObj = Instantiate (obsTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		obsTextObj.transform.SetParent (gCon.dialogueHolder.transform);
		obsTextObj.GetComponent<RectTransform> ().localPosition = obsTextPrefab.transform.position;
		obsTextObj.GetComponent<RectTransform> ().localScale = Vector3.one;
		obsTextC = obsTextObj.GetComponentInChildren<Text> ();

		obsTextObj.SetActive (false);


		//continue button init
		continueButtonPrefab = gCon.gameSettings.continueButton;
		continueButton = Instantiate (continueButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		continueButton.transform.SetParent (gCon.dialogueHolder.transform);
		continueButton.GetComponent<RectTransform> ().localPosition = continueButtonPrefab.transform.position;
		continueButton.GetComponent<RectTransform> ().localScale = Vector3.one;
		continueButton.SetActive (false);
		continueButton.GetComponent<Button> ().onClick.AddListener(() => Dialoguer.ContinueDialogue());

		//endButton init
		endButtonPrefab = gCon.gameSettings.endButton;
		endButton = Instantiate ( endButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		endButton.transform.SetParent (gCon.dialogueHolder.transform);
		endButton.GetComponent<RectTransform> ().localPosition = endButtonPrefab.transform.position;
		endButton.GetComponent<RectTransform> ().localScale = Vector3.one;
		endButton.SetActive (false);
		endButton.GetComponent<Button> ().onClick.AddListener(() => Dialoguer.EndDialogue());


		Dialoguer.events.onStarted += OnStarted;
		Dialoguer.events.onEnded += OnEnded;
		Dialoguer.events.onTextPhase += OnTextPhase;

	}


	private void OnStarted()
	{
		obsTextObj.SetActive (false);

		started = true;
	}

	private void OnEnded()
	{
		started = false;
		obsTextObj.SetActive (false);
		continueButton.SetActive (false);
		endButton.SetActive (false);
	}

	private void OnTextPhase(DialoguerTextData data)
	{
//		Debug.Log (data.choices);
		choices = data.choices;
		text = data.text;
		endButton.SetActive (true);
		if(choices == null) 
		{
			continueButton.SetActive(true);
		}
		obsTextC.text = text;
//		obsText.GetComponentInChildren<Text> ().text = text;
	}

}
