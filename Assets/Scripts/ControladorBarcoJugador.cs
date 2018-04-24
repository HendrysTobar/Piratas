using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarcoJugador : Controlador
{
    #region Atributos
    private Animator animator;
    /// <summary>
    /// Delegado que encapsula un método, se inicializa como delegado anónimo
    /// </summary>
    public Action<bool> aMuerte = delegate { };
    #endregion

	#region Funciones de evento
    public override void Start()
    {
        // Realiza llamada del método de a clase base
        base.Start();
        // Se obtiene referencia del componente Animator de uno de los hijos de este objeto de juego
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Si se mantiene presionado el botón izquierdo del mouse y si la posición del mouse no está sobre algún elemento de UI (Interfaz gráfica) entonces
        if (Input.GetMouseButton(0) && !Util.IsPointerOverUIObject())
        {
            // Se realiza la rotación y el movimiento del barco jugador
            RotarBarco(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            MoverBarco(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
	#endregion

    #region Métodos
    /// <summary>
    /// Sobreescribe el mmétodo ReflejarDano de la clase base
    /// </summary>
    public override void ReflejarDano()
    {
        // Realiza llamada del método de la clase base
        base.ReflejarDano();
        // Se invoca delegado (Action), éste recibe como parámetro un booleano el cual es obtenido del método VerificarEstadoDanoActual
        aMuerte(VerificarEstadoDanoActual());
        // se hace uso del componente Animator, el cual establece el valor del parámetro de tipo entero: Dano, el cual es usado para cambiar el estado de Animación de este barco
        animator.SetInteger("Dano", puntosDeDano);
    }
    #endregion
}
