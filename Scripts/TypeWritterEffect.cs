using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
public class TypeWritterEffect : MonoBehaviour
{
    public float delay;
    string fullText;
    public string currentText = "";
    public AudioSource[] keyboard;
    public int startTextX;
    public int endTextX;
    public int startTextY;
    public int endtTextY;
    public bool randomPos = false;
    public bool isPaused = false;
    public bool continueAfterPause = false;
    int k=0;
    int i;
    StreamReader streamReader = new StreamReader("Assets/text2.txt");

    System.Random rand =new System.Random();
    // Start is called before the first frame update
    void Start()
    {
       // alltext = streamReader.ReadToEnd();
       // lines = alltext.Split(new string[] {"\n"}, StringSplitOptions.None);
        keyboard = this.GetComponents<AudioSource>();
        //StartCoroutine(BossWriter());
    }
    public void RunTyping()
    {
        StartCoroutine(BossWriter());
    }
    IEnumerator BossWriter()
    {
        yield return new WaitForSeconds(0.2f);
        using (streamReader)
        {
            while ((fullText = streamReader.ReadLine()) != null)
            {
                if (continueAfterPause) continueAfterPause = false;
                this.GetComponent<Text>().text = "";
                //fullText = streamReader.ReadLine();
               // v++;
                // Debug.Log(fullText);
                if (fullText=="NEXT_SCENE")
                {
                    Debug.Log("Ended Scene");
                    SceneManager.LoadScene("EndScene");
                }
                yield return TheWriter(fullText);
            }
        }
    }

    IEnumerator TheWriter(string text)
    {
        int indexStartText = 0; //index  from where start writing text from the fullText (Current Line)
        if (isPaused) //Continuing the writing after the pause (updating the index)
        { 
            string part = text;
            text = fullText;
           // Debug.Log($" part = {part} | text= {text}");
            indexStartText = text.IndexOf(part) +1;
            isPaused = false;
            continueAfterPause = true;
        }
        for (i = indexStartText; i < text.Length; i++) 
        {
          //  Debug.Log($"Full= {fullText} + i = {i}");
            float randomVolume = (float)((rand.NextDouble() * 0.075) + 0.02); //Generating random volume from 0.02 to 0.095
            currentText = text.Substring(0, i); //Typing effect step 1 - holding the string in a variable and add char each time
            this.GetComponent<Text>().text = currentText; // step 2 - typing it
            if (text[i] == ' ') //Sound Control [space]
            {
                keyboard[5].volume = randomVolume;
                keyboard[5].Play();
            }
            else if (((text[i] == '.') || (text[i] == '!') || (text[i] == '?')) && (i == text.Length - 1)) //end of line
            {
                currentText = "";
                float randomWait = (float)((rand.NextDouble() * 0.5) + 0.7); //Generating random wait from 0.7 to 1.2
                int randomX = rand.Next(startTextX, endTextX);
                int randomY = rand.Next(startTextY, endtTextY);
                this.GetComponent<Text>().text += text[i];
                yield return new WaitForSeconds(randomWait); //Wait after the dot.
                this.GetComponent<Text>().text = "";
                if (randomPos) this.GetComponent<RectTransform>().localPosition = new Vector3(randomX, randomY, 0); // if randomPos them start giving random position to text
            }
            else if ((text[i]=='9')) // && (i < text.Length - 1))
            {
                currentText = "";
                float randomWait = (float)((rand.NextDouble() * 0.5) + 0.7); //Generating random wait from 0.7 to 1.2
                int randomX = rand.Next(startTextX, endTextX);
                int randomY = rand.Next(startTextY, endtTextY);
                this.GetComponent<Text>().text += "...";
                yield return new WaitForSeconds(randomWait); //Wait after the dot.
                this.GetComponent<Text>().text = "";
                if (randomPos) this.GetComponent<RectTransform>().localPosition = new Vector3(randomX, randomY, 0); // if randomPos them start giving random position to text
            }
            else //playing keyboard sound
            {
                keyboard[k].volume = randomVolume;
                keyboard[k].Play();
                k++; if (k == 5) k = 0;
            }
            yield return new WaitForSeconds(delay);
            
        }
        if (continueAfterPause)
            {
            yield return StartCoroutine(BossWriter());
            }
    }
    

    // Update is called once per frame
    private void Update()
    {
        //Switching from Random Position to Fixed
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (randomPos)
            {
                this.GetComponent<RectTransform>().localPosition = new Vector3(233, 3, 0);
                randomPos = false;
            }
            else
            {

                randomPos = true;
            }
        }
        //Pausing the typing
        if (Input.GetKeyDown(KeyCode.Space))    
        {
            if (isPaused)
            {
               // Debug.Log("Started again , fulltext= " + fullText + " CurrentTextAfter = " + fullText.Substring(i) + " i = " + i.ToString());
                StartCoroutine(TheWriter(fullText.Substring(i-1)));
                //isPaused = false;
                    }
            else
            {
                //Debug.Log($"Paused , full = {fullText} , current = {currentText} , i = {i}");
                StopAllCoroutines();
                isPaused=true;
            }
        }
    }
}
