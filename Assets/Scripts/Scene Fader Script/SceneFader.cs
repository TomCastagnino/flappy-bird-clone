﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance;

    [SerializeField]
    private GameObject fadeCanvas;
    [SerializeField]
    private Animator fadeAnim;


	void Awake () {
        MakeSingleton();
	}
	
	void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn(string levelName)
    {
        StartCoroutine(FadeInAnimation(levelName));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimation());
    }

    IEnumerator FadeInAnimation(string levelName)
    {
        fadeCanvas.SetActive(true);
        fadeAnim.Play("FadeIn");
        yield return new WaitForSeconds(.7f);
        SceneManager.LoadScene(levelName);
        FadeOut();
    }

    IEnumerator FadeOutAnimation()
    {
        fadeAnim.Play("FadeOut");
        yield return new WaitForSeconds(1f);
        fadeCanvas.SetActive(false);
    }

}
