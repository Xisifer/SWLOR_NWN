﻿using SWLOR.Game.Server.Legacy.Quest;
using SWLOR.Game.Server.Legacy.Service;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest
{
    public class CZ220SuppliesWeaponsmith: AbstractQuest
    {
        public CZ220SuppliesWeaponsmith()
        {
            CreateQuest(7, "CZ-220 Supplies - Weaponsmith", "cz220_weaponsmith")
                
                .AddObjectiveCollectItem(1, "club_b", 1, true)
                .AddObjectiveTalkToNPC(2)

                .AddRewardGold(50)
                .AddRewardKeyItem(3)
                .AddRewardFame(2, 5)

                .OnAccepted((player, questGiver) =>
                {
                    KeyItemService.GivePlayerKeyItem(player, 4);
                })
                .OnCompleted((player, questGiver) =>
                {
                    KeyItemService.RemovePlayerKeyItem(player, 4);
                });
        }
    }
}