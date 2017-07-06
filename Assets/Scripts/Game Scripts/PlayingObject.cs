using UnityEngine;
using System.Collections;

public enum ObjectType
{
    None,
    Horizontal,
    Vertical,
    Universal,
	Doble,
	Signo,
	Bomb
};

public class PlayingObject : MonoBehaviour 
{

    public ObjectType objectType = ObjectType.None;

    public GameObject horizontalPowerPrefab;
    public GameObject verticalPowerPrefab;
    internal ColumnScript myColumnScript;
    internal int indexInColumn;
    public bool isTraced = false;
    public bool brust = false;
    public PlayingObject[] adjacentItems; //left,right,up,down

    public bool isSelected = false;
    public int itemId;
    public Vector3 initialScale;
    internal SpecialPlayingObject specialObjectScript;
    internal GameObject specialObjectToForm = null;

    string left1 = "left1";
    string left2 = "left2";
    string left3 = "left3";

    string right1 = "right1";
    string right2 = "right2";
    string right3 = "right3";

    string up1 = "up1";
    string up2 = "up2";
    string up3 = "up3";

    string down1 = "down1";
    string down2 = "down2";
    string down3 = "down3";

	public static int contadorRotas;

    void Awake()
    {
        transform.localScale = new Vector3(.7f, .7f, .7f);
        initialScale = transform.localScale;
    }
	
	void Start () 
    {
        
        specialObjectScript = GetComponent<SpecialPlayingObject>();
        itemId = GetInstanceID();
        int ind = Random.Range(0, 6);

        adjacentItems = new PlayingObject[4];
	
	}


