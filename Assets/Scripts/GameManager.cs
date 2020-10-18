using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject loseText, winText;
    bool canRestart = false;

    int numArtifacts = 0;
    private const int GOALARTIFACTS = 4;

    // Start is called before the first frame update
    void Start()
    {
        loseText.SetActive(false);
        winText.SetActive(false);
        numArtifacts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRestart)
        {
            if (Input.GetKeyDown("R"))
                SceneManager.LoadScene("Menu");
        }

        if (numArtifacts >= GOALARTIFACTS)
            Win();
    }

    public void Lose()
    {
        loseText.SetActive(true);
        canRestart = true;
    }

    public void Win()
    {
        winText.SetActive(true);
        canRestart = true;
    }
}
