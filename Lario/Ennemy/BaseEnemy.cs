using Lario.Objects;
using Lario.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Ennemy
{
    public abstract class BaseEnemy : BaseObject
    {
        protected enum State
        {
            Idle,
            Moving,
            Dead
        }

        protected State _state;

        protected List<Vector2> _waypoints;

        protected int? _currentWaypoint = null;

        public bool LoopThroughWaypoint { get; set; }

        protected readonly Vector2 Gravity = new Vector2(0, 9.8f);

        public Vector2 Velocity { get; set; }

        public BaseEnemy() : base()
        {
            _state = State.Idle;
            _waypoints = new List<Vector2>();
        }

        public void AddWaypoint(Vector2 waypoint)
        {
            if(_waypoints.Count == 0)
            {
                _currentWaypoint = 0;
            }

            _waypoints.Add(waypoint);

        }

        public void ClearWaypoint()
        {
            _currentWaypoint = null;
            _waypoints.Clear();
        }
    }
}
