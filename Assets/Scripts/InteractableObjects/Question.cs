using UnityEngine;
using UnityEngine.UI;

public class Question : InteractableObject
{
    
    public bool correct;
    public Sprite questionSprite;
    public override void Interacted()
    {
        UIQuestions.instance.GetComponent<Image>().sprite = questionSprite;
        UIManager.instance.ShowUI(UI.QUESTIONS);
    }

}
