﻿using static WorldOfZuul.Program;

namespace WorldOfZuul
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;

        public Game()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
  
            Room? outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            Room? theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            Room? pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            Room? lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
            Room? office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            outside.SetExits(null, theatre, lab, pub); // North, East, South, West

            theatre.SetExit("west", outside);

            pub.SetExit("east", outside);

            lab.SetExits(outside, office, null, null);

            office.SetExit("west", lab);

            currentRoom = outside;
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
            System.Console.WriteLine("We hope you enjoyed and gain educational information about SDGs");
            System.Console.WriteLine("Creators are:");
            System.Console.WriteLine("___Vedat Esendag___");
            System.Console.WriteLine("___Altan Esmer___");
            System.Console.WriteLine("___Frederik Handberg___");
            System.Console.WriteLine("___Ignat Bozhinov___");
            System.Console.WriteLine("___Leonardo Gianola___");
            System.Console.WriteLine("___Habib Ahmed Wasi___\n");
            System.Console.WriteLine("▄█▀▀║░▄█▀▄║▄█▀▄║██▀▄║");
            System.Console.WriteLine("██║▀█║██║█║██║█║██║█║");
            System.Console.WriteLine("▀███▀║▀██▀║▀██▀║███▀║");
            System.Console.WriteLine("───────────────────────");
            System.Console.WriteLine("───▐█▀▄─ ▀▄─▄▀ █▀▀──█───");
            System.Console.WriteLine("───▐█▀▀▄ ──█── █▀▀──▀───");
            System.Console.WriteLine("───▐█▄▄▀ ──▀── ▀▀▀──▄───");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the World of Zuul! :)");
            System.Console.WriteLine("╔╦╦╦═╦╗╔═╦═╦══╦═╗");
            System.Console.WriteLine("║║║║╩╣╚╣═╣║║║║║╩╣");
            System.Console.WriteLine("╚══╩═╩═╩═╩═╩╩╩╩═╝");
            Console.WriteLine("World of Zuul is a text-based adventure game, \nwhich focus to contribute UN SDGs and educate players to improve life infrastructure in the world");
            System.Console.WriteLine("Do not forget! Your aim is to apply SDGs to future people and save the city. Good Luck!");
            System.Console.WriteLine("First enter your name hero!!");
            Hero hero = new Hero();
            hero.Name = Console.ReadLine();
            if (hero.Name == "")
            {
                System.Console.WriteLine("You dont prefer to say your name ____ OK.");
                System.Console.WriteLine("I'm going to call you Mr Eco");
                hero.Name = "Mr Eco";
                System.Console.WriteLine($"Good to see you {hero.Name}");
            }
            else
            {
                System.Console.WriteLine("Nice name :)");
                System.Console.WriteLine($"Good to see you {hero.Name}");
            }
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. This is not the best moment in your life, is it?");
            Console.WriteLine("You wander around the Sønderborg city, which located in South Denmark next to the Baltic Sea.");
            System.Console.WriteLine("You need to search for something or ... someone to save yourself in this situation.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details about environment.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
