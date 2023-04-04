using UnityEngine;
using System.Collections;

public class Escudo : MonoBehaviour {

    //Este script debe colocarse sobre un objeto que actúe como un item del juego
    //Cuando el personaje con la etiqueta Player toca el objeto, el PJ no sufrirá daño del próximo ataque

    private string tagJugador = "Player";   //Quienes tengan está etiqueta podrán tomar el gameobject que contenga este script
    private Complete.TankHealth tankHealth; //Guarda la referencia al script del pj que contiene las variables de vida   

    void OnTriggerEnter(Collider info)
    {
        //Si lo que entra al Trigger no es un Player salimos del método
        if (info.tag != tagJugador) return;

        //Guardamos la referencia al script que contiene las variables de vida
        tankHealth = info.GetComponent<Complete.TankHealth>();

        //Comprobamos que el player tenga el script asignado para evitar error NullReference
        if (tankHealth == null)
        {
            Debug.Log("El PJ no tiene el script");
            return;
        }

        //Comprobamos si el escudo ya fue activado. Si es así salimos del trigger
        if (tankHealth.tieneEscudo)
            return;

        //Indicamos que el escudo debe activarse
        tankHealth.tieneEscudo = true;

        //Debug.Log("ESCUDO ACTIVADO");

        //Destruimos el objeto
        Destroy(gameObject);
    }
}
