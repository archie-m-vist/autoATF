using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AATF
{
    public class Check_Stats
    {
        public static void check_stats(player line)
        {
            // default to error
            line.statType = -1;
            if (!(line.Goalkeeping == line.Stamina &&
                    line.Dribbling == line.Goalkeeping &&
                    line.Finishing == line.Dribbling &&
                    line.Low_Pass == line.Finishing &&
                    line.Lofted_Pass == line.Low_Pass &&
                    line.Header == line.Lofted_Pass &&
                    line.Controlled_Spin == line.Header &&
                    line.Saving == line.Controlled_Spin &&
                    line.Clearing == line.Saving &&
                    line.Reflexes == line.Clearing &&
                    line.Body_Balance == line.Reflexes &&
                    line.Physical_Contact == line.Body_Balance &&
                    line.Kicking_Power == line.Physical_Contact &&
                    line.Explosive_Power == line.Kicking_Power &&
                    line.Jump == line.Explosive_Power &&
                    line.Ball_Control == line.Jump &&
                    line.Ball_Winning == line.Ball_Control &&
                    line.Coverage == line.Ball_Winning &&
                    line.Place_Kicking == line.Coverage &&
                    line.Speed == line.Place_Kicking &&
                    line.Stamina == line.Speed))
            {
                Console.WriteLine(line.id + "\t" + line.name + " has mismatched stats.");
                variables.errors++;
            }
            int stat = line.Goalkeeping;
            for (uint i = 0; i < constants.stats.Length; ++i)
            {
                if (stat == constants.stats[i]) { line.statType = 0; line.ptype = i; }
                else if ( stat == constants.stats[i] - 3 ) { line.statType = 1; line.ptype = i; }
                else if ( stat == constants.stats[i] + 3 ) { line.statType = 2; line.ptype = i; }
            }

            // If it doesn't match anything after checking all options
            if (line.statType < 0)
            {
                Console.WriteLine(line.id + "\t" + line.name + " has invalid stats");
                variables.errors++;
            }
        }

        public static void check_prowess(player line)
        {
            // Prowess can be set to any value, but cannot exceed the medal stat for that player
            // a player with valid stats should have them all equal
            int stats = line.Goalkeeping;
            // First check if player has valid stats
            if ( line.statType < 0 )
            {
                // Player has invalid stats, disregard Prowess check
                Console.WriteLine(line.id + "\t" + line.name + " has invalid stats, so Prowess cannot be checked");
                variables.errors++;
                stats = -1;
            }

            if (stats > 0)
            {
                if (line.Attacking_Prowess > stats)
                {
                    Console.WriteLine(line.id + "\t" + line.name + " has invalid Attacking Prowess (Is " + line.Attacking_Prowess + ", should be " + stats + ")");
                    variables.errors++;
                }

                if (line.Defensive_Prowess > stats)
                {
                    Console.WriteLine(line.id + "\t" + line.name + " has invalid Defensive Prowess (Is " + line.Defensive_Prowess + ", should be " + stats + ")");
                    variables.errors++;
                }
            }
        }

        public static void check_ggss(team squad)
        {
            uint[] counts = { 0, 0, 0, 0 };
            uint[] required = { 18, 2, 2, 1 };
            string[] types = { "regular", "bronze", "silver", "gold" };

            foreach (player line in squad.team_players)
            {
                
                int type = Math.Max((int)(line.ptype) - 1, 0);
                counts[type] += 1;
            }

            for ( uint i = 0; i < counts.Length; ++i )
            {
                if (counts[i] == required[i]) { continue; }
                string errortype = "";
                if (counts[i] > required[i]) { errortype = " has too many "; }
                else if (counts[i] < required[i]) { errortype = " has too few ";  }
                Console.WriteLine(squad.team_name + errortype + types[i] + "; has " + counts[i] + ", needs " + required[i]);
            }
        }
    }
}
