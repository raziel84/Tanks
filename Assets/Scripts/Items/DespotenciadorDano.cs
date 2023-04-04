using UnityEngine;
using System.Collections;

public class DespotenciadorDano : MonoBehaviour {

    //Este script debe colocarse sobre un objeto que actúe como un item del juego
    //Cuando el personaje con la etiqueta Player toca el objeto disminuirá de tamaño
    //y disminuirá el daño que provoca. Las balas también serñan más pequeñas mientras dure este efecto

    private string tagJugador = "Player";            //Quienes tengan está etiqueta podrán tomar el gameobject que contenga este script
    private float tiempoTranscurrido = 0;            //Para guardar la cantidad de tiempo que a transcurrido
    private bool isPlayer = false;                   //Se volverá a true cuando el gameobject que entre al trigger tenga la etiqueta Player
    private Transform padre;                        //Guarda la referencia a la transform del Player que toco este objeto
    private Complete.TankShooting tankShooting;     //Guarda la referencia al script del pj que calcula el daño e instancia las balas
    private float extraDamage = -20f;                //La cantidad de daño extra que hará

    // Update is called once per frame
    void Update()
    {
        //Si hubo colision y el valor de bonus es diferente significa que el PJ tomo un ítem con otro efecto
        if (isPlayer && tankShooting.bonusDamage != extraDamage)
        {
            Destroy(gameObject);
        }

        //Si lo que toco el trigger tiene la etiqueta Player empezaremos a sumar el deltaTime
        if (isPlayer)
        {
            //Disminuimos el tamaño del PJ con un Lerp
            padre.localScale = new Vector3(Mathf.Lerp(1f, 0.7f, Time.time), Mathf.Lerp(1f, 0.7f, Time.time), Mathf.Lerp(1f, 0.7f, Time.time));
            //Acumulamos el deltaTime
            tiempoTranscurrido = tiempoTranscurrido + Time.deltaTime;            
        }
        //Controlamos que la cantidad de tiempo transcurrido sea aproximadamente 5 s
        if (tiempoTranscurrido > 4.9f && tiempoTranscurrido < 5.1f)
        {
            //Restauramos el tamaño del PJ con un Lerp
            padre.localScale = new Vector3(Mathf.Lerp(0.7f, 1f, Time.time), Mathf.Lerp(0.7f, 1f, Time.time), Mathf.Lerp(0.7f, 1f, Time.time));
            //Restauramos el daño
            tankShooting.bonusDamage = 0f;
            //Volvemos las balas al tamaño por defecto
            tankShooting.bonusSize = 0;
            //Destruimos este objeto
            DestruirObjeto();
        }

    }

    void OnTriggerEnter(Collider info)
    {
        //Si lo que entra al Trigger no es un Player salimos del método
        if (info.tag != tagJugador) return;
        //Hacemos el item hijo del PJ que lo tomo
        transform.SetParent(info.transform);
        //Guardamos la referencia al Transform del padre
        padre = info.transform;
        //Desactivamos el renderer para que no se muestre en la escena
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Desactivamos el collider para evitar colisiones
        gameObject.GetComponent<Collider>().enabled = false;

        //Guardamos la referencia al script del pj que calcula el daño e instancia las balas
        tankShooting = info.GetComponent<Complete.TankShooting>();

        //Comprobamos que el player tenga el script asignado para evitar error NullReference
        if (tankShooting == null)
        {
            Debug.Log("El PJ no tiene el script");
            return;
        }

        if (tankShooting.bonusDamage < 0)                               //Si se cumple significa que el PJ ya cogio una copia de este ítem
        {
            transform.SetParent(null);                                  //quita el objeto de la jerarquia del tanque
            gameObject.GetComponent<MeshRenderer>().enabled = true;     //activa el Renderer del objeto
            gameObject.GetComponent<Collider>().enabled = true;      //activamos el collider
            return;                                                     //Salimos del Trigger
        }

        //Pasamos la disminuciñon de daño extra realizará
        tankShooting.bonusDamage = extraDamage;
        //Indicamos con -1 que las balas reduzcan su tamaño
        tankShooting.bonusSize = -1;
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

