
public class CodePanel : InteractableObject
{
    protected override void Update()
    {
        base.Update();
    }
    public override void Interacted()
    {
        UIManager.instance.ShowUI(UI.CODEPANEL);
    }
}
