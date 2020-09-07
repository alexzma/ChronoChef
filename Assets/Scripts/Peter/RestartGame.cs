using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public SceneLoader sceneLoader;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    IEnumerator Start()
    {
       yield return StartCoroutine(ExampleRoutine(20.0f)); 
       sceneLoader.LoadScene("Alex_Start_Scene");
    }

    // Update is called once per frame
    IEnumerator ExampleRoutine(float waitTime)
    {
       yield return new WaitForSeconds(waitTime);
       
    }
}
