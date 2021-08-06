using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Success : MonoBehaviour
{
    public GameManager GameManager;
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            if(Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
            else
            {
                GameManager.Home();
            }
            
        } 
        
        else if (Input.GetKey("return"))
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
            GameManager.Home();
        }
    }
}
