using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject skor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "skor_akhir")
        {
            GetComponent<Text>().text = Score.score.ToString();
        }
        else if (gameObject.name == "highscore")
        {
            int HS = PlayerPrefs.GetInt("highscore");
            GetComponent<Text>().text = HS.ToString();
        }
    }
}
