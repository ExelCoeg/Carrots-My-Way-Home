using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>

{
    public bool is2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        PlayGame();
    }
    public void PlayGame(){
        //play game 
    }
    public void ResumeGame(){
        Time.timeScale = 1;
    }
    public void PauseGame(){
        UIManager.instance.ShowUI(UI.PAUSE);
        Time.timeScale = 0;
    }
}
