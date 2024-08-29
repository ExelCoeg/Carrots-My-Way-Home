using TMPro;
using UnityEngine;

public class PaperHint : InteractableObject
{
    UIFirstHint uiFirstHint;
    public string hint;
   
    protected override void Update() {
        base.Update();
        uiFirstHint = GetComponentInParent<FirstLevelPuzzle>().uiFirstHint;
        if(uiFirstHint.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)){
            uiFirstHint.Hide();
        }
        uiFirstHint.GetComponentInChildren<TextMeshProUGUI>().text = hint;
    }
    public override void Interacted()
    {
        UIManager.instance.ShowUI(UI.FIRSTHINT);
    }
  
    
}
