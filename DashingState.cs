using UnityEngine;

public class DashingState : IPlayerState
{
    public void HandleMovement(PlayerMovement player)
    {
        // No movement during dash - original logic from FixedUpdate when isDashing
        return;
    }

    public void HandleJump(PlayerMovement player)
    {
        // No jumping during dash
        return;
    }

    public void HandleDash(PlayerMovement player)
    {
        // No additional dashing during dash
        return;
    }

    public void HandleInput(PlayerMovement player)
    {
        // During dash, player has limited control - matches original isDashing logic
        HandleMovement(player);
        HandleJump(player);
        HandleDash(player);
    }
} 