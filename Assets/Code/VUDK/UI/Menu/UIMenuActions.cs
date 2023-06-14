namespace VUDK.UI.Menu
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class UIMenuActions : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}