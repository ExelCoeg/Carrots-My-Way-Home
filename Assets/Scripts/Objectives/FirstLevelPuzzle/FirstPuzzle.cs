using UnityEngine;

public class FirstPuzzle : Objective
{
    public string correctAnswer;
    [SerializeField] private GameObject reward;
    [SerializeField] private Transform startRewardPosition;
    [SerializeField] private Transform endRewardPosition;
    [SerializeField] private CodePanel codePanel;
    private void Start() {
        UICodePanel.Instance.SetCorrectAnswer(correctAnswer);
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void CheckComplete()
    {   
       
        if(complete){
            OnComplete();
            codePanel.GetComponent<Collider2D>().enabled = false;
            codePanel.enabled =false;
        }
    }

    public override void OnEnable(){
        base.OnEnable();
        onComplete += SpawnRewardColor;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        onComplete -= SpawnRewardColor;
    }

    public void SpawnRewardColor(){
        GameObject rewardColor = Instantiate(reward,startRewardPosition.position,Quaternion.identity,transform);
        rewardColor.GetComponent<Reward>().Animate(endRewardPosition);
        enabled = false; 
    }


}
