using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Hit obstacle = -[10, 20] score
//Hit score = +[10,20] score


public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Camera cam;
    public GameObject GameController;
    GameControl gameControl;
    bool gameOver = false;
    public float upperLine;
    public float lowerLine;
    public string passSceneName;
public bool inNature = false;
    public GameObject DialogBox;
    // Start is called before the first frame update
    IEnumerator endGame()
    {
        gameControl.gamePaused = true;
        //Show dialog
        DialogBox.SetActive(true);
        yield return new WaitForSeconds(5f);
        //Move to password scene
         SceneManager.LoadScene(passSceneName);
    }

 IEnumerator increaseSpeed()
    {
        speed *= 2;
        yield return new WaitForSeconds(5f);
        speed = (float)(speed / 2);
    }
    void Start()
    {
        gameControl = GameController.GetComponent<GameControl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //End game
       if (collision.gameObject.tag =="Lion")
        {
            //Ask Lion King Question and wait for secret word
            gameOver = true;
            StartCoroutine(endGame());
        }
       //Collision with obstacles
       if (collision.gameObject.tag=="obstacle")
        {
            System.Random rand = new System.Random();
            float _toAdd = (float)(rand.NextDouble() * (20 - 10) + 10);
            gameControl.score -= _toAdd;
            Debug.Log($"Added = {_toAdd}");
        }    
        if (collision.gameObject.name=="Nature")
        {
            inNature=true;
        }    
        
        }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name    == "Line")
        {
            this.gameObject.transform.Translate(0,2* speed * Time.deltaTime, 0);
        }
        if (collision.gameObject.name == "Line1")
        {
            this.gameObject.transform.Translate(0, -2 * speed * Time.deltaTime, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
       //Movement control
        if (Input.GetKey(KeyCode.UpArrow))
        {
             if(!gameOver)  this.transform.Translate(-1 * speed * Time.deltaTime, 0,0);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!gameOver) this.transform.Translate (0, -1 * speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // if (!collidedWithLine) 
            if (!gameOver) this.transform.Translate(0, 1 * speed * Time.deltaTime, 0);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!gameOver) this.transform.Translate(1 * speed * Time.deltaTime, 0, 0);
        }
        //Setting borders
        if (this.transform.position.y > upperLine)
            this.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, lowerLine, 0), this.transform.rotation);
        if (this.transform.position.y < lowerLine)
            this.transform.SetPositionAndRotation(new Vector3(this.transform.position.x, upperLine, 0), this.transform.rotation);

    }
  
}
