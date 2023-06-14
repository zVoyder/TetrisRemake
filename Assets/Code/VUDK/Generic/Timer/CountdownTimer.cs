namespace VUDK.Generic.Timer
{
    using System.Collections;
    using UnityEngine;
    using TMPro;

    public class CountdownTimer : MonoBehaviour
    {
        [field: SerializeField, Header("Timer"), TextArea(3, 10)]
        public string Text { get; private set; }

        [field: SerializeField]
        public TMP_Text TimerText { get; private set; }

        public void StartTimer(int time)
        {
            StartCoroutine(CountdownRoutine(time));
        }

        private IEnumerator CountdownRoutine(int time)
        {
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                TimerText.text = Text + time.ToString();
                time--;
            }

            SendMessage("TriggerShortAction", SendMessageOptions.DontRequireReceiver);
        }
    }
}
