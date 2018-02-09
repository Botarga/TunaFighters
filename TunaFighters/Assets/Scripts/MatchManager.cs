using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 108 CARTAS
// 54 SOJA
// 36 ACEITE
// 18 SESAMO

public class MatchManager : MonoBehaviour {
	public enum TurnPhases{
		DRAW_PHASE,

		FLAME_SELECT_PHASE,
		FLAME_BATTLE_PHASE,

		TUNA_ADD_PHASE,
		TUNA_MANAGE_PHASE,
		TUNA_QUIT_PHASE,

		INGREDIENTS_PUT_PHASE,
		INGREDIENTS_STEAL_PHASE,
		PRESENTATION_PHASE,

		FLAME_PREPARE_PHASE
	};

	Customer[] customers;
	public Button customersButton;
	public Image customersLetter;
	public Image userDeck;
	public bool user1Turn;
	TurnPhases turnPhase; 

	Card[] user1Deck, user2Deck;
	private const int MAX_CARDS = 108;

	// Use this for initialization
	void Start () {
		customers = new Customer[5];
		user1Deck = ShuffleDeck(CreateDeck ());
		user2Deck = ShuffleDeck(CreateDeck ());

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

	private Card[] CreateDeck()
	{
		Card[] deck = new Card[MAX_CARDS];
		Card.ItemType targetType = Card.ItemType.SOJA;
		for(int i = 0, j = 1; i < MAX_CARDS; ++i, ++j)
		{
			if (j == 10)
				j = 1;
			if (i == 54)
			{
				j = 1;
				targetType = Card.ItemType.OIL;
			}
			else if (i == 90)
			{
				j = 1;
				targetType = Card.ItemType.SESAME;
			}
			deck[i] = new Card(targetType, j);

		}

		return deck;
	}

	private static Card[] ShuffleDeck(Card[] deck)
	{
		Card[] result = new Card[deck.Length];
		int[] index = new int[deck.Length];
		for (int i = 0; i < index.Length; ++i)
			index[i] = i;
		for(int i = 0; i < index.Length; ++i)
		{
			int a = Random.Range (0, deck.Length - 1);
			int b = Random.Range (0, deck.Length - 1);
			int c = index[a];
			index[a] = index[b];
			index[b] = c;
		}
		for(int i = 0; i < index.Length; ++i)
		{
			result[i] = deck[index[i]];
		}


		return result;
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

	public Card(ItemType type, int flame){
		Type = type;
		Flame = flame;
	}

	public ItemType Type;
	public int Flame;
}