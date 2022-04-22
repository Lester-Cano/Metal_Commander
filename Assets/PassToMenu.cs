using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassToMenu : MonoBehaviour
{

    float timer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        Debug.Log(timer);

        if (timer >= 18 || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
