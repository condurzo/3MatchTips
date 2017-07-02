using UnityEngine;
using System.Collections;

public class ColumnScript : MonoBehaviour 
{
	internal int columnIndex = 0;    

	internal ArrayList playingObjectsScriptList;
	internal ArrayList jellyObjects;

	public PlayingObject[] playingObjectsScript;

	public int[] itemAvailability;
	public int[] jellyAvailability;

	int totalNoOfItems = 0;
	int numberOfEmptySpace = 0;

	int numberOfRows;

	void Awake()
	{

	}

	void Start () 
	{
		numberOfRows = LevelStructure.instance.numberOfRows;

		playingObjectsScriptList = new ArrayList();
		jellyObjects = new ArrayList();

		itemAvailability = LevelStructure.instance.columnStructures[columnIndex].itemAvailability;

		if(LevelStructure.instance.columnStructures[columnIndex].jelly.Length > 0)
			jellyAvailability = LevelStructure.instance.columnStructures[columnIndex].jelly;


		Invoke("PopulateInitialColumn", .2f);

		InvokeRepeating("UpdateList", 1, 1);

	}

	void UpdateList()
	{
		if (GameManager.instance.isBusy)
			return;

		playingObjectsScript = new PlayingObject[numberOfRows];
		for (int i = 0; i < playingObjectsScript.Length; i++)
		{
			playingObjectsScript[i] = (PlayingObject)playingObjectsScriptList[i];
		}
	}

	void AddJelly(int index)
	{
		GameObject parent = null;

		jellyAvailability[index] = Mathf.Clamp(jellyAvailability[index], 0, 2);

		for (int i = 0; i < jellyAvailability[index]; i++)
		{
			GameManager.instance.totalNoOfJellies++;
			GameManager.instance.jellyText.text = "Jelly : " + GameManager.instance.totalNoOfJellies.ToString();
			GameObject temp = (GameObject)Instantiate(GameManager.instance.jellyPrefab[i], Vector3.zero, Quaternion.identity);
			temp.transform.parent = transform;
			temp.transform.localPosition = new Vector3(0, -index * GameManager.instance.gapBetweenObjects, -.5f + i * .1f);
			temp.transform.localEulerAngles = new Vector3(90, 0, 0);

			if (parent == null)
			{                
				jellyObjects.Add(temp);
				parent = temp;
			}
			else
			{
				temp.transform.parent = parent.transform;
			}

			parent = temp;

		}



	}

	void CheckForJelly(int i)
	{
		if (jellyAvailability != null)
		{
			if (jellyAvailability[i] >= 1)
			{
				AddJelly(i);
			}
			else
				jellyObjects.Add(null);
		}
		else
			jellyObjects.Add(null);
	}

	void PopulateInitialColumn()
	{        

		for (int i = 0; i < itemAvailability.Length; i++)
		{

			if (itemAvailability[i] >= 1)
				totalNoOfItems++;
		}


		numberOfEmptySpace = numberOfRows - totalNoOfItems;


		for (int i = 0; i < numberOfRows; i++)
		{
			if (itemAvailability[i] == 0)
			{
				playingObjectsScriptList.Add(null);
				jellyObjects.Add(null);
				continue;
			}


			int index = Random.Range(0, 4);//ACA (0,6)

			GameObject objectPrefab;

			if (itemAvailability[i] == 1)
			{
				objectPrefab = GameManager.instance.playingObjectPrefabs[index];                
			}
			else if (itemAvailability[i] == 2)
			{
				if (Random.value < .5f) {
					objectPrefab = GameManager.instance.horizontalPrefabs [Random.Range (0, GameManager.instance.horizontalPrefabs.Length)];
					Debug.Log ("Rompio: " + GameManager.instance.horizontalPrefabs.Length);
				} else {
					objectPrefab = GameManager.instance.verticalPrefabs [Random.Range (0, GameManager.instance.verticalPrefabs.Length)];
					Debug.Log ("Rompio: " + GameManager.instance.verticalPrefabs.Length);
				}
			}
			else if (itemAvailability[i] == 3)
				objectPrefab = GameManager.instance.universalPlayingObjectPrefab;
			else
				objectPrefab = GameManager.instance.playingObjectPrefabs[index];

			CheckForJelly(i);

			GameObject temp = (GameObject)Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
			PlayingObject po = temp.GetComponent<PlayingObject> ();
			po.myColumnScript = this;
			po.indexInColumn = i;
			playingObjectsScriptList.Add(po);
			temp.transform.parent = transform;
			temp.transform.localPosition = new Vector3(0, -i * GameManager.instance.gapBetweenObjects, 0);

			GameObject temp1 = (GameObject)Instantiate(GamePrefabs.instance.playingObjectBackPrefab, temp.transform.position - new Vector3(0, 0, 1), Quaternion.identity);
			temp1.transform.localEulerAngles = new Vector3(90, 0, 0);
		}        
	}

