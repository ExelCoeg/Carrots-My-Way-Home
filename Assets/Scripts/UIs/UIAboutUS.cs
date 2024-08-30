using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboutUS : UIBase
{
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(()=>{
            SoundManager.Instance.PlaySound2D("clickUI");
            Hide();
        });
    }

}
