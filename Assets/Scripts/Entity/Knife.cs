using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public class Knife : BaseWeapon
    {
        public override void Animate()
        {
            animator.SetTrigger(stabHash);
        }
    }
}
