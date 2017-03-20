using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IndividualTraitScipt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GenericTraitsScript infoTrait;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(EventSystem.current.IsPointerOverGameObject()) {
			//Debug.Log ("Is over");
			Debug.Log ("Info " + infoTrait.GetName());
		}
		*/
	}

	public void OnPointerEnter(PointerEventData eventData) {
		Debug.Log ("Another happy landing");
		Debug.Log ("Info " + infoTrait.GetName());
		/*
		GameObject infoName = new GameObject ("text", typeof(RectTransform));
		var textComponent = infoName.AddComponent<Text> ();
		textComponent.text = infoTrait.GetName ();
		textComponent.alignment = TextAnchor.UpperLeft;
		textComponent.fontSize = 24;
		infoName.transform.SetParent (TraitSpriteControl.control.GetUIParent ());
		*/
	}

	public void OnPointerExit(PointerEventData eventData) {
		Debug.Log ("Another happy takeOff");
	}

	public void DestroyItem() {
		Destroy (this.gameObject);
	}

	public void OnMouseOver(){
		Debug.Log ("MouseOver");
		if (infoTrait != null) {
			Debug.Log (infoTrait.GetName ());
		}
	}

	public void SetInfoTrait(GenericTraitsScript info){
		infoTrait = info;
	}
}
