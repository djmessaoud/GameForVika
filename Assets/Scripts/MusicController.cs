using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicController : MonoBehaviour
{
    public TypeWritterEffect TyperScript;
    public AudioSource soundtrack;
    bool muted = false;
  public  CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine(increaseAlpha());
    }
    IEnumerator increaseAlpha()
    {
    //    yield return new WaitForSeconds(2f);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (muted)
            {
                this.GetComponent<AudioSource>().mute = false;
                muted = false;
            }
            else
            {
                this.GetComponent<AudioSource>().mute = true;
                muted = true;
            } 
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            soundtrack.volume += 0.002f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            soundtrack.volume -= 0.002f * Time.deltaTime;
        }
    }
    public void StartButton()
    {
        TyperScript.RunTyping();
       GameObject menu= GameObject.Find("Menu");
        menu.SetActive(false);
        this.GetComponent<AudioSource>().Play();
    }
}
