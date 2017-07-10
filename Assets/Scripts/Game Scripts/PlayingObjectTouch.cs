using UnityEngine;
using System.Collections;

public class PlayingObjectTouch : MonoBehaviour
{
    PlayingObject playingObjectScript;


    void Start()
    {
        playingObjectScript = GetComponent<PlayingObject>();

    }

    bool isTouched = false;

    internal void OnMouseDown ()
	{
		//CHEAT BOMBA
		if (Input.GetKey (KeyCode.B)) {
			int num = 0;
			switch(this.gameObject.name){
			case "Playing Object 1(Clone)":
				num = 17;
				break;
			case "Playing Object 2(Clone)":
				num = 18;
				break;
			case "Playing Object 3(Clone)":
				num = 19;
				break;
			case "Playing Object 4(Clone)":
				num = 20;
				break;
				default:
				num = 0;
				break;
			}
			GameObject bomb = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [num], Vector3.zero, Quaternion.identity);//ACA (0,6)
			bomb.transform.SetParent(this.gameObject.transform.parent);
			CopyComponent<PlayingObject> (playingObjectScript, bomb);
			bomb.GetComponent<PlayingObject> ().objectType = ObjectType.Bomb;
			playingObjectScript.myColumnScript.playingObjectsScriptList[playingObjectScript.indexInColumn]=bomb.GetComponent<PlayingObject>();
			playingObjectScript.myColumnScript.StartMovingDownOldPart ();
			playingObjectScript.myColumnScript.StartMovingDownNewPart();
			playingObjectScript.myColumnScript.DonarAdyacentes (playingObjectScript, bomb.GetComponent<PlayingObject> ());
			bomb.GetComponent<PlayingObject> ().adjacentItems = playingObjectScript.adjacentItems;
			GameOperations.instance.Invoke("AssignNeighbours", .1f);
			Destroy (this.gameObject);
			return;
		}



        if (GameManager.instance.isBusy)
            return;

        isTouched = true;
        SoundFxManager.instance.objectPickSound.Play();

        ObjectSelected();
    }

	T CopyComponent<T>(T original, GameObject destination) where T : Component
     {
         System.Type type = original.GetType();
         var dst = destination.GetComponent(type) as T;
         if (!dst) dst = destination.AddComponent(type) as T;
         var fields = type.GetFields();
         foreach (var field in fields)
         {
             if (field.IsStatic) continue;
             field.SetValue(dst, field.GetValue(original));
         }
         var props = type.GetProperties();
         foreach (var prop in props)
         {
             if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
             prop.SetValue(dst, prop.GetValue(original, null), null);
         }
         return dst as T;
     }

    internal void ObjectSelected()
    {
        if (playingObjectScript.isSelected)
        {
            playingObjectScript.UnSelectMe();
            GameOperations.instance.item1 = null;
            return;
        }

        if (GameOperations.instance.item1 == null)
        {
            GameOperations.instance.item1 = playingObjectScript;
            playingObjectScript.SelectMe();
        }
        else if (Vector2.Distance((Vector2)GameOperations.instance.item1.transform.position, (Vector2)transform.position) < GameManager.instance.gapBetweenObjects + .2f)
        {
            GameOperations.instance.item2 = playingObjectScript;
            playingObjectScript.SelectMe();
            SwapTwoObject.instance.SwapTwoItems(GameOperations.instance.item1, GameOperations.instance.item2);
        }
        else
        {
            GameOperations.instance.item1.UnSelectMe();
            GameOperations.instance.item1 = null;

            GameOperations.instance.item1 = playingObjectScript;
            playingObjectScript.SelectMe();
        }
    }


    void OnMouseDrag ()
	{
		if (!isTouched)
			return;

		Vector3 tempPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        
		if (Vector2.Distance (transform.position, tempPosition) > GameManager.instance.gapBetweenObjects * .4f) {
			if (Mathf.Abs (tempPosition.x - transform.position.x) > Mathf.Abs (tempPosition.y - transform.position.y)) { //left right moved
				if (tempPosition.x > transform.position.x) {
					if (playingObjectScript.adjacentItems.Izquierda != null) {
						playingObjectScript.adjacentItems.Izquierda.GetComponent<PlayingObjectTouch> ().ObjectSelected ();
					} else {
						Debug.Log("NULL");
					}
					} else {
						if (playingObjectScript.adjacentItems.Derecha != null) {
							playingObjectScript.adjacentItems.Derecha.GetComponent<PlayingObjectTouch> ().ObjectSelected ();
					}else {
						Debug.Log("NULL");
					}
                }
            }
            else
            {
                if (tempPosition.y > transform.position.y)
                {
                    if (playingObjectScript.adjacentItems.Arriba != null){
                        playingObjectScript.adjacentItems.Arriba.GetComponent<PlayingObjectTouch>().ObjectSelected();
					}else {
						Debug.Log("NULL");
					}
                }
                else
                {
                    if (playingObjectScript.adjacentItems.Abajo != null){
                        playingObjectScript.adjacentItems.Abajo.GetComponent<PlayingObjectTouch>().ObjectSelected();
					}else {
						Debug.Log("NULL");
					}
                }
            }

            
            OnMouseUp();
        }
        
    }

    void OnMouseUp()
    {
        isTouched = false;
        playingObjectScript.UnSelectMe();
        GameOperations.instance.item1 = null;
    }

}

    

   

