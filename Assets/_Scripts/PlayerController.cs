using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private MeshRenderer _meshRenderer;

    private PlayerCondition _playerCondition;
    public PlayerCondition PlayerConditions => _playerCondition;

    public float _playerSpeed = 10f;

    //private bool _isMoving = false;

    private Vector3 _movingDirection;
    private Vector3 _nextCollisionPosition;

    private float _minSwipeRecognition = 600f;
    //private float _maxSwipeRecognition = 1000f;

    private Vector2 _currentSwipe;
    private Vector2 _lastSwipeFramePos;
    private Vector2 _currentSwipeFramePos;

    private Color solveColor;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _playerCondition = new PlayerCondition(); 
    }
    private void Start()
    {
        solveColor = Random.ColorHSV(0f, 1f, 2f, 5f);
        _meshRenderer.material.color = solveColor;
        _playerCondition.ResetConditions(); 
    }

    private void FixedUpdate()
    {
        #region MOVING REGION
        // apply movement only when the ball should be moving. 
        if (_playerCondition.IsMoving)
        {

            _playerRb.velocity = _playerSpeed * _movingDirection;
        }
        if (_nextCollisionPosition != Vector3.zero)
        {
            // if the distance between the player and the next collision is minimal,that is less than 1, we should stop moving. 
            if (Vector3.Distance(transform.position, _nextCollisionPosition) < 1)
            {
                _nextCollisionPosition = Vector3.zero; // we reset the _nextCollision position. 
                _movingDirection = Vector3.zero; // with no direction to move, the player will stand still. 
                _playerCondition.IsMoving = false; // player is not traveling thus. 
            }
        }

        if (!_playerCondition.IsMoving)
        {
            if (Input.GetMouseButton(0))
            {
                _currentSwipeFramePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if (_lastSwipeFramePos != Vector2.zero) // this is initialization from origin, that is top left side ofscreen. 
                {
                    // remember, check our notes. 
                    // if you want to get the direction we should go
                    // subtract what should be making that direction last (the right side of the operation) 
                    // in this case, we want to get which direction our lastswipe frame should be. 
                    // check illustration on our notebook to be clear, or use new illustrations to learn. 
                    _currentSwipe = _currentSwipeFramePos - _lastSwipeFramePos;
                    if (_currentSwipe.sqrMagnitude < _minSwipeRecognition)
                        return;
                    //otherwise continue. 
                    _currentSwipe.Normalize();

                    // UP/DOWN Swipes 
                    if (_currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                    {
                        //print("Should call swipte");
                        SetDestination(_currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                    }
                    // LEFT/RIGHT Swipes
                    if (_currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                    {
                        SetDestination(_currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                    }
                }
                _lastSwipeFramePos = _currentSwipeFramePos;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _currentSwipe = Vector2.zero;
            _lastSwipeFramePos = Vector2.zero;
        }
        #endregion`

        #region COLOR GROUND REGION
        // get the colliders our ball passes over.
        var groundNavigated = Physics.OverlapSphere(transform.position - (Vector3.up / 2f), .05f);
        foreach (var ground in groundNavigated)
        {
            var groundTile = ground.GetComponent<GroundTileController>();
            if (groundTile && !groundTile.isColored)
                groundTile.ChangeColor(solveColor);
        }
        #endregion
    }

    private void SetDestination(Vector3 direction)
    {
        _movingDirection = direction;

        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, direction, out raycastHit, 100f))
        {
            _nextCollisionPosition = raycastHit.point;
        }

        _playerCondition.IsMoving = true;
        if (!SoundEffect.Instance.GetComponent<AudioSource>().isPlaying)
            SoundEffect.Instance.PlayPlayerMove();
    }

}
