using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Obstacles : 
// Type 1 : Static rotating in place
// Type 2 : Moving linear towards player
// Type 3 : Dynamic moving towards player (circular rotation)

// Math

// Hit obstacle = -[10, 20] score
//Hit score = +[10,20] score
public class ObstacleController : MonoBehaviour
{
    public GameObject Player;
    System.Random rand;
    float playerSpeed;
    int _randomNumber;
     bool _MovingObstacle;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
         playerSpeed = Player.GetComponent<PlayerScript>().speed;
        rand = new System.Random();
        _randomNumber = rand.Next() % 2;
        _MovingObstacle = false;
    }
    public void setValue(bool x)
    {
        _MovingObstacle = x;
    }
 
    // Update is called once per frame
    void Update()
    {
        //Generating whether obstacle is moving linear or tricky towards Player

        if ((_randomNumber == 0) && (!_MovingObstacle)) ; // Obstacle type 3 - moving dynamic
                    this.transform.Translate(Vector2.right * Time.deltaTime * (float)(playerSpeed / 2));
       if ((!_MovingObstacle) && (_randomNumber==0)) this.transform.Rotate(0,0,1 * Time.deltaTime); // Rotate obstacle type 3 and 1
        if (this.transform.position.x < Player.transform.position.x-70)
            Destroy(this.gameObject);
        
    }

}
