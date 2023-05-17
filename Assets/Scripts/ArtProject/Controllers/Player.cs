using ArtProject.Input;
using UnityEngine;

namespace ArtProject.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;

        private Rigidbody2D _rigidbody;
        private Controls _controls;
        private float _horizontalInput;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _controls = new Controls();
            _controls.Player.Enable();
        }

        private void Update()
        {
            _horizontalInput = _controls.Player.Walking.ReadValue<float>();
            JumpCheck();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            _rigidbody.velocity = new Vector2(moveSpeed * _horizontalInput, _rigidbody.velocity.y);
        }

        private void JumpCheck()
        {
            if (!_controls.Player.Jumping.WasPressedThisFrame()) return;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}