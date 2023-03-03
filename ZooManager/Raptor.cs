using System;

namespace ZooManager
{
	public class Raptor : Bird // Fulfilling (A) and (B)
	{
		public Raptor(string name)
		{
			emoji = "🦅";
			species = "raptor";
			this.name = name;
			reactionTime = 1; // reaction time 1 (fast)
			ateLast = 0;
		}

		public override void Activate()
		{
			base.Activate();
			Console.WriteLine("WAAAAA *eagle screeches*");
			Hunt();
			Perish();
		}

		public void Hunt() // Now it's looking for both cats and mice, irrespective of species (D)
		{
			if ((Game.Seek(location.x, location.y, Direction.up, "cat")) || (Game.Seek(location.x, location.y, Direction.up, "mouse")))
			{
				Game.Attack(this, Direction.up);
                ateLast = 0;
            }
			else if ((Game.Seek(location.x, location.y, Direction.down, "cat")) || (Game.Seek(location.x, location.y, Direction.down, "mouse")))
            {
				Game.Attack(this, Direction.down);
                ateLast = 0;
            }
			else if ((Game.Seek(location.x, location.y, Direction.left, "cat")) || (Game.Seek(location.x, location.y, Direction.left, "mouse")))
			{
				Game.Attack(this, Direction.left);
                ateLast = 0;
            }
			else if ((Game.Seek(location.x, location.y, Direction.right, "cat")) || (Game.Seek(location.x, location.y, Direction.right, "mouse")))
			{
				Game.Attack(this, Direction.right);
                ateLast = 0;
            }
		}
        public void Perish() // See Game.Perishing
        {
            if (ateLast >= 4)
            {
                Game.PerishingRaptor(this);
            }
        }
    }
}

