using UnityEngine;

namespace _Project.Scripts.Features.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController2D : MonoBehaviour {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float deceleration = 15f;

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 12f;

        [SerializeField] private float coyoteTime = 0.1f;
        [SerializeField] private float jumpBufferTime = 0.1f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Dash Settings")]
        [SerializeField] private float dashSpeed = 15f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 0.5f;

        private Rigidbody2D _rigidbody;
        private bool _isGrounded;
        private float _horizontalInput;
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;

        private bool _isDashing;
        private float _dashTime;
        private float _lastDashTime;
        private Vector2 _dashDirection;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            HandleInput();
            HandleGroundCheck();
            HandleCoyoteAndBuffer();
            HandleJump();
            HandleDash();
        }

        private void FixedUpdate() {
            if (_isDashing) {
                _rigidbody.linearVelocity = _dashDirection * dashSpeed;
                return;
            }

            Move();
        }

        private void HandleInput() {
            _horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
                _jumpBufferCounter = jumpBufferTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                TryStartDash();
        }

        private void HandleGroundCheck() {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private void HandleCoyoteAndBuffer() {
            if (_isGrounded)
                _coyoteTimeCounter = coyoteTime;
            else
                _coyoteTimeCounter -= Time.deltaTime;

            if (_jumpBufferCounter > 0)
                _jumpBufferCounter -= Time.deltaTime;
        }

        private void Move() {
            float targetSpeed = _horizontalInput * moveSpeed;
            float speedDiff = targetSpeed - _rigidbody.linearVelocity.x;
            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
            float movement = speedDiff * accelRate;

            _rigidbody.AddForce(Vector2.right * movement);
        }

        private void HandleJump() {
            if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0) {
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, jumpForce);
                _jumpBufferCounter = 0;
            }

            if (Input.GetButtonUp("Jump") && _rigidbody.linearVelocity.y > 0)
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
        }

        private void TryStartDash() {
            if (Time.time < _lastDashTime + dashCooldown) return;
            if (_isDashing) return;

            _isDashing = true;
            _dashTime = Time.time;
            _lastDashTime = Time.time;
            _dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (_dashDirection == Vector2.zero)
                _dashDirection = new Vector2(transform.localScale.x, 0);

            _rigidbody.gravityScale = 0;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void HandleDash() {
            if (_isDashing && Time.time >= _dashTime + dashDuration) {
                _isDashing = false;
                _rigidbody.gravityScale = 1;
            }
        }

        private void OnDrawGizmosSelected() {
            if (groundCheck == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}