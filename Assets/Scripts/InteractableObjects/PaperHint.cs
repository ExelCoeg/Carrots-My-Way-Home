using TMPro;
using UnityEngine;

public class PaperHint : InteractableObject
{
    UIFirstHint uiFirstHint;
    public string hint;
    private void Start() {
        
    }
    private void Update() {
        uiFirstHint = GetComponentInParent<FirstPuzzle>().uiFirstHint;
        if(uiFirstHint.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape)){
            uiFirstHint.Hide();
        }
        uiFirstHint.GetComponentInChildren<TextMeshProUGUI>().text = hint;
    }
    public override void Interactted()
    {
        uiFirstHint.Show();
    }
  
    
}
