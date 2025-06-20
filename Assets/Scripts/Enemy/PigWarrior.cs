using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PigWarrior : Pig
{
    [SerializeField] private float _leftConstraint;
    [SerializeField] private float _rightConstraint;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage; // Damage ini akan dilewatkan ke TakeDamage
    [SerializeField] private int _hitsPerSecond;

    // Mengubah _king menjadi _playerTransform
    private Transform _playerTransform; 
    
    // Asumsi: Player Anda memiliki komponen script bernama 'Health'
    private Health _playerHealthComponent; // <--- UBAH DI SINI: Health

    private float _startPositionX;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private int _currentDirection = -1;
    private bool _isFacingRight;
    private float _timeAfterLastHit;
    private bool _move = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _startPositionX = transform.position.x;

        // Mencari objek Health di scene.
        _playerHealthComponent = FindObjectOfType<Health>(); // <--- UBAH DI SINI: Health
        if (_playerHealthComponent != null)
        {
            _playerTransform = _playerHealthComponent.transform; // Menggunakan transform dari komponen Health
        }
        else
        {
            Debug.LogError("Health script (atau komponen 'Health') tidak ditemukan di scene! Pastikan GameObject Player Anda memiliki script Health."); // <--- UBAH DI SINI
            enabled = false; 
        }
    }

    private enum WarriorState 
    {
        Idle,
        Attack
    }

    private WarriorState _currentState = WarriorState.Idle;

    private void Update()
    {
        if (IsActive) 
        {
            if (_currentState == WarriorState.Idle)
            {
                GetPatrolDirection();
            }
            else if (_currentState == WarriorState.Attack)
            {
                GetPlayerDirection();
            }

            if (_move)
                Move(_currentDirection);
        }
    }

    private void GetPlayerDirection()
    {
        if (_playerTransform == null) return; 

        if (_playerTransform.position.x <= transform.position.x)
            _currentDirection = -1;
        else if (_playerTransform.position.x >= transform.position.x)
            _currentDirection = 1;
    }

    private void GetPatrolDirection()
    {
        float leftConstraintX = _startPositionX - _leftConstraint;
        float rightConstraintX = _startPositionX + _rightConstraint;

        if (transform.position.x < leftConstraintX)
            _currentDirection = 1;
        else if (transform.position.x > rightConstraintX)
            _currentDirection = -1;
    }

    private void Move(int direction)
    {
        if (_currentDirection < 0 && _isFacingRight)
            Flip();
        else if (_currentDirection > 0 && !_isFacingRight)
            Flip();

        _rigidBody.velocity = new Vector2(direction * _speed, _rigidBody.velocity.y);

        _animator.SetFloat("Speed", _rigidBody.velocity.sqrMagnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mencoba mendapatkan komponen Health dari objek yang bertabrakan
        if (collision.TryGetComponent<Health>(out Health playerHealth)) // <--- UBAH DI SINI: Health
        {
            _currentState = WarriorState.Attack;
            _move = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Mencoba mendapatkan komponen Health dari objek yang bertabrakan
        if (collision.TryGetComponent<Health>(out Health playerHealth)) // <--- UBAH DI SINI: Health
        {
            _timeAfterLastHit += Time.deltaTime;

            if (_timeAfterLastHit >= (1f / _hitsPerSecond))
            {
                playerHealth.TakeDamage(_damage); // <--- UBAH DI SINI: panggil TakeDamage dan gunakan _damage
                _timeAfterLastHit = 0;
            }

            _animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Mencoba mendapatkan komponen Health dari objek yang bertabrakan
        if (collision.TryGetComponent<Health>(out Health playerHealth)) // <--- UBAH DI SINI: Health
        {
            _animator.SetBool("Attack", false);
            _move = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PigWarrior>(out PigWarrior pig))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}