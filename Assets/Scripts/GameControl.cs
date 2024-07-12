using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControl : MonoBehaviour
{
    public bool gamePaused = false;
    public bool gameEnd = false;
    public float score = 0;
    public GameObject scoreText;
    public GameObject[] Obstacles;
    public Camera cam;
    public float cameraSpeed;
    public float gameSpeed;
    int obstaclesNumber;
    public int upperLine;
    public int lowerLine;
    List<float> closeObstacleRanges = new List<float>();
    float currentPositionX;
    float currentPositionY;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
         obstaclesNumber = Obstacles.Length;
        //Updating Close Obstacles Spawn Range at beginning
        closeObstacleRanges.Insert(0, currentPositionX - 120);
        closeObstacleRanges.Insert(1, currentPositionX + 300);
        closeObstacleRanges.Insert(2, currentPositionY - 150);
        closeObstacleRanges.Insert(3, currentPositionY + 160);
        //Creating fixed obstacles Coroutine
        StartCoroutine(CreateFixedObstacle());
    }
    
    // Update is called once per frame

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            gameSpeed+=0.05f;
        }
         if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            gameSpeed-=0.05f;
        }
        //Player in nature
        if (Player.GetComponent<PlayerScript>().inNature==true)
        {
            DestroyAllObstacles();
        }
        if (!gamePaused)
        {
        //Update score
        score += 0.05f * gameSpeed;
        scoreText.GetComponent<Text>().text = ((int)score).ToString();
        //Updating player's position and obstacles ranges   
        currentPositionX = Player.transform.position.x;
        currentPositionY = Player.transform.position.y;
        closeObstacleRanges.Clear();
        closeObstacleRanges.Insert(0,currentPositionX - 120);
        closeObstacleRanges.Insert(1, currentPositionX + 600);
        closeObstacleRanges.Insert(2, currentPositionY - 350);
        closeObstacleRanges.Insert(3, currentPositionY + 360);
        //Moving Camera with player
        cam.transform.Translate(1 * cameraSpeed * Time.deltaTime, 0, 0);
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CreateMovingObstacle());
        }*/

    }
    //Pause mechanism
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //Unpause and continue obstacle spawn
            if (gamePaused)
            { 
            gamePaused = false;
            StartCoroutine(CreateFixedObstacle());
            }
            else //Pause and destroy obstacles
            { gamePaused = true;
            DestroyAllObstacles();
            }
            
        }
    }
    IEnumerator CreateFixedObstacle()
    {
        while (!gamePaused && !gameEnd)
        {
            System.Random rand = new System.Random();
            // Random Obstable choice
             int randomObstacle = rand.Next(0, obstaclesNumber);
            //Giving X position for the obstacle (after the player)
            float randomX = rand.Next((int)currentPositionX + 50, (int)closeObstacleRanges[1]);
            // Picking a side (up or down) to spawn the obstacle and then giving Y position
            int side = rand.Next();
            float randomY;
            if (side % 2 == 0)
             randomY = rand.Next((int)currentPositionY , upperLine);
            else  randomY = rand.Next(lowerLine, (int) currentPositionY);
            //Spawning obstacle
            yield return new WaitForSeconds((float)(gameSpeed/2));
            GameObject obstacle = Obstacles[randomObstacle];
           GameObject spawnedObstacle= Instantiate(obstacle, new Vector3(randomX, randomY, 0), Player.transform.rotation);
            //Determining whether moving or fixed obstacle
            int _nMovingOrFixedObstacle = rand.Next() % 2;
           if (_nMovingOrFixedObstacle == 0) // Move obstacle to player if we got moving case
            {
                spawnedObstacle.transform.right = (Player.transform.position - spawnedObstacle.transform.position);
                spawnedObstacle.GetComponent<Rigidbody2D>().AddForce(spawnedObstacle.transform.right * gameSpeed);
                spawnedObstacle.GetComponent<ObstacleController>().setValue(true);
            }
            else
            {
                spawnedObstacle.transform.position=new Vector3(Player.transform.position.x+100,Player.transform.position.y+150,Player.transform.position.z);
                
            }
          /*  spawnedObstacle.transform.right = (Player.transform.position - spawnedObstacle.transform.position);
                spawnedObstacle.GetComponent<Rigidbody2D>().AddForce(spawnedObstacle.transform.right * gameSpeed);
                spawnedObstacle.GetComponent<ObstacleController>().setValue(true);*/
        
           //Destroying obstacle after some seconds
            // Destroy(spawnedObstacle, 2f);
            //Generating random time to wait and wait to spawn next obstacle
            yield return new WaitForSeconds(0.5f*(float)(1/gameSpeed));
        }
    }
    public void DestroyAllObstacles()
    {
        GameObject[] aliveObstacles = GameObject.FindGameObjectsWithTag("obstacle");
        Debug.Log($"Obstacles found = {aliveObstacles.Length}");
        foreach (GameObject obstacle in aliveObstacles)
        {
            Debug.Log($"name obstacle = {obstacle.name}");
            Destroy(obstacle);
        }
    }
}
