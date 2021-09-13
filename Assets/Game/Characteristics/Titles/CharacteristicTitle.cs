using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Characteristics.Titles
{
    [CreateAssetMenu(fileName = "CharacteristicTitle", menuName = "Runic/Titles/CharacteristicTitle")]
    public class CharacteristicTitle : BaseTitle
    {
        public int ExtraHealth = 20;
        public int ExtraMana = 20;
        public int ExtraStamina = 20;
    }
}
