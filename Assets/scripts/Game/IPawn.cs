using UnityEngine;

public interface IPawn 
{
    public bool IsGrounded { get; }

    public void AddInput(float horizontalInput);
    public void Bounce(float amount);
}
