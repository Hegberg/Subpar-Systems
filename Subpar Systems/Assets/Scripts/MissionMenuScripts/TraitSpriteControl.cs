using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitSpriteControl : MonoBehaviour {

	public static TraitSpriteControl control;
	private Transform traitUIParent;

	public Transform testUISprite;
	public Transform traitBackgroundUI;
	public Transform machineGun;
	public Transform backLineCommander;
	public Transform assault;
	public Transform grenedier;
	public Transform rifleman;
	public Transform adrenalineJunky;
	public Transform m31WifeF32Dead;
	public Transform m31FriendM29Dead;
	public Transform m31MarriedToF32;
	public Transform m31GoodWithM29;
	public Transform f32GoodWithM41;
	public Transform f27BadWithM40;
	public Transform f27GoodWithF25;
	public Transform brutalEfficiency;

	private int traitsPerRow = 3;
	private int denominatorOfFractionOfExtraSpace = 4;

	//set anchor to top left
	private Vector2 anchorMin = new Vector2(0f, 1f);
	private Vector2 anchorMax = new Vector2(0f, 1f);

	// Use this for initialization
	void Start () {
		if (control == null)
		{
			control = this;

			DontDestroyOnLoad(this.gameObject);

			traitUIParent = this.gameObject.transform;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowTraits(List<GenericTraitsScript> traitList){
		//set trait vector for background
		Vector3 traitVector = new Vector3( traitBackgroundUI.GetComponent<Renderer>().bounds.size.x / 2,
			-traitBackgroundUI.GetComponent<Renderer>().bounds.size.y / 2, 0);
		//create first trait vector
		traitUIParent = GameObject.FindObjectOfType<Canvas>().transform;
		Transform traitBackground = (Transform)Instantiate(traitBackgroundUI, traitVector, Quaternion.identity);
		RectTransform traitRectBackground = traitBackground.GetComponent<RectTransform> ();
		traitRectBackground.anchorMin = anchorMin;
		traitRectBackground.anchorMax = anchorMax;
		traitBackground.SetParent (traitUIParent, false);

		//create each trait
		for (int i = 0; i < traitList.Count; ++i) {
			
			traitVector.x = (((i % traitsPerRow) + 0.5f) * 
				(testUISprite.GetComponent<Renderer>().bounds.size.x + (testUISprite.GetComponent<Renderer>().bounds.size.x/denominatorOfFractionOfExtraSpace)));
			traitVector.y = ((i / traitsPerRow) * 
				(-testUISprite.GetComponent<Renderer>().bounds.size.y - (testUISprite.GetComponent<Renderer>().bounds.size.y/denominatorOfFractionOfExtraSpace)));
			traitVector.y -= traitBackgroundUI.GetComponent<Renderer> ().bounds.size.y / 2;
			traitVector.z = 0;
			Transform trait = (Transform)Instantiate(testUISprite, traitVector, Quaternion.identity);
			RectTransform traitRect = trait.GetComponent<RectTransform> ();
			traitRect.anchorMin = anchorMin;
			traitRect.anchorMax = anchorMax;
			trait.SetParent (traitUIParent, false);
		}
	}

	public void UnShowTraits(){
		traitUIParent.BroadcastMessage ("DestroyItem");
	}
}
