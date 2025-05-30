using UnityEngine;
using System.Collections;

public class NormalState : IPlayerState
{
    public void HandleMovement(PlayerMovement player)
    {
        // Original Move() logic
        player.transform.position = new Vector3(player.transform.position.x + player.HorizontalInput() * player.moveSpeed * Time.deltaTime, player.transform.position.y, player.transform.position.z);
    }

    public void HandleJump(PlayerMovement player)
    {
        // Original Jump() logic
        if (Input.GetButtonDown("Jump") && player.grounded)
        {
            SoundManager.PlaySound("JumpSound");
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, player.jumpForce), ForceMode2D.Impulse);
        }
    }

    public void HandleDash(PlayerMovement player)
    {
        // Original dash input logic
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canDash && (player.dashNumber > 0))
        {
            SoundManager.PlaySound("DashSound");
            player.StartCoroutine(player.DashCoroutine());
            player.dashNumber -= 1;
        }
    }

    public void HandleInput(PlayerMovement player)
    {
        HandleMovement(player);
        HandleJump(player);
        HandleDash(player);
        
        // Original Flip() logic
        if (player.isFacingRight && player.HorizontalInput() < 0f || !player.isFacingRight && player.HorizontalInput() > 0f)
        {
            Vector3 localScale = player.transform.localScale;
            player.isFacingRight = !player.isFacingRight;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
        }
    }
} 