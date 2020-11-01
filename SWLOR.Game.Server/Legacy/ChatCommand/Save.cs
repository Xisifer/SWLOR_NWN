﻿using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Legacy.ChatCommand.Contracts;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;

namespace SWLOR.Game.Server.Legacy.ChatCommand
{
    [CommandDetails("Manually saves your character. Your character also saves automatically every few minutes.", CommandPermissionType.Player)]
    public class Save: IChatCommand
    {
        /// <summary>
        /// Exports user's character bic file.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        /// <param name="targetLocation"></param>
        /// <param name="args"></param>
        public void DoAction(NWPlayer user, NWObject target, NWLocation targetLocation, params string[] args)
        {
            PlayerService.SaveCharacter(user);
            PlayerService.SaveLocation(user);
            NWScript.ExportSingleCharacter(user.Object);
            NWScript.SendMessageToPC(user.Object, "Character saved successfully.");
        }

        public string ValidateArguments(NWPlayer user, params string[] args)
        {
            return string.Empty;
        }

        public bool RequiresTarget => false;
    }
}