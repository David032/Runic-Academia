using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Characteristics.Titles
{
    [CreateAssetMenu(fileName = "DamageTitle", menuName = "Runic/Titles/DamageTitle")]
    public class DamageTitle : BaseTitle
    {
        public int ExtraDamage = 20;
        public List<TypeOfEntity> AffectedTypes = new List<TypeOfEntity>();
    }
}