    void CheckForSpecialCandyFormation(string objName)//ACA CHEKEO PARA METER 4 o 5
    {
		if (Tiempo.Playing) {
			if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
				if (objName == left2 && objName == left1 && objName == right1 && objName == right2) {
					parentCallingScript.specialObjectToForm = GameManager.instance.universalPlayingObjectPrefab;
				} else if (objName == up2 && objName == up1 && objName == down1 && objName == down2) {
					parentCallingScript.specialObjectToForm = GameManager.instance.universalPlayingObjectPrefab;
				}
				//}
				//ACA HACER LOGICA DE SI ES AGUA, FUEGO ...... para que no aparescan

				if ((objName == left2 && objName == left1 && objName == right1) || (objName == left1 && objName == right1 && objName == right2)) {
					if (Random.value < .5f) {
					
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 1") {
							Debug.Log ("1");
						}
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 2") {
							Debug.Log ("2");
						}
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 3") {
							Debug.Log ("3");
						}
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 4") {
							Debug.Log ("4");
						}
						parentCallingScript.specialObjectToForm = parentCallingScript.horizontalPowerPrefab;
					} else {
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 1") {
							Debug.Log ("1");
						}
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 2") {
							Debug.Log ("2");
						}
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 3") {
							Debug.Log ("3");
						}
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 4") {
							Debug.Log ("4");
						}
						parentCallingScript.specialObjectToForm = parentCallingScript.verticalPowerPrefab;
					}
				} else if ((objName == up2 && objName == up1 && objName == down1) || (objName == up1 && objName == down1 && objName == down2)) {
					if (Random.value < .5f) {
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 1") {
							Debug.Log ("1");
						}
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 2") {
							Debug.Log ("2");
						}
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 3") {
							Debug.Log ("3");
						}
						if (parentCallingScript.horizontalPowerPrefab.name == "Horizontal Split 4") {
							Debug.Log ("4");
						}
						parentCallingScript.specialObjectToForm = parentCallingScript.horizontalPowerPrefab;
					} else {
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 1") {
							Debug.Log ("1");
						}
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 2") {
							Debug.Log ("2");
						}
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 3") {
							Debug.Log ("3");
						}
						if (parentCallingScript.verticalPowerPrefab.name == "Vertical Split 4") {
							Debug.Log ("4");
						}
						parentCallingScript.specialObjectToForm = parentCallingScript.verticalPowerPrefab;
					}
				}


			}
		}

    }


    void AssignLRUD()
    {
			if (Tiempo.Playing) {

				left1 = "left1";
				left2 = "left2";
				left3 = "left3";
				right1 = "right1";
				right2 = "right2";
				right3 = "right3";
				up1 = "up1";
				up2 = "up2";
				up3 = "up3";
				down1 = "down1";
				down2 = "down2";
				down3 = "down3";

				if (adjacentItems [0]) {
					left1 = adjacentItems [0].name;
					if (adjacentItems [0].adjacentItems [0]) {
						left2 = adjacentItems [0].adjacentItems [0].name;
						if (adjacentItems [0].adjacentItems [0].adjacentItems [0])
							left3 = adjacentItems [0].adjacentItems [0].adjacentItems [0].name;
					}
				}

				if (adjacentItems [1]) {
					right1 = adjacentItems [1].name;
					if (adjacentItems [1].adjacentItems [1]) {
						right2 = adjacentItems [1].adjacentItems [1].name;
						if (adjacentItems [1].adjacentItems [1].adjacentItems [1])
							right3 = adjacentItems [1].adjacentItems [1].adjacentItems [1].name;
					}
				}

				if (adjacentItems [2]) {
					up1 = adjacentItems [2].name;
					if (adjacentItems [2].adjacentItems [2]) {
						up2 = adjacentItems [2].adjacentItems [2].name;
						if (adjacentItems [2].adjacentItems [2].adjacentItems [2])
							up3 = adjacentItems [2].adjacentItems [2].adjacentItems [2].name;
					}
				}

				if (adjacentItems [3]) {
					down1 = adjacentItems [3].name;
					if (adjacentItems [3].adjacentItems [3]) {
						down2 = adjacentItems [3].adjacentItems [3].name;
						if (adjacentItems [3].adjacentItems [3].adjacentItems [3])
							down3 = adjacentItems [3].adjacentItems [3].adjacentItems [3].name;
					}
				}
			}
		
    }

    internal bool JustCheckIfCanBrust(string objName, int parentIndex)//ACA ROMPE DE A TRES
    {
		AssignLRUD ();
		if (parentIndex == 0)
			right1 = "right1";
		if (parentIndex == 1)
			left1 = "left1";
		if (parentIndex == 2)
			down1 = "down1";
		if (parentIndex == 3)
			up1 = "up1";

		bool canBurst = false;
	
		if ((objName == left1 && objName == left2) || (objName == left1 && objName == right1) || (objName == right1 && objName == right2)
		    || (objName == up1 && objName == up2) || (objName == up1 && objName == down1) || (objName == down1 && objName == down2)) {
			canBurst = true;
			GameOperations.instance.doesHaveBrustItem = true;

		}
		if (canBurst) {
			if (parentCallingScript)

				CheckForSpecialCandyFormation (objName);
		}
		return canBurst;
    }

    internal bool CheckIfCanBrust()
    {
		if (isDestroyed)
			return false;
		if (Tiempo.Playing) {
			if (brust) {
				GameOperations.instance.doesHaveBrustItem = true;
				return true;
			}
			AssignLRUD ();
			if ((name == left1 && name == left2) || (name == left1 && name == right1) || (name == right1 && name == right2)
			   || (name == up1 && name == up2) || (name == up1 && name == down1) || (name == down1 && name == down2)) {
				AssignBurst ("normal");
				GameOperations.instance.doesHaveBrustItem = true;
			}
		}

		return brust;
    }

    string brustBy = "normal";

    internal void AssignBurst(string who)
    {
			if (Tiempo.Playing) {

				if (brust)
					return;

				brustBy = who;
				brust = true;

				if (specialObjectScript) {
					// GameOperations.instance.delay = .5f;
					GetComponent<SpecialPlayingObject> ().AssignBrustToItsTarget ();
				}

		}
    }

    internal bool isMovePossible()
    {

			for (int i = 0; i < 4; i++) {
				if (adjacentItems [i]) {
				Debug.Log (adjacentItems [i].JustCheckIfCanBrust (name, i));
					if (adjacentItems [i].JustCheckIfCanBrust (name, i)) {
						GameOperations.instance.suggestionItems [0] = this;
						GameOperations.instance.suggestionItems [1] = adjacentItems [i];
						return true;
					}
				}
			}

			return false;

    }

    static PlayingObject parentCallingScript;

    internal bool isMovePossibleInDirection(int dir)
    {

			parentCallingScript = this;
			
			if (adjacentItems [dir]) {
			Debug.Log (adjacentItems [dir].JustCheckIfCanBrust (name, dir));
				if (adjacentItems [dir].JustCheckIfCanBrust (name, dir)) {
					return true;
				}
			}

			return false;

    }

    bool isDestroyed = false;

    internal void DestroyMe()
    {
			if (Tiempo.Playing) {

		
				if (isDestroyed)
					return;

				if (myColumnScript.jellyAvailability != null) {
					if (myColumnScript.jellyAvailability [indexInColumn] == 2) {
						myColumnScript.jellyAvailability [indexInColumn] = 1;
						Destroy (((GameObject)myColumnScript.jellyObjects [indexInColumn]).transform.GetChild (0).gameObject);
						Instantiate (GamePrefabs.instance.jellyParticles, transform.position, Quaternion.identity);
						GameManager.instance.totalNoOfJellies--;
					} else if (myColumnScript.jellyAvailability [indexInColumn] == 1) {
						myColumnScript.jellyAvailability [indexInColumn] = 0;
						Destroy (((GameObject)myColumnScript.jellyObjects [indexInColumn]));
						Instantiate (GamePrefabs.instance.jellyParticles, transform.position, Quaternion.identity);
						GameManager.instance.totalNoOfJellies--;
					}
					GameManager.instance.jellyText.text = "Jelly : " + GameManager.instance.totalNoOfJellies.ToString ();

				}

				isDestroyed = true;
				//GameManager.numberOfItemsPoppedInaRow++;
				//ACA chekeo el color de la bola que exploto
				Debug.Log(this.gameObject.name);
				if(this.gameObject.name == "Playing Object 1(Clone)"){
					GameManager.explotoAgua = true;
				}
				
				
				GameManager.instance.AddScore ();

				if (specialObjectScript) {
					iTween.ScaleTo (gameObject, Vector3.zero, .8f);
				} else
					iTween.ScaleTo (gameObject, Vector3.zero, .8f);

				if (brustBy == "smoke")
					Instantiate (GamePrefabs.instance.smokeParticles, transform.position, Quaternion.identity);
				else
					Instantiate (GamePrefabs.instance.starParticles, transform.position, Quaternion.identity);

				Invoke ("Dest", 1f);
			}

    }

    void Dest()
    {
			if (Tiempo.Playing) {

				iTween.Stop (gameObject);
				Destroy (gameObject);
			}

    }

    int counter = 0;

    internal void Animate()//HINT
    {
			if (Tiempo.Playing) {

				if (counter % 2 == 0) {
					iTween.ScaleTo (gameObject, initialScale * 1.2f, .8f);
				} else {
					iTween.ScaleTo (gameObject, initialScale / 1.1f, .8f);
				}
				counter++;
				Invoke ("AnimateNO", .8f);
				CancelInvoke ("Animate");
				Invoke ("Animate", .8f);
			}

    }
	internal void AnimateNO()
	{
			if (Tiempo.Playing) {

				iTween.ScaleTo (gameObject, initialScale * 1.0f, .8f);

				//iTween.ScaleTo(gameObject, initialScale / 1.1f, .8f);
			}

	}


    internal void StopAnimating()
    {
			if (Tiempo.Playing) {

				CancelInvoke ("Animate");
				if (isSelected == false)
					iTween.Stop (gameObject);

				transform.localScale = initialScale;
			}

    }

    internal void SelectMe()
    {
			if (Tiempo.Playing) {

				isSelected = true;
				transform.Find ("Image").GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color (1, 1, 1, .5f));
			}

    }

    internal void UnSelectMe()
    {
			if (Tiempo.Playing) {

				isSelected = false;
				transform.Find ("Image").GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color (.5f, .5f, .5f, .5f));

		}
    }

    internal GameObject WillFormSpecialObject()
    {

			return specialObjectToForm;

    }

    internal void Burn()
    {
			if (Tiempo.Playing) {

				transform.Find ("Image").GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color (.2f, .2f, .2f, .5f));
			}
		}
    
}
