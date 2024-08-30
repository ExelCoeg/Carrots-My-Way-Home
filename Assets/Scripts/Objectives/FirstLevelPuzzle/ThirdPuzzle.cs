using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;
public class ThirdPuzzle : Objective
{   
    [SerializeField] public List<Question> questionsList;
    [SerializeField] private GameObject gate;
    [SerializeField] private Transform gateStartPos;
    [SerializeField] private Transform gateEndPoint;
        [Header("Reward")]
    [SerializeField] private GameObject reward;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    private bool completeQuestions;
    private bool spawned;
    private void Start() {
        foreach(var questions in questionsList){
            questions.available = false;
        }
    }

    protected override void Update()
    {
        base.Update();
        complete = completeQuestions && reward.TryGetComponent(out GoldenReward goldenReward) ? reward.GetComponent<GoldenReward>().interacted : false;
        completeQuestions = questionsList[0].correct && questionsList[1].correct && questionsList[2].correct && questionsList[3].correct;
    }
    public override void CheckComplete()
    {
        if(completeQuestions){
            OnComplete();
        }
        if(complete) enabled = false;
    }

    
    public IEnumerator OpenGate(){
        yield return reward.transform.DOMoveY(endPos.position.y,1f).WaitForCompletion();
        yield return gate.transform.DOMoveY(gateEndPoint.position.y,1f).WaitForCompletion();

    }
    public void SpawnReward(){
        if(!spawned){
            reward = Instantiate(reward,startPos.position,Quaternion.identity,transform);
            StartCoroutine(OpenGate());
        }
        spawned = true;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        onComplete += SpawnReward;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        onComplete -= SpawnReward;
    }
}
