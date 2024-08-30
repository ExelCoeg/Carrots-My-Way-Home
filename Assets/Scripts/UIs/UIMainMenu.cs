using UnityEngine.UI;
using UnityEngine;

public class UIMainMenu : UIBase
{
    public Button startButton;
    public Button aboutUsButton;
    public Button quitButton;
    public UIAboutUS uiAboutUS;
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.PlayMusic("bg");
        startButton.onClick.AddListener(()=>{
            SoundManager.Instance.PlaySound2D("clickUI");

            GameManager.instance.StartGame();
        });
        aboutUsButton.onClick.AddListener(()=>{
            SoundManager.Instance.PlaySound2D("clickUI");
            uiAboutUS.Show();
            
        });
        quitButton.onClick.AddListener(()=>{
            SoundManager.Instance.PlaySound2D("clickUI");

            GameManager.instance.QuitGame();
        });
    }
    private void Update() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
