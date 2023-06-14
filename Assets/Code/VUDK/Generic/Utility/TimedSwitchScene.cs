using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSwitchScene : MonoBehaviour
{
    public float Time = 10;
    public int BuildIndexSceneToLoad;

    public void ChangeScene()
    {
        StartCoroutine(LoadSceneIn());
    }

    private IEnumerator LoadSceneIn()
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(BuildIndexSceneToLoad, LoadSceneMode.Single);
    }
}
