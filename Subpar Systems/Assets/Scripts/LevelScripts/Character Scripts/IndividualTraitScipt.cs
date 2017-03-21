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
		//Debug.Log ("Another happy landing");
		//Debug.Log ("Info " + infoTrait.GetName());

		GameObject textDescription = GameObject.Find ("TraitDescription");
		Debug.Log (textDescription.name.ToString());
		textDescription.GetComponent<Text> ().text = infoTrait.GetName ();
		/*
		GameObject infoName = new GameObject ("text", typeof(RectTransform));
		Text textComponent = infoName.AddComponent<Text> ();
		infoName.transform.SetParent (TraitSpriteControl.control.GetUIParent ());

		textComponent.text = infoTrait.GetName ();
		textComponent.alignment = TextAnchor.UpperLeft;
		textComponent.fontSize = 24;
		infoName.transform.SetParent (TraitSpriteControl.control.GetUIParent ());
		*/
		/*
		TextGenerationSettings settings = new TextGenerationSettings ();
		settings.textAnchor = TextAnchor.UpperLeft;
		settings.color = Color.white;
		settings.generationExtents = new Vector2 (500.0f, 500.0f);
		settings.pivot = Vector2.zero;
		settings.richText = true;
		settings.fontSize = 24;
		settings.verticalOverflow = VerticalWrapMode.Overflow;
		TextGenerator generator = new TextGenerator ();
		generator.Populate (infoTrait.GetName (), settings);
		Debug.Log ("I generated: " + generator.vertexCount + "verts!");
		*/

	}

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log ("Another happy takeOff");
		GameObject textDescription = GameObject.Find ("TraitDescription");
		Debug.Log (textDescription.name.ToString());
		textDescription.GetComponent<Text> ().text = "";
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
