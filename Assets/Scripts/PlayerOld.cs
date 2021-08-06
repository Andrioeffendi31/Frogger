using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOld : MonoBehaviour
{
    private static readonly float Cooldown = 0.25f;

    private bool isCoolingdown = false;
    private bool inRiver = false;
    public GameObject frog;
    public Animator froganim;
    public Bar timeBar;
    public GameObject Death, TimesUp;
    public GameObject finish1, finish2, finish3, finish4, finish5, jumpSFX, DeathSFX;
    public GameManager GameManager;
    public Transform SpawnPoint;

    public float maxTime;
    public float timeLeft;

    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MoveRowRight>().enabled = false;
        gameObject.GetComponent<MoveRowLeft>().enabled = false;

        if (Score.trophy1 == 0)
        {
            finish1.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Score.trophy1 >= 1)
        {
            finish1.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Score.trophy2 == 0)
        {
            finish2.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Score.trophy2 >= 1)
        {
            finish2.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Score.trophy3 == 0)
        {
            finish3.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Score.trophy3 >= 1)
        {
            finish3.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Score.trophy4 == 0)
        {
            finish4.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Score.trophy4 >= 1)
        {
            finish4.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (Score.trophy5 == 0)
        {
            finish5.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Score.trophy5 >= 1)
        {
            finish5.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        
        timeLeft = maxTime;
        timeBar.SetMaxTime(maxTime);
        TimesUp.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (timeLeft > 0)
        {
            timeLeft -= (float)0.05f;
            timeBar.SetTime(timeLeft);
        }
        else
        {
            TimesUp.SetActive(true);
            GameObject.Find("DeathSFX").GetComponent<AudioSource>().Play();
            Instantiate(Death, this.transform.position, this.transform.rotation);
            GameManager.Respawning();
            Destroy(this.gameObject);
            Health.health--;
            if (Health.health < 0)
            {
                Debug.Log("RIP");
                Score.score = 0;
                //GameOver

            }
        }
    }

    private void ResetTimer()
    {
        timeLeft = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolingdown)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        if (Mathf.Abs(y) > 0)
        {
            if (y > 0)
            {
                frog.transform.rotation = Quaternion.identity;
            }
            else
            {
                frog.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign(x) * 180));
            }
            StartCoroutine(Move(new Vector3(0, Mathf.Sign(y) * 16, 0)));
        }
        else if (Mathf.Abs(x) > 0)
        {
            frog.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign(x) * -90));

            StartCoroutine(Move(new Vector3(Mathf.Sign(x) * 16, 0, 0)));
        }

        if(finish1.gameObject.GetComponent<SpriteRenderer>().enabled == true &&
           finish2.gameObject.GetComponent<SpriteRenderer>().enabled == true &&
           finish3.gameObject.GetComponent<SpriteRenderer>().enabled == true &&
           finish4.gameObject.GetComponent<SpriteRenderer>().enabled == true &&
           finish5.gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            Score.trophy1 = 0;
            Score.trophy2 = 0;
            Score.trophy3 = 0;
            Score.trophy4 = 0;
            Score.trophy5 = 0;
            GameManager.Finish();
        }
    }

    private IEnumerator Move(Vector3 delta)
    {
        isCoolingdown = true;

        froganim.SetTrigger("move");
        GameObject.Find("jumpSFX").GetComponent<AudioSource>().Play();
        var start = transform.position;
        var end = start + delta;
        var time = 0f;
        while(time < 1.5f)
        {
            transform.position = Vector3.Lerp(start, end, time);
            time = time + Time.deltaTime / Cooldown;
            yield return null;
        }
        Score.score += 20;
        Debug.Log(Score.score);
        transform.position = end;
        isCoolingdown = false;
    }

    private void OnTriggerEnter2D(Collider2D floatingObj)
    {
        if (floatingObj.gameObject.tag == "floatingKanan")
        {
            Debug.Log("Ngambang");
            gameObject.GetComponent<MoveRowRight>().enabled = true;
        }

        if (floatingObj.gameObject.tag == "floatingKiri")
        {
            Debug.Log("Ngambang");
            gameObject.GetComponent<MoveRowLeft>().enabled = true;
        }

        if (floatingObj.gameObject.tag == "river")
        {
            if (Health.health >= 0)
            {
                GameObject.Find("DeathSFX").GetComponent<AudioSource>().Play();
                Instantiate(Death, this.transform.position, this.transform.rotation);
                GameManager.Respawning();
                Destroy(this.gameObject);
                Health.health--;
            }
            
            if (Health.health < 0)
            {
                Debug.Log("RIP");
                GameManager.GameOver2();
                //GameOver

            }
        }

        if (floatingObj.gameObject.tag == "borderSamping")
        {
            if(Health.health >= 0)
            {
                GameObject.Find("DeathSFX").GetComponent<AudioSource>().Play();
                Instantiate(Death, this.transform.position, this.transform.rotation);
                GameManager.Respawning();
                Destroy(this.gameObject);
                Health.health--;
            }
            
            if (Health.health < 0)
            {
                Debug.Log("RIP");
                GameManager.GameOver1();
                //GameOver

            }
        }

        if (floatingObj.gameObject.tag == "finish1")
        {
            finish1.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Score.trophy1++;
            GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
            GameManager.respawnTime = 2;
            GameManager.Respawning();
        }
        if (floatingObj.gameObject.tag == "finish2")
        {
            finish2.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Score.trophy2++;
            GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
            GameManager.respawnTime = 2;
            GameManager.Respawning();
        }
        if (floatingObj.gameObject.tag == "finish3")
        {
            finish3.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Score.trophy3++;
            GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
            GameManager.respawnTime = 2;
            GameManager.Respawning();
        }
        if (floatingObj.gameObject.tag == "finish4")
        {
            finish4.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.position = SpawnPoint.transform.position;
            Score.trophy4++;
            GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
            GameManager.respawnTime = 2;
            GameManager.Respawning();
        }
        if (floatingObj.gameObject.tag == "finish5")
        {
            finish5.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.position = SpawnPoint.transform.position;
            Score.trophy5++;
            GameObject.Find("CoinSFX").GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
            GameManager.respawnTime = 2;
            GameManager.Respawning(); 
        }

    }

    void OnTriggerExit2D(Collider2D floatingObj)
    {
        if (floatingObj.gameObject.tag == "floatingKanan")
        {
            gameObject.GetComponent<MoveRowRight>().enabled = false;
        }
        if (floatingObj.gameObject.tag == "floatingKiri")
        {
            gameObject.GetComponent<MoveRowLeft>().enabled = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Car")
        {
            GameObject.Find("DeathSFX").GetComponent<AudioSource>().Play();
            if (Health.health >= 0)
            {
                Instantiate(Death, this.transform.position, this.transform.rotation);
                Destroy(col.gameObject);
                GameManager.Respawning();
                Destroy(this.gameObject);
                Health.health--;
            }
            
            if (Health.health < 0)
            {
                //GameOver
                Destroy(col.gameObject);
                GameManager.GameOver1();

            }
        }
        if (col.gameObject.tag == "finish3")
        {
            this.transform.position = SpawnPoint.transform.position;
        }
        //else if (col.gameObject.tag == "borderBawah")
        //{
        //    Destroy(this.gameObject);
        //    GameManager.instance.Respawn();
        //}
    }

}
