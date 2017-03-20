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

	public List<Transform> TraitsInOrderOfCreatedClass;

	private int traitsPerRow = 3;
	private int multipleForSpacing = 15;
	private int multipleForPushingDown = 50;

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

			TraitsInOrderOfCreatedClass.Add (machineGun);
			TraitsInOrderOfCreatedClass.Add (brutalEfficiency);
			TraitsInOrderOfCreatedClass.Add (backLineCommander);
			TraitsInOrderOfCreatedClass.Add (f27GoodWithF25);
			TraitsInOrderOfCreatedClass.Add (f27BadWithM40);
			TraitsInOrderOfCreatedClass.Add (f32GoodWithM41);
			TraitsInOrderOfCreatedClass.Add (m31GoodWithM29);
			TraitsInOrderOfCreatedClass.Add (m31MarriedToF32);
			TraitsInOrderOfCreatedClass.Add (m31FriendM29Dead);
			TraitsInOrderOfCreatedClass.Add (m31WifeF32Dead);
			TraitsInOrderOfCreatedClass.Add (adrenalineJunky);
			TraitsInOrderOfCreatedClass.Add (rifleman);
			TraitsInOrderOfCreatedClass.Add (grenedier);
			TraitsInOrderOfCreatedClass.Add (assault);

			//Debug.Log (TraitsInOrderOfCreatedClass.Count);
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

		//Debug.Log (TraitsInOrderOfCreatedClass.Count);

		//create each trait
		for (int i = 0; i < traitList.Count; ++i) {
			int tempPos = traitList [i].GetPositionInSpriteControlList ();
			traitVector.x = (((i % traitsPerRow) + 0.5f) * 
				(TraitsInOrderOfCreatedClass[tempPos].GetComponent<RectTransform>().rect.width *  multipleForSpacing));
			traitVector.y = (((i / traitsPerRow) + 1f) * 
				(-TraitsInOrderOfCreatedClass[tempPos].GetComponent<RectTransform>().rect.height * multipleForSpacing));
			traitVector.y -= (TraitsInOrderOfCreatedClass[tempPos].GetComponent<RectTransform> ().rect.height * multipleForPushingDown);
			traitVector.z = 0;
			Transform trait = (Transform)Instantiate(TraitsInOrderOfCreatedClass[tempPos], traitVector, Quaternion.identity);
			trait.gameObject.GetComponent<IndividualTraitScipt> ().SetInfoTrait (traitList [i]);
			RectTransform traitRect = trait.GetComponent<RectTransform> ();
			traitRect.anchorMin = anchorMin;
			traitRect.anchorMax = anchorMax;
			trait.SetParent (traitUIParent, false);
		}
	}

	public void UnShowTraits(){
		traitUIParent.BroadcastMessage ("DestroyItem");
	}

	public Transform GetUIParent() {
		return traitUIParent;
	}
}
