using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
public class UIShowMessage : UIBase
{
    public TextMeshProUGUI messageText;
    float timer;

    private void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                StartCoroutine(FadeOutMessage());
            }
        }
    }

    public void SetMessage(string message) {
        messageText.text = message;
        messageText.alpha = 1; // Ensure the text is fully visible
        TimerStart();
    }

    public void TimerStart() {
        timer = 2f;
    }

    public IEnumerator FadeOutMessage() {
        yield return messageText.DOFade(0, 1f).WaitForCompletion();
        messageText.text = "";
        
    }
}
