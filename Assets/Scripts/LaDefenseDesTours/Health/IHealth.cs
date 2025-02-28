using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours
{
    public interface Health
    {
        float maxHealth { get; set; }
        float health { get; set; }

        event Action OnHealthChanged;
    }
}
