using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public CanvasGroup pressToExit;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Application.Quit();
        }
    }
    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            pressToExit.alpha = pressToExit.alpha == 0 ? 1 : 0;
            timer = 0f;
        }
    }
}
