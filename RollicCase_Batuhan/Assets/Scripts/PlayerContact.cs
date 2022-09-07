using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContact : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("CollectableObject"))
        {
            if (GameManager.instance.gameState == GameManager.GameState.Stop)
            {
                PushCollectableObject(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Challenge"))
        {
            ChallangeAreaContact(other.GetComponent<ChallengeAreaScript>());
        }
        if (other.gameObject.CompareTag("FinishStart"))
        {
            GameManager.instance.StartClimbingTheRamp();
            other.gameObject.GetComponent<Collider>().enabled = false;
        }
        if (other.gameObject.CompareTag("FinishEnd"))
        {
            GameManager.instance.gameState = GameManager.GameState.EndClimbing;
            GetComponent<PlayerMovement>().ThrowPlayer();
        }
    }
    private void ChallangeAreaContact(ChallengeAreaScript _challangeArea)
    {
        if (GameManager.instance.gameState != GameManager.GameState.NextLevel && !_challangeArea.contactControlFlag)
        {
            _challangeArea.contactControlFlag = true;
            GameManager.instance.gameState = GameManager.GameState.Stop;
            StartCoroutine(_challangeArea.ChallengeControl());
        }
    }
    private void PushCollectableObject(GameObject _collectableObject)
    {
        _collectableObject.GetComponent<Rigidbody>().AddForce(0, 0, 150);
    }
}
