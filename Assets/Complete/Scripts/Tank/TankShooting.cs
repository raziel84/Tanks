﻿using UnityEngine;
using UnityEngine.UI;
using CnControls;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        private string m_FireButton;                // The input axis that is used for launching shells.
        private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

        public Canvas joystick;
        private SimpleButton simpleButton;          //Para asignar la referencia al script Simple Button
        private string botonPresionado;             //Guardamos la referencia al nombre del botón según el player

        public float bonusDamage;                   //Modifica el daño básico
        public int bonusSize = 0;                   //Modifica el tamaño de la bala. 1 aumenta, -1 dismuye, 0 normal

        private void Awake()
        {
            simpleButton = joystick.GetComponentInChildren<SimpleButton>();     //Asignamos la referencia al script simple button      
        }


        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }


        private void Start ()
        {
            //Solamente el player 1 utiliza el botón del joystick tactil
            if (m_PlayerNumber == 1)
            {
                simpleButton.ButtonName = "Fire" + m_PlayerNumber;
            }
            else { 
                
                // The fire axis is based on the player number.
                m_FireButton = "Fire" + m_PlayerNumber;
            }
            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
            //Llamamos a este método para guardar en una variable intermedia el nombre asignado al botón
            QueJugadorEs();
        }

        private void QueJugadorEs()
        {
            //Método para asignar el joystick táctico sólo al player 1
            if(m_PlayerNumber == 1) {
                botonPresionado = simpleButton.ButtonName;
            } else {
                botonPresionado = m_FireButton;
            }
        }


        private void Update ()
        {
            // The slider should have a default value of the minimum launch force.
            m_AimSlider.value = m_MinLaunchForce;

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                // ... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
            }
            // Otherwise, if the fire button has just started being pressed...
            else if (CnInputManager.GetButtonDown(botonPresionado))
            {
                // ... reset the fired flag and reset the launch force.
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                // Change the clip to the charging clip and start it playing.
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play ();
            }
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (CnInputManager.GetButton(botonPresionado) && !m_Fired)
            {
                // Increment the launch force and update the slider.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;
            }
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (CnInputManager.GetButtonUp(botonPresionado) && !m_Fired)
            {
                // ... launch the shell.
                Fire ();
            }
        }


        private void Fire ()
        {
            // Set the fired flag so only Fire is only called once.
            m_Fired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            //Llamamos a CalculoSize para asignar el tamaño de la bala
            shellInstance.transform.localScale = CalculoSize();

            //Guardamos la referencia al script ShellExplosion de la bala instanciada
            Complete.ShellExplosion shellExplotion = shellInstance.GetComponent<Complete.ShellExplosion>();
            
            //Asignamos el bonus de daño
            shellExplotion.bonusDamage = bonusDamage;
            
            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();

            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;
        }

        private Vector3 CalculoSize()
        {
            //Creamos una variable para devolver el tamaño
            Vector3 tamano = Vector3.zero;

            switch (bonusSize)
            {
                //Tamaño por defecto
                case 0:
                    tamano = Vector3.one;
                    break;
                //Tamaño aumentado al coger un ítem de aumento de daño
                case 1:
                    tamano = new Vector3(1.3f, 1.3f, 1.3f);
                    break;
                ////Tamaño disminuido al coger un ítem de reducción de daño
                case -1:
                    tamano = new Vector3(0.7f, 0.7f, 0.7f);
                    break;
            }

            return tamano;
        }
    }
}