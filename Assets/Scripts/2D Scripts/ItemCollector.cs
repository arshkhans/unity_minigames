using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text cherriesText;
    [SerializeField] private AudioSource collectSoundEffect;

    private int Cherries = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            Cherries++;
            cherriesText.text = Cherries.ToString();
        }
    }
}
