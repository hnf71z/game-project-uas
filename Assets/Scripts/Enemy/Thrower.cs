using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Thrower : Pig
{
    [SerializeField] private float _secondsBetweenThrowing;
    [SerializeField] private Vector2 _throwDirection;
    [SerializeField] private float _randoDirectionXMin;
    [SerializeField] private float _randoDirectionXMax;

    private Animator _animator;
    private float _secondsAfterLastThrow;
    protected ItemHolder Items;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        FindItemPool();
    }

    private void Update()
    {
        if (IsActive)
        {
            _secondsAfterLastThrow += Time.deltaTime;

            if (_secondsAfterLastThrow >= _secondsBetweenThrowing)
            {
                _animator.SetTrigger("Throw");
                _secondsAfterLastThrow = 0;
            }
        }
    }

    private void ThrowItem()
    {
        GameObject item = Items.GetItem();

        float randomDirectionX = _throwDirection.x * Random.Range(1.2f, 1.7f);

        if (item != null)
        {
            item.transform.position = transform.position;
            item.SetActive(true);
            item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            item.GetComponent<Rigidbody2D>().AddForce(GetRandomizeThrowDirection(), ForceMode2D.Impulse);
        }
    }

    private Vector2 GetRandomizeThrowDirection()
    {
        float randomDirectionX = _throwDirection.x * Random.Range(_randoDirectionXMin, _randoDirectionXMax);
        return new Vector2(randomDirectionX, _throwDirection.y);
    }

    protected abstract void FindItemPool();
}
