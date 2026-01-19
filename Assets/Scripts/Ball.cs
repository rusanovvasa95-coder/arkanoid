using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour, IPointerDownHandler
{
    public float JumpForce;

    private Rigidbody2D _rigidbody;
    private Vector3 _reflectedDirection;
    private Vector3 _startBallPosition;
    private bool _ballOnPlatform;
    private bool _launchAgain;
    private Platform _platform;
    private Vector3 _savedDirection;

    private void Awake()
    {
        _ballOnPlatform = true;
        _launchAgain = true;
        _startBallPosition = transform.position;

        _rigidbody = GetComponent<Rigidbody2D>();
        _platform = FindFirstObjectByType<Platform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionPoint = collision.contacts[0].normal;
        _rigidbody.linearVelocity = Vector3.Reflect(_reflectedDirection, collisionPoint).normalized * JumpForce;
        _savedDirection = _rigidbody.linearVelocity;

        CorrectBallVelocity();
    }

    private void Update()
    {
        if (_ballOnPlatform)
        {
            transform.position =
                new Vector3(_platform.transform.position.x, _startBallPosition.y);
        }

        if (_rigidbody.IsSleeping() && !_ballOnPlatform && _launchAgain)
        {
            StartCoroutine(ReturnBallToMoveAfterStuck());
        }
    }

    private IEnumerator ReturnBallToMoveAfterStuck()
    {
        _launchAgain = false;
        yield return new WaitForSeconds(2f);
        _rigidbody.linearVelocity = _savedDirection;
        _launchAgain = true;
    }

    private void OnMouseDown()
    {
        if (!_ballOnPlatform)
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {        
        _reflectedDirection = _rigidbody.linearVelocity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_ballOnPlatform)
        {
            _rigidbody.linearVelocity = (Vector3.up + new Vector3(Random.Range(-1, 2), 0)) * JumpForce;
            _savedDirection = _rigidbody.linearVelocity;
            _ballOnPlatform = false;
        }
    }

    private void CorrectBallVelocity() 
    {
        float minYVelocity = 1f;
        Vector3 ballVelocityY = _rigidbody.linearVelocity;
        if (Mathf.Abs(ballVelocityY.y) < minYVelocity)
        {
            ballVelocityY.y = ballVelocityY.y > 0 ? minYVelocity : -minYVelocity;
            _rigidbody.linearVelocity = ballVelocityY.normalized * JumpForce;
        }

        float minXVelocity = 1f;
        Vector4 ballVelocityX = _rigidbody.linearVelocity;
        if (Mathf.Abs(ballVelocityX.x) < minXVelocity)
        {
            ballVelocityX.x = ballVelocityX.x > 0 ? minXVelocity : -minXVelocity;
            _rigidbody.linearVelocity = ballVelocityX.normalized * JumpForce;
        }
    }
}

