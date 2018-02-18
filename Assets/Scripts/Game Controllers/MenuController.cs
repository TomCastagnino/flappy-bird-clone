using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;
    private bool isGreenBirdUnlocked = true, isRedBirdUnlocked = true;


	void Awake () {
        MakeInstance();
	}

    private void Start()
    {
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        CheckIfBirdsAreUnlocked();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void CheckIfBirdsAreUnlocked()
    {
        if (GameController.instance.IsGreenBirdUnlocked() == 1)
        {
            isGreenBirdUnlocked = true;
        }
        if (GameController.instance.IsRedBirdUnlocked() == 1)
        {
            isRedBirdUnlocked = true;
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeIn("Gameplay");
    }

    public void ChangeBird()
    {
        if (isGreenBirdUnlocked && isRedBirdUnlocked)
        {
            switch (GameController.instance.GetSelectedBird())
            {
                case 0:
                    birds[0].SetActive(false);
                    birds[1].SetActive(true);
                    birds[2].SetActive(false);
                    birds[3].SetActive(false);
                    GameController.instance.SetSelectedBird(1);
                    break;
                case 1:
                    birds[0].SetActive(false);
                    birds[1].SetActive(false);
                    birds[2].SetActive(true);
                    birds[3].SetActive(false);
                    GameController.instance.SetSelectedBird(2);
                    break;
                case 2:
                    birds[0].SetActive(false);
                    birds[1].SetActive(false);
                    birds[2].SetActive(false);
                    birds[3].SetActive(true);
                    GameController.instance.SetSelectedBird(3);
                    break;
                case 3:
                    birds[0].SetActive(true);
                    birds[1].SetActive(false);
                    birds[2].SetActive(false);
                    birds[3].SetActive(false);
                    GameController.instance.SetSelectedBird(0);
                    break;

            }
        }
        else if (isGreenBirdUnlocked)
        {
            switch (GameController.instance.GetSelectedBird())
            {
                case 0:
                    birds[0].SetActive(false);
                    birds[1].SetActive(true);
                    birds[2].SetActive(false);
                    GameController.instance.SetSelectedBird(1);
                    break;
                case 1:
                    birds[0].SetActive(true);
                    birds[1].SetActive(false);
                    birds[2].SetActive(false);
                    GameController.instance.SetSelectedBird(0);
                    break;
            }
        }
    }
}
