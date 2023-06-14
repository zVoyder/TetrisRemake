namespace VUDK.Generic.ShortActions
{
    using UnityEngine;
    public class ActivateGameObject : ShortAction
    {
        public GameObject gameObjectToEnable;

        public override void TriggerShortAction()
        {
            gameObjectToEnable.SetActive(true);
        }
    }
}