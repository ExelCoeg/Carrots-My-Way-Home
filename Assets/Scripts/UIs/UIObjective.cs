using TMPro;
using UnityEngine;

public class UIObjective : UIBase
{
    public static UIObjective instance;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI objectiveMainText;
    public Objective objective;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this);
        }
    }
}
