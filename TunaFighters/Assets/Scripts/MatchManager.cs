using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {
	Costumer[] costumers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ModifyCostumer(int index, bool unlocked, int roastLevel, bool notify = true)
	{
		if (index < costumers.Length) {
			costumers [index].Unlocked = unlocked;
			costumers [index].RoastLevel = roastLevel;
		}
	}
}

class Costumer
{
	public bool Unlocked{ get; set; }
	public int RoastLevel{get;set;}
}
