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
//			GameObject temp5 = (GameObject)Instantiate (GameManager.instance.playingObjectPrefabs [num], Vector3.zero, Quaternion.identity);//ACA (0,6)
//			PlayingObject po = temp5.GetComponent<PlayingObject> ();
//			for (int i = 0; i < po.adjacentItems.Length; i++) {
//				po.adjacentItems [i] = playingObjectScript.adjacentItems [i];
//			}
//			po.myColumnScript = playingObjectScript.myColumnScript;
//			playingObjectScript.myColumnScript.playingObjectsScriptList.Insert (0, temp5.GetComponent<PlayingObject> ()); //numberOfEmptySpace
//			temp5.transform.parent = playingObjectScript.gameObject.transform.parent;
//			temp5.transform.localPosition = playingObjectScript.gameObject.transform.localPosition;
			//Destroy (this.gameObject);
			playingObjectScript.myColumnScript.ChangeItem (playingObjectScript.indexInColumn, GameManager.instance.playingObjectPrefabs [num], this.gameObject.name);
			Destroy (this.gameObject);
		}
        if (GameManager.instance.isBusy)
            return;

        isTouched = true;
        SoundFxManager.instance.objectPickSound.Play();

        ObjectSelected();
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


    void OnMouseDrag()
    {
        if (!isTouched)
            return;

        Vector3 tempPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Vector2.Distance(transform.position, tempPosition) > GameManager.instance.gapBetweenObjects * .4f)
        {
            if (Mathf.Abs(tempPosition.x - transform.position.x) > Mathf.Abs(tempPosition.y - transform.position.y)) //left right moved
            {
                if (tempPosition.x > transform.position.x)
                {
                    if (playingObjectScript.adjacentItems[0] != null)
                        playingObjectScript.adjacentItems[0].GetComponent<PlayingObjectTouch>().ObjectSelected();
                }
                else
                {
                    if (playingObjectScript.adjacentItems[1] != null)
                        playingObjectScript.adjacentItems[1].GetComponent<PlayingObjectTouch>().ObjectSelected();
                }
            }
            else
            {
                if (tempPosition.y > transform.position.y)
                {
                    if (playingObjectScript.adjacentItems[2] != null)
                        playingObjectScript.adjacentItems[2].GetComponent<PlayingObjectTouch>().ObjectSelected();
                }
                else
                {
                    if (playingObjectScript.adjacentItems[3] != null)
                        playingObjectScript.adjacentItems[3].GetComponent<PlayingObjectTouch>().ObjectSelected();
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

    

   

