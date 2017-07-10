using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{    
    public static GameManager instance;
    public GameObject[] playingObjectPrefabs;
	public GameObject[] playingObjectPrefabsTime;
    public GameObject[] horizontalPrefabs;
    public GameObject[] verticalPrefabs;
    public GameObject universalPlayingObjectPrefab;
	public GameObject signoPlayingObjectPrefab;
    public GameObject []jellyPrefab;

    internal int numberOfColumns;
    internal int numberOfRows;
    public float gapBetweenObjects = .7f;
    public float swappingTime = .8f;
    public float objectFallingDuration = .5f;
    internal float initialObjectFallingDuration;
    internal bool isBusy = false;
    public int totalNoOfJellies = 0;

    public iTween.EaseType objectfallingEase;

    internal TextMesh scoreText;
    internal TextMesh jellyText;
    int score;
	int temp;
	int bestScoreTemp;

	public GameObject NewBestScoreText;
	public TextMesh ScoreTextFinal;
	public TextMesh BestScoreTextFinal;

    internal static int numberOfItemsPoppedInaRow = 0;
    
	public static bool explotoAgua;

    void Awake()
    {
        instance = this;
    }

	void Start () 
    {
		bestScoreTemp = PlayerPrefs.GetInt ("BestScore");
        scoreText = GameObject.Find("Score Text").GetComponent<TextMesh>();
        jellyText = GameObject.Find("Jelly Text").GetComponent<TextMesh>();
        initialObjectFallingDuration = objectFallingDuration;
        numberOfColumns = LevelStructure.instance.numberOfColumns;
        numberOfRows = LevelStructure.instance.numberOfRows;
        numberOfItemsPoppedInaRow = 0;
        //scoreText.text = "Score : " + score.ToString();
		scoreText.text = score.ToString();
        jellyText.text = "Jelly : " + totalNoOfJellies.ToString();
	}

    internal void AddScore()
    {
		//ACA logica para sumar los puntos dependiendo el ambiente
		if (Tiempo.AguaBool) {
			if (explotoAgua) {
				temp = 100;
				explotoAgua = false;
			} else {
				temp = 50;
			}
		}
		//int temp = 2 * numberOfItemsPoppedInaRow; //* (numberOfItemsPoppedInaRow / 5 + 1);

      //  print(temp);
        score += temp;
        //scoreText.text = "Score : " + score.ToString();
		scoreText.text = score.ToString();
	 }

	void Update(){
		if (!Tiempo.Playing) {
			if (bestScoreTemp == 0) {
				PlayerPrefs.SetInt ("BestScore", score);
				NewBestScoreText.SetActive (true);
				BestScoreTextFinal.text = score.ToString ();
				ScoreTextFinal.text = score.ToString ();
			}
			if (score > bestScoreTemp) {
				PlayerPrefs.SetInt ("BestScore", score);
				NewBestScoreText.SetActive (true);
				BestScoreTextFinal.text = score.ToString ();
				ScoreTextFinal.text = score.ToString ();
			} else {
				ScoreTextFinal.text = score.ToString ();
				BestScoreTextFinal.text = bestScoreTemp.ToString ();
			}
		}
	}
}
