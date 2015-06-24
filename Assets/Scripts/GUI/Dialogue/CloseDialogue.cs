using UnityEngine;
using System.Collections;

public class CloseDialogue : MonoBehaviour {

	public void CloseDialogueM()
	{
		GameController.instance.dCon.obsTextObj.SetActive(false);
		GameController.instance.dCon.continueButton.SetActive (false);
		GameController.instance.dCon.endButton.SetActive (false);
	}
}
