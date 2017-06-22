﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AATF
{
    public class Check_Cards
    {
        public static void check_cards(player line, team squad)
        {
            int total_cards = 0;
            bool captaincy = false;

            // Count how many Style Cards the player has
            foreach(int i in line.Cards_Style)
            {
                if(i == 1)
                {
                    total_cards++;
                }
            }

            // Count how many Skill Cards the player has
            foreach (int i in line.Cards_Skills)
            {
                if (i == 1)
                {
                    total_cards++;
                }
            }

            // Count trick cards
            uint trickCount = 0;
            for (uint i = 0; i < constants.trick_cards.Length; ++i) { trickCount += (uint)line.Cards_Skills[constants.trick_cards[i]]; }

            // Captaincy Cards are excluded from the limit
            if(line.Cards_Skills[25] == 1)
            {
                captaincy = true;
            }

            // Check the card counts
            if ((line.is_regular) || (line.is_regular_system1))
            {
                if (total_cards > constants.cards_limit_regular)
                {
                    if ((total_cards == constants.cards_limit_regular + 1) && (captaincy))
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has CAPTAINCY");
                        squad.captains++;
                    }
                    else
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has too many Cards (Has " + total_cards + ", can only have " + constants.cards_limit_regular + " max)");
                        variables.errors++;
                    }
                }
            }
            else if ((line.is_silver) || (line.is_silver_system1))
            {
                trickCount = Math.Min(trickCount, constants.trick_cards_silver);
                if (trickCount > 0)
                {
                    Console.WriteLine(line.id + "\t" + line.name + " has " + trickCount + " free trick card(s)");
                }
                if (total_cards-trickCount > constants.cards_limit_silver)
                {
                    if ((total_cards == constants.cards_limit_silver + 1) && (captaincy))
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has CAPTAINCY");
                        squad.captains++;
                    }
                    else
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has too many regular Cards (Has " + (total_cards-trickCount) + ", can only have " + constants.cards_limit_silver + " max and " + constants.trick_cards_silver + " free trick(s))");
                        variables.errors++;
                    }
                }
            }
            else if ((line.is_gold) || (line.is_gold_system1))
            {
                trickCount = Math.Min(trickCount, constants.trick_cards_gold);
                if (trickCount > 0)
                {
                    Console.WriteLine(line.id + "\t" + line.name + " has " + trickCount + " free trick card(s)");
                }
                if (total_cards - trickCount > constants.cards_limit_gold)
                {
                    if ((total_cards-trickCount == constants.cards_limit_gold + 1) && (captaincy))
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has CAPTAINCY");
                        squad.captains++;
                    }
                    else
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has too many regular Cards (Has " + total_cards + ", can only have " + constants.cards_limit_gold + " max and "+constants.trick_cards_gold+" free trick(s))");
                        variables.errors++;
                    }
                }
            }
            else if ((line.is_goalkeeper) || (line.is_goalkeeper_system1))
            {
                if (total_cards > constants.cards_limit_goalkeeper)
                {
                    if ((total_cards == constants.cards_limit_goalkeeper + 1) && (captaincy))
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has CAPTAINCY");
                        squad.captains++;
                    }
                    else
                    {
                        Console.WriteLine(line.id + "\t" + line.name + " has too many Cards (Has " + total_cards + ", can only have " + constants.cards_limit_goalkeeper + " max)");
                        variables.errors++;
                    }
                }
            }
            else { }
        }

        public static void check_captaincy(team squad)
        {
            // Verify that only one player in the team has an extra captaincy card
            if(squad.captains > 1)
            {
                Console.WriteLine(squad.team_name + " has more than one player with an additional Captaincy card, and so are above the card limit");
                variables.errors++;
            }
        }
    }
}
