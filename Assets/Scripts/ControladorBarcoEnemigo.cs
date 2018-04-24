using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarcoEnemigo : Controlador
{
    #region Atributos
    /// <summary>
    /// Almacena cada uno de los sprites que representan los estados de deterioro del barco según su estado de daño
    /// </summary>
    public Sprite[] estadosDeDano;
    private GameObject barcoJugador;
    /// <summary>
    /// Distancia mínima a la que se podrá acercar un barco enemigo al barco jugador
    /// </summary>
    private float distanciaMinima = 2.5f;
    public float tiempoInicioDisparo;
    public float tiempoIntervaloDisparo;
    #endregion

	#region Funciones de evento
    public override void Start()
    {
        // Llamado a la función de evento de la clase base
        base.Start();
        // Se obtine referencia del barco del jugador (directamente haciendo la busqued de su script: ControladorBarcoJugador)
        barcoJugador = FindObjectOfType<ControladorBarcoJugador>().gameObject;
        // Invoca repetidamente el método cuyo nombre es pasado como primer parámetro, con un tiempo de retraso y un tiempo de intervalo entre cada llamada
        InvokeRepeating("Disparar", tiempoInicioDisparo, tiempoIntervaloDisparo);
    }

    private void Update()
    {
        // Si la referencia al barco del jugador es diferente de nulo
        if (barcoJugador != null)
        {
            // se obtiene la distancia que hay desde el barco jugador hasta este barco
            float distancia = (barcoJugador.transform.position - this.transform.position).magnitude;
            // si esa distancia es mayor a la distancia mínima en que se puede acercar
            if (distancia > distanciaMinima)
            {
                // Entonces d¡este barco se movera hacía donde se mueva el barco del jugador
                MoverBarco(barcoJugador.transform.position);
            }

            // Se realiza la rotación de este barco en función de la posición del barco del jugador
            RotarBarco(barcoJugador.transform.position);
        }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Sobreescribe el método de la clase base
    /// </summary>
    public override void ReflejarDano()
    {
        // se realiza llamado del método de la clase base
        base.ReflejarDano();
        // Se verifica estado actual de este barco
        if (VerificarEstadoDanoActual())
        {
            // Antes de ser destruido se incrementa en 1 la variable enemigosDestruidos para saber cuantos enemigos han sido destruidos (por el jugador o por otro barco enemigo)
            ManejadorJuego.Instance.enemigosDestruidos++;
            return;
        }
        // Se realiza el cambio del sprite actual, dependiendo de los puntos de daño
        spriteRenderer.sprite = estadosDeDano[puntosDeDano];
    }
    #endregion	
}
