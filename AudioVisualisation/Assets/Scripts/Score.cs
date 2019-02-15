using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public int Max_Score;
    public int cmb = 0;
    [SerializeField] private Text txtMaxScore;
    [SerializeField] private Text txt;
    [SerializeField] private Text combo;
    [SerializeField] private PlayerController player;

	
    
	void Start ()
    {
        txt.text = "Score: " + player.score;
        Max_Score = PlayerPrefs.GetInt ("MaxScore");
        txtMaxScore.text = "MaxScore: " + Max_Score;
        combo.enabled = false;
    }
	
	
	void Update ()
    {

        if (player.score <= 0)
        {
            player.score = 0;
        }
        if (cmb == 3)
        {
            player.score += 10;
            cmb = 0;
        }

		if (Max_Score < player.score) {
			Max_Score = player.score;
			PlayerPrefs.SetInt ("MaxScore", Max_Score);
		}
		txt.text = "Score: " + player.score;
        txtMaxScore.text = "MaxScore: " + Max_Score;
        if (cmb > 0)
        {
            combo.enabled = true;
            combo.text = "Sphere x" + cmb;
        } else if (cmb < 1)
        {
            combo.enabled = false;
        }

	}
}
