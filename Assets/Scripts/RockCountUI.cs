using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RockCountUI : MonoBehaviour
{
    public Text ScoreCountText;
    public int currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void ScoreDisplay(int scoreCount)
    {
        ScoreCountText.text = scoreCount.ToString();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void InstructionScene()
    {
        SceneManager.LoadScene(0);
    }
}
