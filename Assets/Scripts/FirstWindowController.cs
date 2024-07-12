using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FirstWindowController : MonoBehaviour
{
    public GameObject background;
    int picsLength;
    int step;
    public GameObject back2;
    public GameObject textOne;

    public GameObject textmove;
    // Start is called before the first frame update
    void Start()
    {
        step =0;
    }

   /* public IEnumerator showPictures()
    {

    }*/
    public void buttonReady()
    {
        if (step==0)
        {
            background.SetActive(false);
            back2.SetActive(true);
            textmove.SetActive(false);
            step++;
        }
        else {
            SceneManager.LoadScene("TheGame");
        }
    }
    // Update is called once per frame
    void Update()
    {
        textOne.transform.Translate(0,25*Time.deltaTime,0);
    }
}
