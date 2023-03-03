using System;

namespace ZooManager
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)
            ateLast = 0;
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            Flee(); // It will prioritize fleeing over hunting
            Hunt();
            Perish();
        }

        /* Note that our cat is currently not very clever about its hunting.
         * It will always try to attack "up" and will only seek "down" if there
         * is no mouse above it. This does not affect the cat's effectiveness
         * very much, since the overall logic here is "look around for a mouse and
         * attack the first one you see." This logic might be less sound once the
         * cat also has a predator to avoid, since the cat may not want to run in
         * to a square that sets it up to be attacked!
         */
        /*
        private void Decision()
        {
            if ((Game.Seek(location.x, location.y, Direction.up, "raptor"))
                || (Game.Seek(location.x, location.y, Direction.down, "raptor"))
                || (Game.Seek(location.x, location.y, Direction.left, "raptor"))
                || (Game.Seek(location.x, location.y, Direction.right, "raptor")))
            {
                Flee();
            }
            else Hunt();
        }
        */
        public void Flee() // Fulfilling (E)
        {
            if (Game.Seek(location.x, location.y, Direction.up, "raptor"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "raptor"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "raptor"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "raptor"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }
        }
        public void Hunt() // Fulfilling (E)
        {
            if ((Game.Seek(location.x, location.y, Direction.up, "chick")) || (Game.Seek(location.x, location.y, Direction.up, "mouse")))
            {
                Game.Attack(this, Direction.up);
                ateLast = 0;
            }
            else if ((Game.Seek(location.x, location.y, Direction.down, "chick")) || (Game.Seek(location.x, location.y, Direction.down, "mouse")))
            {
                Game.Attack(this, Direction.down);
                ateLast = 0;
            }
            else if ((Game.Seek(location.x, location.y, Direction.left, "chick")) || (Game.Seek(location.x, location.y, Direction.left, "mouse")))
            {
                Game.Attack(this, Direction.left);
                ateLast = 0;
            }
            else if ((Game.Seek(location.x, location.y, Direction.right, "chick")) || (Game.Seek(location.x, location.y, Direction.right, "mouse")))
            {
                Game.Attack(this, Direction.right);
                ateLast = 0;
            }
        }
        public void Perish() // See Game.Perishing
        {
            if (ateLast >= 4)
            {
                Game.PerishingCat(this);
            }
        }
    }
}

