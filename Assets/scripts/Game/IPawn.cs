using UnityEngine;

public interface IPawn 
{
    public bool IsGrounded { get; }

    public void AddInput(float horizontalInput);
    public void AddInput(Vector2 input);
    public void Bounce(float amount);
}
