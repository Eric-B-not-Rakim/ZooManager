using System;
using System.Collections.Generic;

namespace ZooManager
{
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        static private int maxCellsX = 10;
        static private int maxCellsY = 10;


        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);

        static public void SetUpGame()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
        }

        static public void AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                if (numCellsY >= maxCellsY) return; // hit maximum height!
                List<Zone> rowList = new List<Zone>();
                for (var x = 0; x < numCellsX; x++)
                {
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                numCellsY++;
                if (d == Direction.down) animalZones.Add(rowList);
                // if (d == Direction.up) animalZones.Insert(0, rowList);
            }
            else // must be left or right...
            {
                if (numCellsX >= maxCellsX) return; // hit maximum width!
                for (var y = 0; y < numCellsY; y++)
                {
                    var rowList = animalZones[y];
                    // if (d == Direction.left) rowList.Insert(0, new Zone(null));
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                numCellsX++;
            }
        }

        static public void ZoneClick(Zone clickedZone)
        {
            Console.Write("Got animal ");
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAlive();
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddAnimalToHolding(string animalType)
        {
            if (holdingPen.occupant != null) return;
            if (animalType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (animalType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
			if (animalType == "raptor") holdingPen.occupant = new Raptor("EEEAGGLEEEE");
            if (animalType == "chick") holdingPen.occupant = new Chick("bertie rooster");
            if (animalType == "skull") holdingPen.occupant = new Skull("spooky scary skeletons");
            if (animalType == "alien") holdingPen.occupant = new Alien("* x files theme *");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            ActivateAnimals();
        }

        static public void ActivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant != null && zone.occupant.reactionTime == r)
                        {
                            zone.occupant.Activate();
                        }
                    }
                }
            }
        }
        static public void ActivateAlive() // Adds one more turn to each creature's life
        {
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    if (zone.occupant != null)
                    {
                        zone.occupant.AddAlive();
                    }
                }
            }
        }

        static public bool Seek(int x, int y, Direction d, string target)
        {
            switch (d)
            {
                case Direction.up:
                    y--;
                    break;
                case Direction.down:
                    y++;
                    break;
                case Direction.left:
                    x--;
                    break;
                case Direction.right:
                    x++;
                    break;
            }
            if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
            if (animalZones[y][x].occupant == null) return false;
            if (animalZones[y][x].occupant.species == target)
            {
                return true;
            }
            return false;
        }

        static public bool AlienSeek(int x, int y, Direction d)
            // This is super simple. Basically it's the same as the Seek method above,
            // but instead of looking for a specific target we're looking for anything
            // that's not another alien. That means skulls, other creatures, etc.
        {
            switch (d)
            {
                case Direction.up:
                    y--;
                    break;
                case Direction.down:
                    y++;
                    break;
                case Direction.left:
                    x--;
                    break;
                case Direction.right:
                    x++;
                    break;
            }
            if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
            if (animalZones[y][x].occupant == null) return false;
            if (animalZones[y][x].occupant.species != "alien")
            {
                return true;
            }
            return false;
        }

        /* This method currently assumes that the attacker has determined there is prey
         * in the target direction. In addition to bug-proofing our program, can you think
         * of creative ways that NOT just assuming the attack is on the correct target (or
         * successful for that matter) could be used?
         */

        static public void Attack(Creature attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    animalZones[y - 1][x].occupant = attacker;
                    break;
                case Direction.down:
                    animalZones[y + 1][x].occupant = attacker;
                    break;
                case Direction.left:
                    animalZones[y][x - 1].occupant = attacker;
                    break;
                case Direction.right:
                    animalZones[y][x + 1].occupant = attacker;
                    break;
            }
            animalZones[y][x].occupant = null;
        }

        static public bool RaptorSeek(int x, int y, Direction d)
            // See the Raptor.cs for more information (L)
        {
            switch (d)
            {
                case Direction.up:
                    y = y - 2;
                    break;
                case Direction.down:
                    y = y + 2;
                    break;
                case Direction.left:
                    x = x - 2;
                    break;
                case Direction.right:
                    x = x + 2;
                    break;
            }
            if (y < 0 || x < 0 || y > numCellsY - 2 || x > numCellsX - 2) return false;
            if (animalZones[y][x].occupant == null) return true;
            if (animalZones[y][x].occupant.species == "")
            {
                return true;
            }
            return false;
        }

        static public void RaptorMove(Creature raptor, Direction d)
            // See the Raptor.cs for more information (L)
        {
            int x = raptor.location.x;
            int y = raptor.location.y;

            switch (d)
            {
                case Direction.up:
                    animalZones[y - 2][x].occupant = raptor;
                    break;
                case Direction.down:
                    animalZones[y + 2][x].occupant = raptor;
                    break;
                case Direction.left:
                    animalZones[y][x - 2].occupant = raptor;
                    break;
                case Direction.right:
                    animalZones[y][x + 2].occupant = raptor;
                    break;
            }
            animalZones[y][x].occupant = null;
        }

        static public void Maturing(Chick chick) // Fulfills (N)
        {
            Console.WriteLine($"What's this? {chick.name} is evolving!");
            int x = chick.location.x;
            int y = chick.location.y;
            Raptor buffChick = new Raptor(chick.name); // Basically we're creating a whole new raptor object and replacing the chick
            animalZones[y][x].occupant = buffChick;
            buffChick.turnsAlive = 0;
        }
        static public void PerishingRaptor(Raptor raptor) // Fulfills (P)
        {
            Console.WriteLine($"Oh no! {raptor.name} has bit the dust!");
            int x = raptor.location.x;
            int y = raptor.location.y;
            Skull skeletor = new Skull(raptor.name); // Basically we're creating a whole new skull object and replacing the raptor
            animalZones[y][x].occupant = skeletor;
            skeletor.turnsAlive = 0;
        }
        static public void PerishingCat(Cat cat) // Fulfills (P)
        {
            Console.WriteLine($"Oh no! {cat.name} has bit the dust!");
            int x = cat.location.x;
            int y = cat.location.y;
            Skull skeletor = new Skull(cat.name); // Basically we're creating a whole new skull object and replacing the cat
            animalZones[y][x].occupant = skeletor;
            skeletor.turnsAlive = 0;
        }


        /* We can't make the same assumptions with this method that we do with Attack, since
         * the animal here runs AWAY from where they spotted their target (using the Seek method
         * to find a predator in this case). So, we need to figure out if the direction that the
         * retreating animal wants to move is valid. Is movement in that direction still on the board?
         * Is it just going to send them into another animal? With our cat & mouse setup, one is the
         * predator and the other is prey, but what happens when we have an animal who is both? The animal
         * would want to run away from their predators but towards their prey, right? Perhaps we can generalize
         * this code (and the Attack and Seek code) to help our animals strategize more...
         */

        static public bool Retreat(Animal runner, Direction d)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    /* The logic below uses the "short circuit" property of Boolean &&.
                     * If we were to check our list using an out-of-range index, we would
                     * get an error, but since we first check if the direction that we're modifying is
                     * within the ranges of our lists, if that check is false, then the second half of
                     * the && is not evaluated, thus saving us from any exceptions being thrown.
                     */
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                    /* Note that in these four cases, in our conditional logic we check
                     * for the animal having one square between itself and the edge that it is
                     * trying to run to. For example,in the above case, we check that y is greater
                     * than 0, even though 0 is a valid spot on the list. This is because when moving
                     * up, the animal would need to go from row 1 to row 0. Attempting to go from row 0
                     * to row -1 would cause a runtime error. This is a slightly different way of testing
                     * if 
                     */
                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
            }
            return false; // fallback
        }
    }
}

