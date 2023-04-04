using UnityEngine;
using System.Collections;
using Complete;

public class Botiquin : MonoBehaviour {

    //Este script debe colocarse sobre un objeto que actúe como un item del juego
    //Cuando el personaje con la etiqueta Player toca el objeto recuperará vida

    public float cantidadACurar = 25f;              //puntos a curar.
    private string playerTag = "Player";            //para comprobar si el collider que dispara el trigger es un jugador.

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);     //para que el objeto rote una vez que se crea.
    }

    void OnTriggerEnter(Collider tank)
    {
        //si el tag del del collider "tank" no es playerTag.
        if (tank.tag != playerTag) return;
        
        //se obtiene el script "Tankhealth" del tanque.
        Complete.TankHealth vidaTanque = tank.gameObject.GetComponent<Complete.TankHealth>();

        //Comprobamos que el player tenga el script asignado para evitar error NullReference
        if (vidaTanque == null)
        {
            Debug.Log("El PJ no tiene el script");
            return;
        }

        vidaTanque.Curacion(cantidadACurar);        //se le pasa al script los puntos a curar para que haga el cálculo.
        Destroy(gameObject);                        //destruye el botiquin.                                                                 
        
    }
}
