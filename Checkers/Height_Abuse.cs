using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AATF
{
    class Height_Abuse
    {
        public static void check_height_abuse(team squad)
        {
            uint[] brackets = new uint[4];
            uint[] height_brackets = { 194, 185, 180, 175 };

            foreach (player line in squad.team_players)
            {
                bool found = false;
                for (uint i = 0; i < height_brackets.Length; ++i)
                {
                    
                    if (line.height == height_brackets[i])
                    {
                        brackets[i]++;
                        found = true;
                        break;
                    }
                }
                if ( line.height == 189 && line.is_goalkeeper ) { brackets[1]++; found = true; }
                if ( line.height < 175 ) { brackets[3]++; found = true; }
                if (!found)
                {
                    Console.WriteLine("HEIGHT ABUSE: " + line.id + "\t" + line.name + " has invalid height " + line.height + "; must be 194, 185, 180, 175.");
                    variables.errors++;
                }
                if (line.Goalkeeping != constants.stats[line.ptype])
                {
                    if (line.Goalkeeping == constants.stats[line.ptype]+3)
                    {
                        if ( line.height > 175 )
                        {
                            Console.WriteLine("HEIGHT ABUSE: " + line.id + "\t" + line.name + " has boosted stats but height above 175.");
                            variables.errors++;
                        }
                        if ( line.ptype >= 2 ) // no medals with boosted stats
                        {
                            Console.WriteLine("HEIGHT ABUSE: " + line.id + "\t" + line.name + " has boosted stats but is a medal player.");
                            variables.errors++;
                        }
                    }
                    else if (line.Goalkeeping == constants.stats[line.ptype]-3)
                    {
                        if ( line.height < 194 )
                        {
                            Console.WriteLine("HEIGHT ABUSE: " + line.id + "\t" + line.name + " has nerfed stats but height below 194.");
                            variables.errors++;
                        }
                    }
                }
            }
        }
    }
}
