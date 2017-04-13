using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSpriteControl : MonoBehaviour {

	public static TraitSpriteControl control;

	private Transform traitUIParent;

    public Sprite machineGun;
    public Sprite backLineCommander;
	public Sprite schoolBonds;
	public Sprite personalGrudges;
	public Sprite lackOfHumour;
	public Sprite distractingThoughts;
	public Sprite marriedLife;
    public Sprite assault;
    public Sprite grenedier;
    public Sprite rifleman;
    public Sprite adrenalineJunky;
    public Sprite brutalEfficiency;
	public Sprite canuckistanEquipment;
    public Sprite sleepDeprived;
    public Sprite frontLineCommander;
	public Sprite tank;
	public Sprite patriotism;
	public Sprite sSTraining;
	public Sprite attachment;
	public Sprite hardworking;
	public Sprite berserkRage;
	public Sprite angerIssues;
	public Sprite tooAngryToFeelPain;
	public Sprite handsOffLeaderShip;
	public Sprite comradery;
	public Sprite runningTally;
	public Sprite lovedByTroops;
	public Sprite drugAddiction;
	public Sprite somethingToLiveFor;
	public Sprite brotherBonds;
	public Sprite imAWarHeroDammit;
	public Sprite iveKilledSeveralBoysJustLikeYou;
	public Sprite medicalProfessional;
	public Sprite recklessAbandon;
	public Sprite flashbacks;

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
			TraitsInOrderOfCreatedClass.Add (schoolBonds);
			TraitsInOrderOfCreatedClass.Add (personalGrudges);
			TraitsInOrderOfCreatedClass.Add (lackOfHumour);
			TraitsInOrderOfCreatedClass.Add (distractingThoughts);
			TraitsInOrderOfCreatedClass.Add (marriedLife);
			TraitsInOrderOfCreatedClass.Add (adrenalineJunky);
			TraitsInOrderOfCreatedClass.Add (rifleman);
			TraitsInOrderOfCreatedClass.Add (grenedier);
			TraitsInOrderOfCreatedClass.Add (assault);
			TraitsInOrderOfCreatedClass.Add (canuckistanEquipment);
			TraitsInOrderOfCreatedClass.Add (sleepDeprived);
			TraitsInOrderOfCreatedClass.Add (patriotism);
			TraitsInOrderOfCreatedClass.Add (frontLineCommander);
			TraitsInOrderOfCreatedClass.Add (tank);
			TraitsInOrderOfCreatedClass.Add (sSTraining);
			TraitsInOrderOfCreatedClass.Add (attachment);
			TraitsInOrderOfCreatedClass.Add (hardworking);
			TraitsInOrderOfCreatedClass.Add (berserkRage);
			TraitsInOrderOfCreatedClass.Add (angerIssues);
			TraitsInOrderOfCreatedClass.Add (tooAngryToFeelPain);
			TraitsInOrderOfCreatedClass.Add (handsOffLeaderShip);
			TraitsInOrderOfCreatedClass.Add (comradery);
			TraitsInOrderOfCreatedClass.Add (runningTally);
			TraitsInOrderOfCreatedClass.Add (lovedByTroops);
			TraitsInOrderOfCreatedClass.Add (drugAddiction);
			TraitsInOrderOfCreatedClass.Add (somethingToLiveFor);
			TraitsInOrderOfCreatedClass.Add (brotherBonds);
			TraitsInOrderOfCreatedClass.Add (imAWarHeroDammit);
			TraitsInOrderOfCreatedClass.Add (iveKilledSeveralBoysJustLikeYou);
			TraitsInOrderOfCreatedClass.Add (medicalProfessional);
			TraitsInOrderOfCreatedClass.Add (recklessAbandon);
			TraitsInOrderOfCreatedClass.Add (flashbacks);

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
                //Debug.Log(textToFind);
                GameObject trait = GameObject.Find(textToFind);
                //Debug.Log(trait.name);
                //Debug.Log(traitList[i].GetName() + " " + traitList[i].GetPositionInSpriteControlList());
                trait.GetComponent<Image>().sprite = TraitsInOrderOfCreatedClass[traitList[i].GetPositionInSpriteControlList()];
                trait.GetComponent<Image>().enabled = true;
				//trait.GetComponent<IndividualTraitScipt> ().SetInfo (traitList [i].GetName ());
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