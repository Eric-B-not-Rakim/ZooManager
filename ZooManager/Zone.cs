using System;
namespace ZooManager
{
    public class Zone
    {
        private Creature _occupant = null;
        public Creature occupant
        {
            get { return _occupant; }
            set {
                _occupant = value;
                if (_occupant != null) {
                    _occupant.location = location;
                }
            }
        }

        public Point location;

        public string emoji
        {
            get
            {
                if (occupant == null) return "";
                return occupant.emoji;
            }
        }

        public string rtLabel
        {
            get
            {
                if (occupant == null) return "";
                return occupant.reactionTime.ToString();
            }
        }

        public string aliveLabel // shows how many turns we've been alive for!
        {
            get
            {
                if (occupant == null) return "";
                return occupant.turnsAlive.ToString();
            }
        }

        public Zone(int x, int y, Creature animal) // Yes, the occupant is still labeled "animal", but hey
        {
            location.x = x;
            location.y = y;

            occupant = animal;
        }
    }
}
