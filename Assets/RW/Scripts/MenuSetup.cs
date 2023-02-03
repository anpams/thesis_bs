using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuSetup : MonoBehaviour
{
    public GameObject[] sabers;
    public GameObject gameManager;
    public GameObject highScoreText;
    public GameObject scoreText;
    private Vector3 leftStart;
    private Vector3 rightStart;

    private void Awake()
    {
        leftStart = sabers[0].transform.position;
        rightStart = sabers[1].transform.position;
    }

    private void SetSaberLocation(GameObject saber, Vector3 position)
    {
        if (saber){
            saber.transform.SetParent(null);
            saber.transform.position= transform.position;
            saber.transform.localPosition = position;
            saber.transform.localRotation= Quaternion.identity;
        }
    }

    public void OnEnable()
    {
        SetSaberLocation(sabers[0], leftStart);
        SetSaberLocation(sabers[1], rightStart);

        SetScores();
    }

    public void SetScores()
    {
        TextMeshProUGUI highscore = highScoreText.GetComponent<TextMeshProUGUI>();
        highscore.text= gameManager.GetComponent<GameManager>().highScore.ToString();

        TextMeshProUGUI score= scoreText.GetComponent<TextMeshProUGUI>();
        score.text = gameManager.GetComponent<GameManager>().score.ToString();
    }
}
