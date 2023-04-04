using UnityEngine;
using System.Collections;

public class BotiquinRegeneradorV1 : MonoBehaviour
{

    //Este script debe colocarse sobre un objeto que actúe como un item del juego
    //Cuando el personaje con la etiqueta Player toca el objeto recuperará vida en cada cuadro
    //hasta que se cumpla el tiempo establecido en la variable timer.

    private string tagJugador = "Player";   //Quienes tengan está etiqueta podrán tomar el gameobject que contenga este script
    public float vidaCurar = 10f;           //Cantidad de vida a recuperar por segundo
    public float curacion;                  //Guarda la cantidad de vida a recuperar por cada cuadro
    private Complete.TankHealth tankHealth; //Guarda la referencia al script del pj que contiene las variables de vida
    private float timer;                    //Tiempo que debe transcurrir antes de detener el efecto y destruir este gameobject
    private bool isPlayer = false;          //Se volverá a true cuando el gameobject que entre al trigger tenga la etiqueta Player

    private void Update()
    {   //Update corre en cada frame

        if (timer > 0)
        {
            //Restamos de timer el tiempo que tardo en dibujarse cada cuadro (Time.deltaTime)
            timer = timer - Time.deltaTime;
            //Guardamos en la variable curacion el porcentaje de vida en relacion con Time.deltaTime
            curacion = vidaCurar * Time.deltaTime;
            //Llamamos al método Curación del pj y le pasamos la porción de curación
            tankHealth.Curacion(curacion);
        }

        //Controlamos que el timer sea menor a cero y que lo que entro al trigger sea un Player
        if (timer < 0 && isPlayer)
        {
            DestruirObjeto();
        }
    }

    void OnTriggerEnter(Collider info)
    {
        //Si lo que entra al Trigger no es un Player salimos del método
        if (info.tag != tagJugador) return;

        //Hacemos hijo del Player el gameobject con este script
        transform.SetParent(info.transform);
        //Desactivamos el renderer para que no se muestre en la escena
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Guardamos la referencia al script que contiene las variables de vida
        tankHealth = info.gameObject.GetComponent<Complete.TankHealth>();

        //Comprobamos que el player tenga el script asignado para evitar error NullReference
        if (tankHealth == null) {
            Debug.Log("El PJ no tiene el script");
            return;
        }
        //Asignamos el tiempo que durara la curación
        timer = 5f;
        //Volvemos a true la variable
        isPlayer = true;

    }

    private void DestruirObjeto()
    {
        //El gameobject deja de ser hijo del Player
        transform.SetParent(null);
        //Destruimos el gameobject
        Destroy(gameObject);
    }
}
