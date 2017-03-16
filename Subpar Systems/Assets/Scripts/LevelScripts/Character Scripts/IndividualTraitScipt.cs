using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTraitScipt : MonoBehaviour {

	public GenericTraitsScript infoTrait;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DestroyItem() {
		Destroy (this.gameObject);
	}

	public void OnMouseOver(){
		Debug.Log ("HI");
		if (infoTrait != null) {
			Debug.Log (infoTrait.GetName ());
		}
	}

	public void SetInfoTrait(GenericTraitsScript info){
		infoTrait = info;
	}
}
