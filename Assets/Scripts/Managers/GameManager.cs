using UnityEngine;
using System;
public class GameManager : SingletonMonoBehaviour<GameManager>

{
    public bool is2D;
    public bool isPaused;
    [Header("Players")]
    public Player player2D;
    public Player player3D;
    [Header("Camera")]
    public Camera mainCamera;
    [Header("Spawn Points")]

    public Transform playerSpawnPoint;

    public Material outlineMaterial2D;
    public Material spriteDefaultMaterial2D;
    

    public Transform objectiveTransform;

    public event Action onSwitchDimension;
    void Update()
    {
        isPaused = Time.timeScale == 0;
    
    }

    public void StartGame(){
        PlayGame();
    }
    public void PlayGame(){
        //play game 
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        UIManager.instance.HideUI(UI.PAUSE);
    }
    public void PauseGame(){
        UIManager.instance.ShowUI(UI.PAUSE);
        Time.timeScale = 0;
    }
    public void SwitchDimension(){
        // transition between 2D and 3D
        is2D = !is2D;
        if(is2D){
            player2D.enabled = true;
            player3D.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            objectiveTransform.position = player2D.transform.position;
        }
        else{
            player2D.enabled = false;
            player3D.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            objectiveTransform.position = player3D.transform.position;
        }
        
    }
    public void OnSwitchDimension(){
        onSwitchDimension?.Invoke();
    }
    private void OnEnable() {
        onSwitchDimension += SwitchDimension;
    }
    private void OnDisable() {
        onSwitchDimension -= SwitchDimension;
    }
}
