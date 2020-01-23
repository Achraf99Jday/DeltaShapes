using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    
    public class sound : MonoBehaviour {
    public GameObject bu;
   
    public Sprite on, off;
    public  void sounde ()
    {  if ( Input.GetMouseButtonDown(0))
        { if (bu.GetComponent<SpriteRenderer>().sprite== on)
            {
                bu.GetComponent<SpriteRenderer>().sprite = off;
               
                
            }
          else
            {
                bu.GetComponent<SpriteRenderer>().sprite = on;
            }
        }

    }
	
}
