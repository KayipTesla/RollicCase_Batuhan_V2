using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChallengeAreaScript : MonoBehaviour
{
    [SerializeField] int collectableObjectCount;
    [SerializeField] int maxCollectableObjectCount;

    [SerializeField] Text collectableObjectCountText;

    [SerializeField] List<GameObject> collectableObject;

    [SerializeField] Animator animator;
    public bool contactControlFlag= false;
   

    private void Start()
    {
        collectableObjectCountText.text = collectableObjectCount.ToString() + "/" + maxCollectableObjectCount.ToString();
    }

    public IEnumerator ChallengeControl()
    {
        yield return new WaitForSeconds(3f);
        if (collectableObjectCount>=maxCollectableObjectCount)
        {
            StartCoroutine(CollectTheObject());
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }
    private IEnumerator CollectTheObject()
    {
        while (collectableObject.Count - 1 >= 0)
        {
            collectableObject[collectableObject.Count - 1].SetActive(false);
            collectableObject.Remove(collectableObject[collectableObject.Count - 1]);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(ContinueGame());
    }
    private IEnumerator ContinueGame()
    {
        animator.SetBool("OpenGate", true);
        yield return new WaitForSeconds(2f);
        GameManager.instance.gameState = GameManager.GameState.Play;
        LevelPartInCrease();
    }
    public void LevelPartInCrease()
    {
        GameManager.instance.mapPartIndex++;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableObject"))
        {
            collectableObject.Add(other.gameObject);
            collectableObjectCount++;
            collectableObjectCountText.text = collectableObjectCount.ToString() + "/" + maxCollectableObjectCount.ToString();
        }
    }
}
