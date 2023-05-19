using ArtProject.Data;
using UnityEngine;

namespace ArtProject.Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundManager : MonoBehaviour
    {
        private Collider2D _collider;
        private Player currentPlayer;
        private static readonly int IsJumping = Animator.StringToHash("isJumping");

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.TryGetComponent<Player>(out var player)) return;

            player.OffGroundType = null;
            currentPlayer = player;
            // Subscribe to the Player's jumpSignal.
            currentPlayer.jumpSignal += OnPlayerJump;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.collider.TryGetComponent<Player>(out var player) || !currentPlayer.Equals(player)) return;
            
            if (currentPlayer.OffGroundType != OffGroundType.IsJumping)
                currentPlayer.OffGroundType = OffGroundType.IsFalling;
            // Unsubscribe from the Player's jumpSignal
            currentPlayer.jumpSignal -= OnPlayerJump;
            currentPlayer = null;
        }

        private void OnPlayerJump(Player player)
        {
            currentPlayer.OffGroundType = OffGroundType.IsJumping;
            player.Animator.SetTrigger(IsJumping);
        }
    }
}