public interface IState
{
    public void Enter(Character onwer);
    public void Update();
    public void FixedUpdate();
    public void LateUpdate();
    public void Exit();
}