	internal void AssignNeighbours()
	{
		for (int i = 0; i < playingObjectsScriptList.Count; i++) 
		{
			if (playingObjectsScriptList [i] == null)
				continue;

			if (columnIndex == 0)//left
				((PlayingObject)playingObjectsScriptList [i]).adjacentItems [0] = null;
			else
				((PlayingObject)playingObjectsScriptList [i]).adjacentItems [0] = ((PlayingObject)ColumnManager.instance.gameColumns [columnIndex - 1].playingObjectsScriptList [i]);

			if (columnIndex == ColumnManager.instance.gameColumns.Length - 1) // right
				((PlayingObject)playingObjectsScriptList [i]).adjacentItems [1] = null;
			else
				((PlayingObject)playingObjectsScriptList[i]).adjacentItems[1] = ((PlayingObject)ColumnManager.instance.gameColumns[columnIndex + 1].playingObjectsScriptList[i]);

			if (i == 0) //up
				((PlayingObject)playingObjectsScriptList[i]).adjacentItems[2] = null;
			else
				((PlayingObject)playingObjectsScriptList[i]).adjacentItems[2] = ((PlayingObject)playingObjectsScriptList[i - 1]);

			if (i == numberOfRows - 1) // down
				((PlayingObject)playingObjectsScriptList[i]).adjacentItems[3] = null;
			else
				((PlayingObject)playingObjectsScriptList[i]).adjacentItems[3] = ((PlayingObject)playingObjectsScriptList[i + 1]);
		}
	}

	internal void ChangeItem(int index, GameObject newItemPrefab,string _name)
	{
		GameObject temp = (GameObject)Instantiate(newItemPrefab, Vector3.zero, Quaternion.identity);
		temp.GetComponent<PlayingObject>().myColumnScript = this;
		temp.GetComponent<PlayingObject>().indexInColumn = index;
		playingObjectsScriptList[index] = temp.GetComponent<PlayingObject>();
		temp.transform.parent = transform;
		temp.transform.localPosition = new Vector3(0, -index * GameManager.instance.gapBetweenObjects, 0);
		temp.GetComponent<SpecialPlayingObject>().name = _name;
		// iTween.ScaleFrom(temp, Vector3.zero, .6f);
		//   print("new Candy Created");
	}

	internal void DeleteBrustedItems()
	{       

		for (int i = 0; i < numberOfRows; i++)
		{
			if (playingObjectsScriptList[i] != null)
			{
				if (((PlayingObject)playingObjectsScriptList[i]).brust)
				{
					((PlayingObject)playingObjectsScriptList[i]).DestroyMe();

					GameObject specialObject = ((PlayingObject)playingObjectsScriptList[i]).WillFormSpecialObject();

					if (specialObject!= null)
					{
						ChangeItem(i, specialObject, ((PlayingObject)playingObjectsScriptList[i]).name);
					}
					else
						playingObjectsScriptList[i] = null;


				}
			}
		}

		int count = 0;
		for (int i = 0; i < playingObjectsScriptList.Count; i++,count++)
		{
			if ((PlayingObject)playingObjectsScriptList[i] == null && itemAvailability[count] >= 1)
			{
				playingObjectsScriptList.RemoveAt(i);
				i--;
			}
		}

		numberOfItemsToAdd = numberOfRows - playingObjectsScriptList.Count;
	}

	void ArrangeColumnItems()
	{
		for (int i = numberOfRows - 1; i >= 0; i--)
		{
			if (itemAvailability[i] >= 1 && playingObjectsScriptList[i] == null)
			{
				for (int j = i; j >= 0; j--)
				{
					if (playingObjectsScriptList[j] != null)
					{                        
						playingObjectsScriptList[i] = playingObjectsScriptList[j];
						playingObjectsScriptList[j] = null;
						break;
					}
				}
			}
		}
	}

