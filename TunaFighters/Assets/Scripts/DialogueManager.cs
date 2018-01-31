using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	private Queue<string> sentences;
	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
	}


	public void StartDialogue(Dialogue dialogue){
		nameText.text = dialogue.name;
		animator.SetBool ("IsOpen", true);

		sentences.Clear ();
		foreach (string s in dialogue.sentences) {
			sentences.Enqueue (s);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	IEnumerator TypeSentence(string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}
	void EndDialogue(){
		animator.SetBool ("IsOpen", false);
	}
}
