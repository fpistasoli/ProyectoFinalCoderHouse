using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonfire.Items
{
    public enum PickupType
    {
        HealthBoost, //aumenta health points (por defecto 5 puntos)
        PowerBoost, //aumenta el damage de proyectiles y punch (por defecto 2 puntos)
        Upgrade, //aumenta en 1 punto todos los health y power boosts en el inventario
    }
}



