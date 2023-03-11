using System;

namespace ZooManager
{
    public class Alien : Creature, IPredator
    {

        public Alien(string name)
        {
            emoji = "👽";
            species = "alien";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)
            isPredator = true;
        }

        public override void Activate()
        {
            base.Activate();
            Hunt();
            Console.WriteLine("I am an alien. BOOOWEEEEOOOOO.");
        }

        public void Hunt()
        {
            //For more info on how this works, look at the method in Game.cs
            if (Game.AlienSeek(location.x, location.y, Direction.up))
            {
                Game.Attack(this, Direction.up);
                ateLast = 0;
            }
            else if (Game.AlienSeek(location.x, location.y, Direction.down))
            {
                Game.Attack(this, Direction.down);
                ateLast = 0;
            }
            else if (Game.AlienSeek(location.x, location.y, Direction.left))
            {
                Game.Attack(this, Direction.left);
                ateLast = 0;
            }
            else if (Game.AlienSeek(location.x, location.y, Direction.right))
            {
                Game.Attack(this, Direction.right);
                ateLast = 0;
            }
        }
    }
}
