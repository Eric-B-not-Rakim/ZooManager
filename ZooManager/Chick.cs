using System;
using ZooManager;

public class Chick : Bird, IPrey // Fulfulling (C)
{
    bool matured;

    public Chick(string name)
    {
        emoji = "🐥";
        species = "chick";
        this.name = name;
        reactionTime = new Random().Next(6, 10);
        matured = false; // Have we all grown up yet?
        isPrey = true;
    }

    public override void Activate()
    {
        base.Activate();
        Console.WriteLine("WAKE UP I AM THE ROOSTER");
        Flee();
        Mature();
    }

    public void Flee()
    {
        if ((Game.Seek(location.x, location.y, Direction.up, "cat")) || (Game.Seek(location.x, location.y, Direction.up, "mouse")))
        {
            if (Game.Retreat(this, Direction.down)) return;
        }
        else if ((Game.Seek(location.x, location.y, Direction.down, "cat")) || (Game.Seek(location.x, location.y, Direction.down, "mouse")))
        {
            if (Game.Retreat(this, Direction.up)) return;
        }
        else if ((Game.Seek(location.x, location.y, Direction.left, "cat")) || (Game.Seek(location.x, location.y, Direction.left, "mouse")))
        {
            if (Game.Retreat(this, Direction.right)) return;
        }
        else if ((Game.Seek(location.x, location.y, Direction.right, "cat")) || (Game.Seek(location.x, location.y, Direction.right, "mouse")))
        {
            if (Game.Retreat(this, Direction.left)) return;
        }
    }

    public void Mature() // See Game.Maturing
    {
        if ((turnsAlive >= 4) && (matured == false))
        {
            Game.Maturing(this);
        }
    }
}

