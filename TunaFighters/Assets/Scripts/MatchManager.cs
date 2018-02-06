using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {
	public enum TurnPhases{
		DRAW_PHASE, FLAME_SELECT_PHASE, FLAME_BATTLE_PHASE
	};

	Customer[] customers;
	public Button customersButton;
	public Image customersLetter;
	public Image userDeck;
	public bool user1Turn;
	TurnPhases turnPhase; 

	Card[] user1Deck, user2Deck;
	private const int MAX_CARDS = 50;

	// Use this for initialization
	void Start () {
		customers = new Customer[5];
		user1Deck = new Card[MAX_CARDS];
		user2Deck = new Card[MAX_CARDS];

		turnPhase = TurnPhases.DRAW_PHASE;
		user1Turn = true;
		for (int i = 0; i < customers.Length; ++i)
			customers [i] = new Customer ();
		ModifyCustomer (0, true, Random.Range(1, 6) << 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (user1Turn) {
			if (turnPhase == TurnPhases.DRAW_PHASE) {
				turnPhase = TurnPhases.FLAME_SELECT_PHASE;
				Debug.Log ("Robas una carta");
			}
		} else {
			
		}
	}

	public void ModifyCustomer(int index, bool unlocked, int roastLevel, bool notify = true)
	{
		if (index < customers.Length) {
			customers[index].Unlocked = unlocked;
			customers[index].RoastLevel = roastLevel;
			if (notify) {
				Animator an = customersButton.GetComponent(typeof(Animator)) as Animator;

				if (an != null)
					an.SetBool ("NewClient", true);

				var image = customersLetter.transform.GetChild (index).GetComponent(typeof(Image)) as Image;
				image.color = Color.white;
				var text = customersLetter.transform.GetChild (index + 5).GetComponent(typeof(Text)) as Text;
				text.text = "Roast: " + roastLevel;
			}

		}
	}

	public void DisableNewClientNotification(){
		Animator an = customersButton.GetComponent(typeof(Animator)) as Animator;

		if (an != null)
			an.SetBool ("NewClient", false);
	}

	public void ModifyCustomerLetter(){
		Animator an = customersLetter.GetComponent(typeof(Animator)) as Animator;

		if (an != null)
			an.SetBool ("OpenCustomerLetter", !an.GetBool("OpenCustomerLetter"));
	}

	public void DisableUILetter()
	{
		userDeck.gameObject.SetActive (!userDeck.IsActive());
	}

}

class Customer
{
	public bool Unlocked{ get; set; }
	public int RoastLevel{get;set;}
}

class Card
{
	public enum ItemType
	{
		SOJA, OIL, SESAME 
	};

	public ItemType Type { get; set;}
	public int Flame{ get; set;}
}