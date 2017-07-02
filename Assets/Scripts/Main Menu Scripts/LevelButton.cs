using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour {

    public int levelNo;

	void Start () 
    {
        transform.Find("Text").GetComponent<TextMesh>().text = name;
	
	}

    void OnMouseDown()
    {
        Application.LoadLevel(int.Parse(name));
    }
}
