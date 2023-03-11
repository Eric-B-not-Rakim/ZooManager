using System;

namespace ZooManager
{
	public class Raptor : Bird, IPredator // Fulfilling (A) and (B)
	{
		public Raptor(string name)
		{
			emoji = "🦅";
			species = "raptor";
			this.name = name;
			reactionTime = 1; // reaction time 1 (fast)
			ateLast = 0;
			isPredator = true;
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
            
                //Array values = Enum.GetValues(typeof(Direction));
                //Random random = new Random();
                //Direction randomDirection = (Direction)values.GetValue(random.Next(values.Length));
            

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
            // Fulfills (L)
            // Okay, so this is some clunky code, but it does a similar thing to attacking and seeking.
            // The only difference is, we're looking two spaces away, and we're actively looking for empty
            // spaces rather than ones that are occupied by a specific entity.
			else if (Game.RaptorSeek(location.x, location.y, Direction.up))
            {
                Game.RaptorMove(this, Direction.up);
            }
            else if (Game.RaptorSeek(location.x, location.y, Direction.down))
            {
                Game.RaptorMove(this, Direction.down);
            }
            else if (Game.RaptorSeek(location.x, location.y, Direction.left))
            {
                Game.RaptorMove(this, Direction.left);
            }
            else if (Game.RaptorSeek(location.x, location.y, Direction.right))
            {
                Game.RaptorMove(this, Direction.right);
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

