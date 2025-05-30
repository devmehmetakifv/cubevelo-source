using UnityEngine;

public interface IPlayerState
{
    void HandleMovement(PlayerMovement player);
    void HandleJump(PlayerMovement player);
    void HandleDash(PlayerMovement player);
    void HandleInput(PlayerMovement player);
} 