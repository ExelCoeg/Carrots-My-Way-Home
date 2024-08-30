using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIMoral : UIBase
{
    public TextMeshProUGUI moralText;
    public static UIMoral instance;
    public override void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public override void Show()
    {
        base.Show();
        StartCoroutine(StartMoral());
    }
    // Start is called before the first frame update
    public IEnumerator StartMoral()
    {
        yield return GetComponent<Image>().DOFade(1,0.5f).WaitForCompletion();
        yield return moralText.DOFade(1,2).WaitForCompletion();
        yield return moralText.DOFade(0,2).WaitForCompletion();
;       GameManager.instance.MainMenu();
    }

    
}
