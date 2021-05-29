using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerControlByArduino : MonoBehaviour
{
    // Cette variable fait le lien avec le script ReadArduinoInputExample.
    // Il faut lui drag'n'drop l'objet ReadArduino dans l'editeur.
    public ReadTwoArduinoValuesExample myArduino;
    Rigidbody rg;

    float touched = 0;
    float ptouched = 0;
    float xpos = 0;
    float ypos = 0;
    float pxpos = 0;
    float pypos = 0;
    float dx = 0;
    float dy = 0;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        touched = myArduino.values[0];
        if (touched == 1 && ptouched == 0) // on vient d'appuyer
        {

            xpos = myArduino.xpos;
            ypos = myArduino.ypos;
        }

        else if (touched == 1  && ptouched == 1) // on est en train de se déplacer
        {
            pxpos =  myArduino.xpos;
            pypos = myArduino.ypos;
           // Debug.Log(pxpos );
        }
        else if (touched == 0 && ptouched == 1) // on vient de relacher
        {


            Debug.Log(dx + ", " + dy);
            rg.AddForce(dx*400, 0, dy*200); //100


        }
         dx = xpos - pxpos;
         dy = ypos - pypos;

        //rg.AddForce(0, 0, 0);
        ptouched = touched;
        // Lecture de la 1er valeur
        //Debug.Log(myArduino.values[0]);

        // Lecture de la 2eme valeur
        //Debug.Log(myArduino.values[1]);

        //Lecteur de la 3eme valeur
        //Debug.Log(myArduino.values[2]);

        // 220 est l'emplacement de la balle au démarrage
       // Vector3 pos = new Vector3(myArduino.xpos, 220, myArduino.ypos);


        // Pour que la sphere ne bouge pas au démarrage
      //  transform.localPosition = pos;

       // gameObject.transform.Translate(pos.x,0,pos.z);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider.GetComponent<Rigidbody>().useGravity = true;
        rg.AddExplosionForce(500, transform.position, 100, 2);
    }
}



 