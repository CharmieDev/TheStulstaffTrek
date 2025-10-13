using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class starbuttonscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    //AudioSource buttonaudio;
    public void playbutton()
    {

        //GetComponent<AudioSource>().Play();

        SceneManager.LoadScene("Between");

    }

    public void endbutton()
    {

        Application.Quit();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
