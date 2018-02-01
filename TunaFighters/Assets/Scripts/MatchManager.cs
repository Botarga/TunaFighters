using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {
	Customer[] customers;
	public Button customersButton;
	public Image customersLetter;

	// Use this for initialization
	void Start () {
		customers = new Customer[5];
		for (int i = 0; i < customers.Length; ++i)
			customers [i] = new Customer ();
		ModifyCustomer (0, true, Random.Range(1, 6) << 1);
	}
	
	// Update is called once per frame
	void Update () {
		
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


}

class Customer
{
	public bool Unlocked{ get; set; }
	public int RoastLevel{get;set;}
}
