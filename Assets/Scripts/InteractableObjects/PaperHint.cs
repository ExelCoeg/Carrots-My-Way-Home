using TMPro;
using UnityEngine;

public class PaperHint : InteractableObject
{
    UIFirstHint uiFirstHint;
   
    public void Update() {
        
        uiFirstHint = GetComponentInParent<FirstLevelPuzzle>().uiFirstHint;
        if(uiFirstHint.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)){
            UIManager.instance.HideUI(UI.FIRSTHINT);
        }
    }
    public override void Interacted()
    {
        SoundManager.Instance.PlaySound2D("paper");
        UIManager.instance.ShowUI(UI.FIRSTHINT);
    }
  
    
}
