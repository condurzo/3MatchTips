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
    public Adyacentes adjacentItems; //left,right,up,down

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
		adjacentItems = new Adyacentes (this);
	
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

				if (adjacentItems.Izquierda) {
					left1 = adjacentItems.Izquierda.name;
					if (adjacentItems.Izquierda.adjacentItems.Izquierda) {
						left2 = adjacentItems.Izquierda.adjacentItems.Izquierda.name;
						if (adjacentItems.Izquierda.adjacentItems.Izquierda.adjacentItems.Izquierda)
							left3 = adjacentItems.Izquierda.adjacentItems.Izquierda.adjacentItems.Izquierda.name;
					}
				}

				if (adjacentItems.Derecha) {
					right1 = adjacentItems.Derecha.name;
					if (adjacentItems.Derecha.adjacentItems.Derecha) {
						right2 = adjacentItems.Derecha.adjacentItems.Derecha.name;
						if (adjacentItems.Derecha.adjacentItems.Derecha.adjacentItems.Derecha)
							right3 = adjacentItems.Derecha.adjacentItems.Derecha.adjacentItems.Derecha.name;
					}
				}

				if (adjacentItems.Arriba) {
					up1 = adjacentItems.Arriba.name;
					if (adjacentItems.Arriba.adjacentItems.Arriba) {
						up2 = adjacentItems.Arriba.adjacentItems.Arriba.name;
						if (adjacentItems.Arriba.adjacentItems.Arriba.adjacentItems.Arriba)
							up3 = adjacentItems.Arriba.adjacentItems.Arriba.adjacentItems.Arriba.name;
					}
				}

				if (adjacentItems.Abajo) {
					down1 = adjacentItems.Abajo.name;
					if (adjacentItems.Abajo.adjacentItems.Abajo) {
						down2 = adjacentItems.Abajo.adjacentItems.Abajo.name;
						if (adjacentItems.Abajo.adjacentItems.Abajo.adjacentItems.Abajo)
							down3 = adjacentItems.Abajo.adjacentItems.Abajo.adjacentItems.Abajo.name;
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

		if (adjacentItems.Izquierda) {
			if (adjacentItems.Izquierda.JustCheckIfCanBrust (name, 0)) {
				GameOperations.instance.suggestionItems [0] = this;
				GameOperations.instance.suggestionItems [1] = adjacentItems.Izquierda;
				return true;
			}
		}
		if (adjacentItems .Derecha) {
					if (adjacentItems.Derecha.JustCheckIfCanBrust (name, 1)) {
						GameOperations.instance.suggestionItems [0] = this;
				GameOperations.instance.suggestionItems [1] = adjacentItems .Derecha;
						return true;
					}
				}
		if (adjacentItems.Arriba) {
			if (adjacentItems .Arriba.JustCheckIfCanBrust (name, 2)) {
						GameOperations.instance.suggestionItems [0] = this;
				GameOperations.instance.suggestionItems [1] = adjacentItems.Arriba;
						return true;
					}
				}
		if (adjacentItems.Abajo) {
			if (adjacentItems .Abajo.JustCheckIfCanBrust (name, 3)) {
						GameOperations.instance.suggestionItems [0] = this;
				GameOperations.instance.suggestionItems [1] = adjacentItems.Abajo;
						return true;
					}
				}

			return false;

    }

    static PlayingObject parentCallingScript;

    internal bool isMovePossibleInDirection (int dir)
	{

		parentCallingScript = this;
		switch (dir) {
		case 0:
			if (adjacentItems.Izquierda) {
				if (adjacentItems.Izquierda.JustCheckIfCanBrust (name, dir)) {
					return true;
				}

			} 
			break;
		case 1:
			if (adjacentItems.Derecha) {
				if (adjacentItems.Derecha.JustCheckIfCanBrust (name, dir)) {
					return true;
				}
			} 
			break;
		case 2:
			if (adjacentItems.Arriba) {
				if (adjacentItems.Arriba.JustCheckIfCanBrust (name, dir)) {
					return true;
				}
			} 
			break;
		case 3:
			if (adjacentItems.Abajo) {
				if (adjacentItems.Abajo.JustCheckIfCanBrust (name, dir)) {
					return true;
				}
			}
			break;
		}

		return false;
    }

    bool isDestroyed = false;

    internal void DestroyMe()
    {
			if (Tiempo.Playing) {

		
				if (isDestroyed)
					return;
//			Debug.Log (myColumnScript);
//				if (myColumnScript.jellyAvailability != null) {
//					if (myColumnScript.jellyAvailability [indexInColumn] == 2) {
//						myColumnScript.jellyAvailability [indexInColumn] = 1;
//						Destroy (((GameObject)myColumnScript.jellyObjects [indexInColumn]).transform.GetChild (0).gameObject);
//						Instantiate (GamePrefabs.instance.jellyParticles, transform.position, Quaternion.identity);
//						GameManager.instance.totalNoOfJellies--;
//					} else if (myColumnScript.jellyAvailability [indexInColumn] == 1) {
//						myColumnScript.jellyAvailability [indexInColumn] = 0;
//						Destroy (((GameObject)myColumnScript.jellyObjects [indexInColumn]));
//						Instantiate (GamePrefabs.instance.jellyParticles, transform.position, Quaternion.identity);
//						GameManager.instance.totalNoOfJellies--;
//					}
//					GameManager.instance.jellyText.text = "Jelly : " + GameManager.instance.totalNoOfJellies.ToString ();
//
//				}

				isDestroyed = true;
				//GameManager.numberOfItemsPoppedInaRow++;
				//ACA chekeo el color de la bola que exploto
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
