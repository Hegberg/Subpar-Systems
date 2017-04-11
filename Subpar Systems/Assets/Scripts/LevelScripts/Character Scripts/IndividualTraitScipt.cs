using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IndividualTraitScipt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	//public GenericTraitsScript infoTrait;
	private string infoToDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void DestroyItem() {
		Destroy (this.gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (infoToDisplay != null) {
			//Debug.Log (infoToDisplay);
			GameObject textBackground = GameObject.Find("TextBackground");
			textBackground.GetComponent<Image>().enabled = true;

			GameObject textDescription = GameObject.Find ("TraitDescription");
			//Debug.Log (textDescription.name.ToString());
			textDescription.GetComponent<Text> ().text = infoToDisplay;
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		GameObject textDescription = GameObject.Find ("TraitDescription");
		textDescription.GetComponent<Text> ().text = "";

		GameObject textBackground = GameObject.Find("TextBackground");
		textBackground.GetComponent<Image>().enabled = false;
	}

	public void SetInfo(string info){
		infoToDisplay = info;
	}
}
