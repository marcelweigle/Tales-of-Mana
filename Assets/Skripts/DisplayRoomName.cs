using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRoomName : MonoBehaviour
{
    public string placeName;
    public GameObject text;
    public Text placeText;
    public bool alreadyDisplayed = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyDisplayed)
        {
            text.SetActive(true);
            alreadyDisplayed = true;
            StartCoroutine(placeNameCo());
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
}
