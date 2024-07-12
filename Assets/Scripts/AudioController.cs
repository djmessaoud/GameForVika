using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public struct song {
   public AudioClip clip;
    public string name;
}
public class AudioController : MonoBehaviour
{

    public List<AudioClip> songs;
    int songsPlayed;
    public float waitTime;
    public Text writingField;
    AudioSource player;
    bool correctLetterPressed;
    public GameControl obstacleCcontroller;
    // Start is called before the first frame update
    void Start()
    {
        obstacleCcontroller = GetComponent<GameControl>();
        AudioClip[] clipsRandomized= songs.ToArray();
        System.Random rand = new System.Random();
        songs = clipsRandomized.OrderBy(x => rand.Next()).ToArray().ToList();
        player = GetComponent<AudioSource>();
        correctLetterPressed = false;
        StartCoroutine(startPlaying());
    }
    IEnumerator startPlaying()
    {
        int k = 0;
        int i = 0;
      /*  writingField.text = songs[k].name[i].ToString();
        player.clip = songs[k];
        player.Play();
        k++;*/
        while (k < songs.Count)
        {
            i = 0;
            player.clip = songs[k];
            player.Play();
            yield return new WaitForSeconds(waitTime);
            Debug.Log($"Song {songs[k].name} started");
            while ( i  < songs[k].name.Length)
            {
                correctLetterPressed = false;
                writingField.text = songs[k].name[i].ToString();
                yield return new WaitUntil(() => correctLetterPressed == true);
                i++;
            }
            k++;
            writingField.text = "?";
            if (k==songs.Count) k=0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rand = new System.Random();
        correctLetterPressed = false;
     //   if (Input.GetKeyDown(KeyCode.A)) keyPressed = KeyCode.A.ToString()[0];
     //Determining which key is pressed
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode)) //If a key is pressed
            {
                if (kcode.ToString().ToLower() == writingField.text) correctLetterPressed = true;
                //If space is pressed then clear all obstacles sometimes
                if (kcode == KeyCode.Space && writingField.text == " ")
                {
                    correctLetterPressed = true;
                   if(rand.Next()%2==0) obstacleCcontroller.DestroyAllObstacles();
                }
                StartCoroutine(waitABit());
                Debug.Log($"Key pressed = {kcode.ToString()}");
            }
        }
    }
    IEnumerator waitABit()
    {
        yield return new WaitForSeconds(0.5f);
        correctLetterPressed=false;
    }
}
