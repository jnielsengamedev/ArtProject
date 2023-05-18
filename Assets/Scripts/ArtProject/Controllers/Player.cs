using ArtProject.Data;
using ArtProject.Input;
using UnityEngine;

namespace ArtProject.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;
        private Animator _animator;
        private Controls _controls;
        private float _horizontalInput;
        private HorizontalDirection _direction = new(1);
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _controls = new Controls();
            _controls.Player.Enable();
        }

        private void Update()
        {
            _horizontalInput = _controls.Player.Walking.ReadValue<float>();

            FlipSprite();
            Animate();
            JumpCheck();
        }


        private void FixedUpdate()
        {
            Movement();
        }

        private void Animate()
        {
            _animator.SetBool(IsWalking, _horizontalInput is < 0 or > -0);
        }

        private void FlipSprite()
        {
            _direction = _horizontalInput switch
            {
                1 or -1 => new HorizontalDirection(_horizontalInput),
                _ => _direction
            };
            transform.localScale = transform.localScale.SetScaleDirection(_direction);
        }

        private void JumpCheck()
        {
            if (!_controls.Player.Jumping.WasPressedThisFrame()) return;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private void Movement()
        {
            _rigidbody.velocity = new Vector2(moveSpeed * _horizontalInput, _rigidbody.velocity.y);
        }
    }
}