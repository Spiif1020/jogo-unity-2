using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Desafio : MonoBehaviour
{

    public GameObject Protege;
    public GameObject Protege2;
    public GameObject Protege3;
    public GameObject Protege4;
    public GameObject Protege5;
    public GameObject Protege6;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      private void OnCollisionEnter2D(Collision2D other)
   {
        
      string tag = other.gameObject.tag;

       switch(tag)
      {
         case "Botao":
            Protege.SetActive(false);
            break;
         default:
            Debug.Log("Portal Aberto!");
            break;
      }       

      switch(tag)
      {
         case "Botao2":
            Protege2.SetActive(false);
            break;
         default:
            Debug.Log("Portal Aberto!");
            break;
      }       

      switch(tag)
      {
         case "Botao3":
            Protege3.SetActive(false);
            break;
         default:
            Debug.Log("Portal Aberto!");
            break;
      }       

      switch(tag)
      {
         case "Botao4":
            Protege4.SetActive(false);
            break;
         default:
            Debug.Log("Portal Aberto!");
            break;
      }       

      switch(tag)
      {
         case "Botao5":
            Protege5.SetActive(false);
            break;
         default:
            Debug.Log("Portal Aberto!");
            break;
      }       

      switch(tag)
      {
         case "Botao6":
            Protege6.SetActive(false);
            break;
         default:
            Debug.Log("Portal Aberto!");
            break;
      }       
     
     }
}