	int numberOfItemsToAdd;

	internal int GetNumberOfItemsToAdd()
	{
		return numberOfRows - playingObjectsScriptList.Count;
	}

	internal void AddMissingItems()
	{
		
		numberOfItemsToAdd = numberOfRows - playingObjectsScriptList.Count;
		if (numberOfItemsToAdd == 0)
			return;
		//LOGICA DE CADA NIVEL Y LOGICA DE LAS DEMOS
		for (int i = 0; i < numberOfItemsToAdd; i++)
		{
			int ran = Random.Range (1, 20);

			//Esto es para el ?, hacer logica de combos
//			GameObject temp = (GameObject)Instantiate(GameManager.instance.playingObjectPrefabs[Random.Range(0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
//			temp.GetComponent<PlayingObject>().myColumnScript = this;
//			playingObjectsScriptList.Insert(0, temp.GetComponent<PlayingObject>()); //numberOfEmptySpace
//			temp.transform.parent = transform;
//			temp.transform.localPosition = new Vector3(0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);

			if (Tiempo.AguaBool) {
				switch (ran) {
				case 1:
					//time comun
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [4], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
					break;
				case 2:
					//time solo agua
					if (PlayerPrefs.GetInt ("Agua2") == 1) {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [4], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
					break;
				case 3:
					GameObject temp3 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp3.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp3.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp3.transform.parent = transform;
					temp3.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 4:
					GameObject temp4 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp4.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp4.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp4.transform.parent = transform;
					temp4.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 5:
					GameObject temp5 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp5.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp5.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp5.transform.parent = transform;
					temp5.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 6:
					GameObject temp6 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp6.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp6.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp6.transform.parent = transform;
					temp6.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 7:
					GameObject temp7 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp7.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp7.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp7.transform.parent = transform;
					temp7.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 8:
					GameObject temp8 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp8.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp8.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp8.transform.parent = transform;
					temp8.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 9:
					GameObject temp9 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp9.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp9.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp9.transform.parent = transform;
					temp9.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 10:
					GameObject temp10 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp10.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp10.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp10.transform.parent = transform;
					temp10.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 11:
					GameObject temp11 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp11.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp11.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp11.transform.parent = transform;
					temp11.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 12:
					GameObject temp12 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp12.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp12.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp12.transform.parent = transform;
					temp12.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 13:
					GameObject temp13 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp13.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp13.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp13.transform.parent = transform;
					temp13.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 14:
					GameObject temp14 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp14.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp14.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp14.transform.parent = transform;
					temp14.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 15:
					GameObject temp15 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp15.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp15.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp15.transform.parent = transform;
					temp15.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 16:
					GameObject temp16 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp16.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp16.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp16.transform.parent = transform;
					temp16.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 17:
					GameObject temp17 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
					temp17.GetComponent<PlayingObject> ().myColumnScript = this;
					playingObjectsScriptList.Insert (0, temp17.GetComponent<PlayingObject> ()); //numberOfEmptySpace
					temp17.transform.parent = transform;
					temp17.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					break;
				case 18:
					if (PlayerPrefs.GetInt ("Agua3") == 1) {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (8, 10)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
					break;
				case 19://V y H Comun
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (8, 10)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
					break;
				}
			}

				if (Tiempo.FuegoBool) {
					switch (ran) {
				case 1:
					//time
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [5], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 2:
					//time solo fuego
					if (PlayerPrefs.GetInt ("Fuego2") == 1) {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [5], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}else {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 3:
						GameObject temp3 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp3.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp3.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp3.transform.parent = transform;
						temp3.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 4:
						GameObject temp4 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp4.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp4.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp4.transform.parent = transform;
						temp4.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 5:
						GameObject temp5 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp5.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp5.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp5.transform.parent = transform;
						temp5.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 6:
						GameObject temp6 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp6.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp6.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp6.transform.parent = transform;
						temp6.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 7:
						GameObject temp7 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp7.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp7.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp7.transform.parent = transform;
						temp7.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 8:
						GameObject temp8 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp8.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp8.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp8.transform.parent = transform;
						temp8.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 9:
						GameObject temp9 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp9.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp9.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp9.transform.parent = transform;
						temp9.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 10:
						GameObject temp10 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp10.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp10.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp10.transform.parent = transform;
						temp10.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 11:
						GameObject temp11 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp11.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp11.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp11.transform.parent = transform;
						temp11.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 12:
						GameObject temp12 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp12.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp12.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp12.transform.parent = transform;
						temp12.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 13:
						GameObject temp13 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp13.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp13.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp13.transform.parent = transform;
						temp13.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 14:
						GameObject temp14 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp14.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp14.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp14.transform.parent = transform;
						temp14.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 15:
						GameObject temp15 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp15.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp15.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp15.transform.parent = transform;
						temp15.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 16:
						GameObject temp16 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp16.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp16.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp16.transform.parent = transform;
						temp16.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 17:
						GameObject temp17 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp17.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp17.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp17.transform.parent = transform;
						temp17.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 18:
					if (PlayerPrefs.GetInt ("Fuego3") == 1) {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (10,12)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 19://V y H
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (10, 12)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					}
				}
				if (Tiempo.TierraBool) {
					switch (ran) {
					case 1:
					//time
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [6], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 2:

					//time solo Tierra
					if (PlayerPrefs.GetInt ("Tierra2") == 1) {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [6], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}else {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 3:
						GameObject temp3 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp3.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp3.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp3.transform.parent = transform;
						temp3.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 4:
						GameObject temp4 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp4.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp4.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp4.transform.parent = transform;
						temp4.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 5:
						GameObject temp5 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp5.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp5.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp5.transform.parent = transform;
						temp5.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 6:
						GameObject temp6 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp6.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp6.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp6.transform.parent = transform;
						temp6.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 7:
						GameObject temp7 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp7.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp7.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp7.transform.parent = transform;
						temp7.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 8:
						GameObject temp8 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp8.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp8.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp8.transform.parent = transform;
						temp8.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 9:
						GameObject temp9 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp9.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp9.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp9.transform.parent = transform;
						temp9.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 10:
						GameObject temp10 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp10.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp10.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp10.transform.parent = transform;
						temp10.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 11:
						GameObject temp11 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp11.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp11.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp11.transform.parent = transform;
						temp11.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 12:
						GameObject temp12 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp12.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp12.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp12.transform.parent = transform;
						temp12.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 13:
						GameObject temp13 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp13.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp13.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp13.transform.parent = transform;
						temp13.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 14:
						GameObject temp14 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp14.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp14.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp14.transform.parent = transform;
						temp14.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 15:
						GameObject temp15 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp15.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp15.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp15.transform.parent = transform;
						temp15.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 16:
						GameObject temp16 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp16.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp16.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp16.transform.parent = transform;
						temp16.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 17:
						GameObject temp17 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp17.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp17.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp17.transform.parent = transform;
						temp17.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 18:
					if (PlayerPrefs.GetInt ("Tierra3") == 1) {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (12,14)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 19://V y H
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (12,14)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					}
				}
				if (Tiempo.AireBool) {
					switch (ran) {
					case 1:
					//time
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [7], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp1 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp1.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp1.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp1.transform.parent = transform;
						temp1.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 2:
					if (PlayerPrefs.GetInt ("Aire2") == 1) {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [7], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}else {
						GameObject temp2 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp2.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp2.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp2.transform.parent = transform;
						temp2.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 3:
						GameObject temp3 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp3.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp3.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp3.transform.parent = transform;
						temp3.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 4:
						GameObject temp4 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp4.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp4.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp4.transform.parent = transform;
						temp4.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 5:
						GameObject temp5 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp5.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp5.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp5.transform.parent = transform;
						temp5.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 6:
						GameObject temp6 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp6.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp6.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp6.transform.parent = transform;
						temp6.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 7:
						GameObject temp7 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp7.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp7.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp7.transform.parent = transform;
						temp7.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 8:
						GameObject temp8 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp8.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp8.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp8.transform.parent = transform;
						temp8.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 9:
						GameObject temp9 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp9.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp9.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp9.transform.parent = transform;
						temp9.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 10:
						GameObject temp10 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp10.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp10.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp10.transform.parent = transform;
						temp10.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 11:
						GameObject temp11 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp11.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp11.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp11.transform.parent = transform;
						temp11.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 12:
						GameObject temp12 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp12.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp12.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp12.transform.parent = transform;
						temp12.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 13:
						GameObject temp13 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp13.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp13.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp13.transform.parent = transform;
						temp13.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 14:
						GameObject temp14 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp14.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp14.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp14.transform.parent = transform;
						temp14.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 15:
						GameObject temp15 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp15.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp15.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp15.transform.parent = transform;
						temp15.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 16:
						GameObject temp16 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp16.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp16.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp16.transform.parent = transform;
						temp16.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 17:
						GameObject temp17 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp17.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp17.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp17.transform.parent = transform;
						temp17.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
						break;
					case 18:
					if (PlayerPrefs.GetInt ("Aire3") == 1) {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (14,16)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp18 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp18.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp18.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp18.transform.parent = transform;
						temp18.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					case 19://V y H
					if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (14,16)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					} else {
						GameObject temp19 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [Random.Range (0, 4)], Vector3.zero, Quaternion.identity);//ACA (0,6)
						temp19.GetComponent<PlayingObject> ().myColumnScript = this;
						playingObjectsScriptList.Insert (0, temp19.GetComponent<PlayingObject> ()); //numberOfEmptySpace
						temp19.transform.parent = transform;
						temp19.transform.localPosition = new Vector3 (0, (i + 1) * GameManager.instance.gapBetweenObjects, 0);
					}
						break;
					}
				}

		}

		ArrangeColumnItems();

		for (int i = 0; i < playingObjectsScriptList.Count; i++)
		{
			if ((PlayingObject)playingObjectsScriptList[i] != null)
				((PlayingObject)playingObjectsScriptList[i]).indexInColumn = i;
		}

		// iTween.Defaults.easeType = iTween.EaseType.bounce;
		iTween.Defaults.easeType = GameManager.instance.objectfallingEase;

		fallingDuration = GameManager.instance.initialObjectFallingDuration * (.9f + numberOfItemsToAdd / 10f);

		GameManager.instance.objectFallingDuration = Mathf.Max(GameManager.instance.objectFallingDuration, fallingDuration);

		SoundFxManager.instance.PlayFallingSound();
		StartMovingDownOldPart();
		StartMovingDownNewPart();
		SoundFxManager.instance.Invoke("StopFallingSound", fallingDuration * .8f);
		// Invoke("PlayColumnFallSound", fallingDuration * .2f);
	}

	void PlayColumnFallSound()
	{
		SoundFxManager.instance.columnFallSound.Play();
	}

	float fallingDuration;

	void StartMovingDownOldPart()
	{

		for (int i = numberOfItemsToAdd; i < playingObjectsScriptList.Count; i++)
		{
			if(itemAvailability[i] >= 1)
				iTween.MoveTo(((PlayingObject)playingObjectsScriptList[i]).gameObject, new Vector3(transform.position.x, -i * GameManager.instance.gapBetweenObjects + transform.position.y, 0), fallingDuration);
		}
	}

	void StartMovingDownNewPart()
	{       

		for (int i = 0; i < numberOfItemsToAdd; i++)
		{
			if (itemAvailability[i] >= 1)
				iTween.MoveTo(((PlayingObject)playingObjectsScriptList[i]).gameObject, new Vector3(transform.position.x, -i * GameManager.instance.gapBetweenObjects + transform.position.y, 0), fallingDuration);
		}


	}

	internal void UnCheckTracedAttribute()
	{
		for (int i = 0; i < playingObjectsScriptList.Count; i++)
		{
			((PlayingObject)playingObjectsScriptList[i]).isTraced = false;           
		}
	}

	internal void UnCheckBrustAttribute()
	{
		for (int i = 0; i < playingObjectsScriptList.Count; i++)
		{
			((PlayingObject)playingObjectsScriptList[i]).brust = false;
		}
	}

	internal void AssignBrustToAllItemsOfName(string itemName)
	{
		for (int i = 0; i < playingObjectsScriptList.Count; i++)
		{
			if ((PlayingObject)playingObjectsScriptList[i])
			{
				if (((PlayingObject)playingObjectsScriptList[i]).name == itemName)
					((PlayingObject)playingObjectsScriptList[i]).AssignBurst("smoke");
			}
		}
	}







}
