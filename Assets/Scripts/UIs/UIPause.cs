using UnityEngine.UI;

public class UIPause : UIBase
{

    public Button resumeButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(()=>{
            SoundManager.Instance.PlaySound2D("clickUI");

            GameManager.instance.ResumeGame();
        });

        quitButton.onClick.AddListener(()=>{
            SoundManager.Instance.PlaySound2D("clickUI");

            GameManager.instance.QuitGame();
        });
    }
}
