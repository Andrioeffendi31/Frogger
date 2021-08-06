using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public static int score;
    public static int trophy1 = 0 , trophy2 = 0, trophy3 = 0, trophy4 = 0, trophy5 = 0;
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = score.ToString();
    }
}
