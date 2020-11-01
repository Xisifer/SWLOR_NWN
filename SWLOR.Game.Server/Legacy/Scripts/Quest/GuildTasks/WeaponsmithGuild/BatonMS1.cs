using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.WeaponsmithGuild
{
    public class BatonMS1: AbstractQuest
    {
        public BatonMS1()
        {
            CreateQuest(251, "Weaponsmith Guild Task: 1x Baton MS1", "wpn_tsk_251")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "morningstar_1", 1, true)

                .AddRewardGold(85)
                .AddRewardGuildPoints(GuildType.WeaponsmithGuild, 19);
        }
    }
}