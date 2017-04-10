using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSpriteControl : MonoBehaviour {

	public static TraitSpriteControl control;

	private Transform traitUIParent;

    public Sprite machineGun;
    public Sprite backLineCommander;
    public Sprite assault;
    public Sprite grenedier;
    public Sprite rifleman;
    public Sprite adrenalineJunky;
    public Sprite m31MarriedToF32;
    public Sprite m31GoodWithM29;
    public Sprite f32GoodWithM41;
    public Sprite f27BadWithM40;
    public Sprite f27GoodWithF25;
    public Sprite brutalEfficiency;
    public Sprite f29GoodWithF28;
    public Sprite sleepDeprived;
    public Sprite f28GoodWithF29;
    public Sprite frontLineCommander;

    public List<Sprite> TraitsInOrderOfCreatedClass;

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
			TraitsInOrderOfCreatedClass.Add (adrenalineJunky);
			TraitsInOrderOfCreatedClass.Add (rifleman);
			TraitsInOrderOfCreatedClass.Add (grenedier);
			TraitsInOrderOfCreatedClass.Add (assault);
			TraitsInOrderOfCreatedClass.Add (f29GoodWithF28);
			TraitsInOrderOfCreatedClass.Add (sleepDeprived);
			TraitsInOrderOfCreatedClass.Add (f28GoodWithF29);
			TraitsInOrderOfCreatedClass.Add (frontLineCommander);

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

        GameObject background = GameObject.Find("Background");

        background.GetComponent<Image>().enabled = true;
        //background.SetActive(true);

        string textBase = "Trait";
        int loopHeight = 0;

        for (int i = 0; i < traitList.Count; ++i)
        {
            //can only handle 9 traits
            if (i < 9)
            {
                int temp = i + 1;
                string textToFind = textBase + temp;
                Debug.Log(textToFind);
                GameObject trait = GameObject.Find(textToFind);
                Debug.Log(trait.name);
                Debug.Log(traitList[i].GetName() + " " + traitList[i].GetPositionInSpriteControlList());
                trait.GetComponent<Image>().sprite = TraitsInOrderOfCreatedClass[traitList[i].GetPositionInSpriteControlList()];
                trait.GetComponent<Image>().enabled = true;
				trait.GetComponent<IndividualTraitScipt> ().SetInfo (traitList [i].GetName ());
                //trait.SetActive(true);
                //increase safetycheck
                ++loopHeight;
            }
        }

        //disable unused icon spots
        for (int i = loopHeight; i < 9; ++i)
        {
            int temp = i + 1;
            string textToFind = textBase + temp;
            //Debug.Log(textToFind + " " + i);
            GameObject trait = GameObject.Find(textToFind);
            //Debug.Log(trait.name);
            trait.GetComponent<Image>().enabled = false;
            //trait.SetActive(false);
        }
    }

	public void UnShowTraits(){
        GameObject background = GameObject.Find("Background");

        background.GetComponent<Image>().enabled = false;
        //background.SetActive(false);

        string textBase = "Trait";

        for (int i = 0; i < 9; ++i)
        {
            int temp = i + 1;
            string textToFind = textBase + temp;
            //Debug.Log(textToFind + " " + i);
            GameObject trait = GameObject.Find(textToFind);
            //Debug.Log(trait.name);
            trait.GetComponent<Image>().enabled = false;
            //trait.SetActive(false);
        }

		GameObject textBackground = GameObject.Find("TextBackground");
		textBackground.GetComponent<Image>().enabled = false;
    }

	public Transform GetUIParent() {
        return traitUIParent;
	}
}
