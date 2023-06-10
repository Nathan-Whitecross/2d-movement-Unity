using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AICrontroller")]
public class AIController : InputController
{
    [Header("Interaction")]
    [SerializeField] private LayerMask _layerMask = -1;
    [Header("Ray")]
    [SerializeField] private float _bottomDistance = 1f;
    [SerializeField] private float _centerDistance = 1f;
    [SerializeField] private float _topDistance = 1f;
    
    [SerializeField] private float _xOffset = 1f;
    [SerializeField] private float _yOffset = 0f;

    private bool isJumping = false;

    public override bool RetrieveJumpInput(GameObject gameObject)
    {
        RaycastHit2D groundInfoCenter = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y + _yOffset), Vector2.right, _centerDistance, _layerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y + _yOffset), Vector2.right * _centerDistance * gameObject.transform.localScale.x, Color.green);

        RaycastHit2D groundInfoTop = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.up, _topDistance, _layerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.up * _topDistance, Color.green);

        if (groundInfoCenter.collider && !groundInfoTop.collider)
        {
            isJumping = true;
            return true;
        }
        else
        {
            isJumping = false;
        }

        return false;
    }

    public override float RetrieveMoveInput(GameObject gameObject)
    {
        RaycastHit2D groundInfoBottom = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.down, _bottomDistance, _layerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y), Vector2.down * _bottomDistance, Color.green);

        RaycastHit2D groundInfoCenter = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y + _yOffset), Vector2.right * gameObject.transform.localScale.x, _centerDistance, _layerMask);
        Debug.DrawRay(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x), gameObject.transform.position.y + _yOffset), Vector2.right * _centerDistance * gameObject.transform.localScale.x, Color.green);

        if ((groundInfoCenter.collider == true || groundInfoBottom.collider == false) && isJumping == false)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        }

        return gameObject.transform.localScale.x;
    }

    public override bool RetrieveJumpHoldInput(GameObject gameObject)
    {
        return false;
    }

    public override bool RetrieveDashInput(GameObject gameObject)
    {
        return false;
    }

    public override float RetrieveClimbInput(GameObject gameObject)
    {
        return 0;
    }
}
