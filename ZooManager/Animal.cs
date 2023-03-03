﻿using System;
namespace ZooManager
{
    public class Animal
    {
        public string emoji;
        public string species;
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        public int turnsAlive = 0; // default turns that you have stayed alive for (M)
        public int ateLast = 0; // Useful only for predators, but hey, might as well stick it here. (P)

        public Point location;

        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }

        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }
        public void AddAlive() // This is adding the turnsAlive variable so we can track how many turns the animal's been alive for (M)
        {
            turnsAlive++;
            ateLast++;
            Console.WriteLine($"Animal {name} at {location.x},{location.y} has been alive for {turnsAlive} turns!");
        }
    }
}
