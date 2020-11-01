﻿using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Quest.Contracts;
using SWLOR.Game.Server.Legacy.Service;

namespace SWLOR.Game.Server.Legacy.Quest.Reward
{
    public class QuestGPReward: IQuestReward
    {
        public GuildType Guild { get; }
        public int Amount { get; }

        public QuestGPReward(GuildType guild, int amount, bool isSelectable)
        {
            Guild = guild;
            Amount = amount;
            IsSelectable = isSelectable;

            var dbGuild = DataService.Guild.GetByID((int) guild);
            MenuName = Amount + " " + dbGuild.Name + " GP";
        }

        public bool IsSelectable { get; }
        public string MenuName { get; }

        public void GiveReward(NWPlayer player)
        {
            var pcGP = DataService.PCGuildPoint.GetByPlayerIDAndGuildID(player.GlobalID, (int)Guild);
            var rankBonus = 0.25f * pcGP.Rank;
            var gp = Amount + (int)(Amount * rankBonus);
            GuildService.GiveGuildPoints(player, Guild, gp);
        }
    }
}