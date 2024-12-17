using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    

    // Start is called before the first frame update
    void Start()
    {
        
        coinText.text = 0.ToString();
    }

    // Update is called once per frame
    
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            coinText.text = (int.Parse(coinText.text) + 1).ToString();
            Destroy(gameObject);
        }
    }
}
    
