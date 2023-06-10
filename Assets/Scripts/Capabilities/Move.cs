using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 5f)] private float _defaultDashCooldown = 1f;
    [SerializeField, Range(0f, 10f)] private float _dashDistance = 2f;

    private Controller _controller;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private CollisionDataRetriever _collisionDataRetriever;
    private WallInteractor _wallInteractor;

    private float _maxSpeedChange, _acceleration, _dashCooldown;
    private bool _onGround, _desiredDash;

    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
        _controller = GetComponent<Controller>();
        _wallInteractor = GetComponent<WallInteractor>();
        _dashCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _direction.x = _controller.input.RetrieveMoveInput(this.gameObject);
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _collisionDataRetriever.GetFriction(), 0f);
        _desiredDash |= _controller.input.RetrieveDashInput(this.gameObject);
    }

    private void FixedUpdate()
    {
        _onGround = _collisionDataRetriever.GetOnGround();
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;

        if (_desiredDash && _dashCooldown <= 0)
        {
            _desiredDash = true;
            _dashCooldown = _defaultDashCooldown;

            _velocity.x += _dashDistance * gameObject.transform.position.x;
        }
        else if (_dashCooldown > 0)
        {
            _dashCooldown -= Time.deltaTime;
        }
    }
}
