using System;

namespace ZooManager
{
    public class Skull : Animal // Fulfulling (P)
    {
        public Skull(string name)
        {
            emoji = "☠";
            species = "skull";
            this.name = name;
            reactionTime = 10;
        }
    }
}
