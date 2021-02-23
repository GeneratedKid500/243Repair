using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    AudioSource aS;

    public CanvasGroup mattGroup;
    public CanvasGroup mattText;

    public CanvasGroup protagGroup;
    public CanvasGroup protagTopText;
    public CanvasGroup protagBottomText;

    public CanvasGroup pressToBegin;

    public float fadeInTime = 0.001f;

    public float blinkTime = 0.5f;
    float timer = 0;

    bool mattState = true;
    bool transitionalState = false;
    bool protagState = false;

    void Start()
    {
        aS = GetComponent<AudioSource>();

        mattGroup.enabled = true;
        mattText.enabled = true;
        protagGroup.enabled = true;
        protagTopText.enabled = true;
        protagBottomText.enabled = true;
        pressToBegin.enabled = true;

        mattGroup.alpha = 0;
        mattText.alpha = 0;
        protagGroup.alpha = 0;
        protagTopText.alpha = 0;
        protagBottomText.alpha = 0;
        pressToBegin.alpha = 1;
    }

    void Update()
    {
        //SceneManager.LoadSceneAsync("LevelScene");

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.X))
        {
            SceneManager.LoadScene("LevelScene");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (mattState)
            {
                if (mattText.alpha < 1)
                {
                    mattGroup.alpha = 1;
                    mattText.alpha = 1;
                }
                else
                {
                    mattState = false;
                    transitionalState = true;
                }
            }
            else if (transitionalState)
            {
                mattGroup.alpha = 0;
                mattText.alpha = 0;

                transitionalState = false; 
                protagState = true;
            }
            else if (protagState)
            {
                if (protagGroup.alpha < 1)
                {
                    protagGroup.alpha = 1;
                }
                else if (protagTopText.alpha < 1)
                {
                    protagTopText.alpha = 1;
                }
                else if (protagBottomText.alpha < 1)
                {
                    protagBottomText.alpha = 1;
                }
                else
                {
                    protagState = false;
                }
            }
            else
            {
                if (pressToBegin.transform.position != new Vector3(0, 0, 0))
                {
                    pressToBegin.transform.position = new Vector3(0, 0, 0);
                    pressToBegin.GetComponent<Text>().text = "Press X to begin...";
                }

                SceneManager.LoadScene("LevelScene");
            }
        }

        if (mattState)
        {
            if (mattText.alpha < 1)
            {
                if (mattGroup.alpha < 1)
                {
                    mattGroup.alpha += fadeInTime;
                }
                else
                {
                    mattText.alpha += fadeInTime;
                }
            }
            else
            {
                pressToBegin.GetComponent<Text>().text = "Press X to continue...";
            }

        }
        else if (transitionalState)
        {
            if (mattText.alpha > 0)
            {
                pressToBegin.GetComponent<Text>().text = "Press X to skip...";
                mattGroup.alpha -= fadeInTime;
                mattText.alpha -= fadeInTime;
            }
            else
            {
                transitionalState = false;
                protagState = true;
            }
        }
        else if (protagState)
        {
            if (protagBottomText.alpha < 1)
            {
                if (protagTopText.alpha < 1)
                {
                    if (protagGroup.alpha < 1)
                    {
                        pressToBegin.GetComponent<Text>().text = "Press X to skip...";
                        protagGroup.alpha += fadeInTime;
                    }
                    else
                    {
                        protagTopText.alpha += fadeInTime;
                    }
                }
                else
                {
                    protagBottomText.alpha += fadeInTime;
                }
            }
            else
            {
                pressToBegin.GetComponent<Text>().text = "Press X to continue...";
            }
        }
        else if (!protagState && !mattState && !transitionalState)
        {
            if (protagBottomText.alpha > 0)
            {
                if (protagTopText.alpha > 0)
                {
                    if (protagGroup.alpha > 0)
                    {
                        protagGroup.alpha -= fadeInTime;
                    }
                    else
                    {
                        protagTopText.alpha -= fadeInTime;
                    }
                }
                else
                {
                    protagBottomText.alpha -= fadeInTime;
                }
            }
            else if (protagBottomText.alpha <= 0)
            {
                pressToBegin.transform.position = new Vector3(0, 0, 0);
                pressToBegin.GetComponent<Text>().text = "Press X to begin...";
            }
        }
    }

    void LateUpdate()
    {
        timer += Time.deltaTime;   
        if (timer >= blinkTime)
        {
            pressToBegin.alpha = pressToBegin.alpha==0 ? 1 : 0;
            timer = 0f;
        }
    }
}
