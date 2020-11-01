using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Quest;

namespace SWLOR.Game.Server.Legacy.Scripts.Quest.GuildTasks.ArmorsmithGuild
{
    public class HeavyHelmetII: AbstractQuest
    {
        public HeavyHelmetII()
        {
            CreateQuest(156, "Armorsmith Guild Task: 1x Heavy Helmet II", "arm_tsk_156")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "helmet_h2", 1, true)

                .AddRewardGold(185)
                .AddRewardGuildPoints(GuildType.ArmorsmithGuild, 39);
        }
    }
}