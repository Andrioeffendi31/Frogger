using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSpawner : MonoBehaviour
{
    public float maxTime = 1;
    public float timer = 0;
    public GameObject Baris;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newBaris = Instantiate(Baris);
            newBaris.transform.position = transform.position + new Vector3(0, 0, 0);
            Destroy(newBaris, 30);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
