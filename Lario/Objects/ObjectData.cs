using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Objects
{
    public class ObjectData
    {
        
        /// <summary>
        /// Affect level score
        /// </summary>
        public int Score { get; set; }


        /// <summary>
        /// Affect player life
        /// </summary>
        public int PlayerLife { get; set; }

        /// <summary>
        /// Set the player to jump
        /// </summary>
        public bool PlayerJump { get; set; }

        /// <summary>
        /// Set the force of the jump
        /// </summary>
        public float JumpForce { get; set; }
    }
}
