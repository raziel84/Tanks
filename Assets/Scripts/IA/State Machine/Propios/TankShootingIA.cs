using UnityEngine;
using UnityEngine.UI;

public class TankShootingIA : MonoBehaviour
    {
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        protected float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        protected float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        protected bool m_Fired;                       // Whether or not the shell has been launched with this button press.

        public float bonusDamage;                   //Modifica el daño básico
        public int bonusSize = 0;                   //Modifica el tamaño de la bala. 1 aumenta, -1 dismuye, 0 normal

        public bool shoot = false;

        private void Awake() {
            
        }

        protected virtual void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }

        protected virtual void Start ()
        {            
            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;            


        }

        protected virtual void Update ()
        {           

            // The slider should have a default value of the minimum launch force.
            m_AimSlider.value = m_MinLaunchForce;

            if (shoot) {
                m_Fired = false;
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;

                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();

                // If the max force has been exceeded and the shell hasn't yet been launched...
                if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
                {
                    // ... use the max force and launch the shell.
                    m_CurrentLaunchForce = m_MaxLaunchForce;
                    Fire();
                }

            }
        }

        protected virtual void Fire ()
        {
            
        }

        protected virtual Vector3 CalculoSize()
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
