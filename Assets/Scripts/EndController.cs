using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class EndController : MonoBehaviour
{
    public GameObject[] pics;
    int count;
    public GameObject video;
    // Start is called before the first frame update
    void Start()
    {
        count=0;
        StartCoroutine(Switcher());
    }
    public IEnumerator Switcher()
    {
        while(count<4)
        {
        yield return new WaitForSeconds(13f);
        pics[count].SetActive(false);
        count++;
        pics[count].SetActive(true);
        }
        if (count==4)
        {
            video.GetComponent<VideoPlayer>().Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
