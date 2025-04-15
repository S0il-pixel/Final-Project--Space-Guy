using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project__Space_Guy
{
    public class UserCharacter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; } = 50;
        public int Scores { get; set; } = 0;
        public List<object> Gear { get; set; }

        public UserCharacter(string name, string description, int credits, int scores, List<object> gear)
        {
            Name = name;
            Description = description;
            Credits = credits;
            Scores = scores;
            Gear = new List<object>();
        }
    }

    public class Stuff
    {
        public class Weapons
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int Damage { get; set; }

            public Weapons(string name, string type, string description, int cost, int damage)
            {
                Name = name;
                Type = type;
                Description = description;
                Cost = cost;
                Damage = damage;
            }
        }

        public class Tools
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Cost { get; set; }
            public int UsesLeft { get; set; }

            public Tools(string name, string description, int cost, int usesLeft)
            {
                Name = name;
                Description = description;
                Cost = cost;
                UsesLeft = usesLeft;
            }
        }

        public class Helpers
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Dialogue { get; set; }
            public string Skills { get; set; }
            public int Cost { get; set; }
            public List<object> Gear { get; set; }

            public Helpers(string name, string description, string dialogue, string skills, int cost, List<object> gear)
            {
                Name = name;
                Description = description;
                Dialogue = dialogue;
                Skills = skills;
                Cost = cost;
                Gear = gear;
            }
        }
    }
}

