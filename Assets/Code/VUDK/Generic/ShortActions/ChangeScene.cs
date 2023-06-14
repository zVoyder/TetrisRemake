namespace VUDK.Generic.ShortActions
{
    using System.Collections;
    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class ChangeScene : ShortAction
    {
        public float time = 10;
        public string sceneToLoad;

        public override void TriggerShortAction()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
    }
}
