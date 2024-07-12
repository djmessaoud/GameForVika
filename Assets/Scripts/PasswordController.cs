using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PasswordController : MonoBehaviour
{
    public Text passwordField;
    public GameObject successFields;
    public GameObject[] hints;
    public AudioSource legendary;
    int hintsCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator goToScene()
    {
        legendary.Play();
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("TappingText");
    }
    public void submitClick()
    {
        if (passwordField.text=="existential crisis" || passwordField.text=="existentialcrisis")
        {
            Debug.Log("Success");
            successFields.SetActive(true);
            StartCoroutine(goToScene());
        }
    }
    public void hintClick()
    {
        if(hintsCounter<3)
        {
        hints[hintsCounter].SetActive(true);
        hintsCounter++;
        }
    }
}
