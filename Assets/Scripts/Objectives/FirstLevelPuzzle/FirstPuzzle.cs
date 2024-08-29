using UnityEngine;

public class FirstPuzzle : Objective
{
    public string correctAnswer;
    [SerializeField] private GameObject reward;
    [SerializeField] private Transform startRewardPosition;
    [SerializeField] private Transform endRewardPosition;
    private void Start() {
        UICodePanel.Instance.SetCorrectAnswer(correctAnswer);
    }
    public override void CheckComplete()
    {   
       
        if(complete){
            OnComplete();
            
        }
    }

    public override void OnEnable(){
        onComplete += SpawnReward;
    }

    public override void OnDisable()
    {
        onComplete -= SpawnReward;
    }

    public void SpawnReward(){
        GameObject rewardColor = Instantiate(reward,startRewardPosition.position,Quaternion.identity,transform);
        rewardColor.GetComponent<RewardColor>().Animate(endRewardPosition);
        enabled = false; 
    }


}
