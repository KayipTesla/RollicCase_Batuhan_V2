using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 firstMousePos;
    private float changeOfMousePos, horizontalMovementChange;
    [SerializeField] private float horizontalSpeed = 8f, playerXClampValue = 4.5f;
    [SerializeField] private Transform playerVisual;
    [SerializeField] Rigidbody rb;
    [SerializeField] float nextLevelRushSpeed;
    private void Start()
    {
        GameManager.instance.nextLevelStartPosition = transform.position;
    }
   private void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            rb.velocity = new Vector3(0, 0, 5);
            if (Input.GetMouseButtonDown(0))
            {
                TouchControlsDown();
            }
            if (Input.GetMouseButtonDown(0))
            {
                TouchControlsDown();
            }
            else if (Input.GetMouseButton(0))
            {
                TouchControls();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                TouchControlsUp();
            }
            MoveCharacter();
        }
        else if (GameManager.instance.gameState == GameManager.GameState.StartClimbing)
        {
            rb.velocity = new Vector3(0, 0, 5);
            if (Input.GetMouseButtonDown(0))
            {
                ThrowForceCalculater();
            }
        }
        else if (GameManager.instance.gameState == GameManager.GameState.EndClimbing)
        {
            PushingPlayer();
        }
        else if (GameManager.instance.gameState == GameManager.GameState.NextLevel)
        {
            transform.position = Vector3.Lerp(transform.position, GameManager.instance.nextLevelStartPosition, nextLevelRushSpeed * Time.deltaTime);
        }
        else if (GameManager.instance.gameState == GameManager.GameState.Lose)
        {
            transform.position = GameManager.instance.nextLevelStartPosition;
        }
    }
    private void TouchControlsDown()
    {
        firstMousePos = Input.mousePosition;
    }
    private void TouchControls()
    {
        horizontalMovementChange = 0;
        changeOfMousePos = Input.mousePosition.x - firstMousePos.x;
        if (Mathf.Abs(changeOfMousePos) > 0.1f)
        {
            horizontalMovementChange = (changeOfMousePos * 1 / Screen.width);
            firstMousePos = Input.mousePosition;
        }
    }
    private void TouchControlsUp()
    {
        horizontalMovementChange = 0;
        firstMousePos = Input.mousePosition;
    }
    private void MoveCharacter()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(
                Mathf.Lerp(transform.localPosition.x, transform.localPosition.x + (horizontalMovementChange * playerXClampValue * 2f), horizontalSpeed * Time.deltaTime),
                            (-1f * playerXClampValue),
                            playerXClampValue),
                transform.localPosition.y,
                transform.localPosition.z);
    }
    private void ThrowForceCalculater()
    {
        if (GameManager.instance.throwForce <=5000)
        {
            GameManager.instance.throwForce += 75;
        }
        rb.AddForce(transform.forward*100000,ForceMode.Force);
    }
    private void PushingPlayer()
    {
        rb.AddForce(transform.forward * 10 * GameManager.instance.throwForce);
        GameManager.instance.gameState = GameManager.GameState.Stop;
    }
    public void ThrowPlayer()
    {
        rb.AddForce(transform.forward * GameManager.instance.throwForce, ForceMode.Force);
    }
}
