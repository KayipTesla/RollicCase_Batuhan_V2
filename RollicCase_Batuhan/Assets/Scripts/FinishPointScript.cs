using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinishPointScript : MonoBehaviour
{
    [SerializeField] int point;
    [SerializeField] Text pointText;

    private void Start()
    {
        pointText.text = point.ToString();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponentInParent<Rigidbody>().velocity == Vector3.zero)
            {
                GameManager.instance.finishPoint = point;
                PlayerStopOnFinishLine();
            }
        }
    }
    private IEnumerator FinishLineAnimationPlay()
    {
        GetComponentInParent<Animator>().SetBool("finish", true);
        yield return new WaitForSeconds(5f);
        GetComponentInParent<Animator>().SetBool("finish", false);
    }
    private void PlayerStopOnFinishLine()
    {
        if (GameManager.instance.gameState != GameManager.GameState.Finish)
        {
            StartCoroutine(GameManager.instance.Finish());
            StartCoroutine(FinishLineAnimationPlay());
        }
    }
}
