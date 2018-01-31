using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public DialogueTrigger nextDialogue;

	public GameObject[] startRenders;
	public GameObject[] startArrows;

	public void TriggerDialogue(){
		foreach (GameObject g in startArrows) 
			g.SetActive (true);
		
		foreach (GameObject g in startRenders) 
			g.SetActive (true);
		
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue, this);
	}

	public void DialogueEnded(){
		foreach (GameObject g in startArrows) {
			g.SetActive (false);
		}
		if(nextDialogue != null)
			nextDialogue.TriggerDialogue ();
	}
}
