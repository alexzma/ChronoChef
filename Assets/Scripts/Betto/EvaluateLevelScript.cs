using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvaluateLevelScript : MonoBehaviour
{
    [SerializeField] private IngredientTracker ingredients;
    [SerializeField] private float targetPercentage;
    [SerializeField] private string winScene;
    [SerializeField] private string lossScene;

    public void EvaluateScore()
    {
        if (ingredients.CalculatePercentageDone() >= targetPercentage)
        {
            SceneManager.LoadScene(winScene);
        } else
        {
            SceneManager.LoadScene(lossScene);
        }
    }
}
