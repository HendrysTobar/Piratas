using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    
    #region Métodos
    /// <summary>
    /// Se encarga de destruir este objeto de juego
    /// Se deja como público para que desde la vista de Animation, se haga uso de los eventos de animación, una vez la animación de explosión finaliza, se ejecuta este método
    /// </summary>
    public void Destruir() { Destroy(this.gameObject); }
    #endregion
}
