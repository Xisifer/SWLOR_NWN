﻿using System;
using System.Globalization;
using SWLOR.Game.Server.Core.NWNX;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Legacy.ChatCommand.Contracts;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;

namespace SWLOR.Game.Server.Legacy.ChatCommand
{
    [CommandDetails("Permanently deletes your character.", CommandPermissionType.Player | CommandPermissionType.DM | CommandPermissionType.Admin)]
    public class Delete : IChatCommand
    {
        /// <summary>
        /// Deletes a player's character. Player must submit the command twice within 30 seconds.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        /// <param name="targetLocation"></param>
        /// <param name="args"></param>
        public void DoAction(NWPlayer user, NWObject target, NWLocation targetLocation, params string[] args)
        {
            var lastSubmission = user.GetLocalString("DELETE_CHARACTER_LAST_SUBMISSION");
            var isFirstSubmission = true;

            // Check for the last submission, if any.
            if (!string.IsNullOrWhiteSpace(lastSubmission))
            {
                // Found one, parse it.
                var dateTime = DateTime.Parse(lastSubmission);
                if(DateTime.UtcNow <= dateTime.AddSeconds(30))
                {
                    // Player submitted a second request within 30 seconds of the last one. 
                    // This is a confirmation they want to delete.
                    isFirstSubmission = false;
                }
            }

            // Player hasn't submitted or time has elapsed
            if (isFirstSubmission)
            {
                user.SetLocalString("DELETE_CHARACTER_LAST_SUBMISSION", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
                user.FloatingText("Please confirm your deletion by entering another \"/delete <CD Key>\" command within 30 seconds.");
            }
            else
            {
                var dbPlayer = DataService.Player.GetByID(user.GlobalID);
                dbPlayer.IsDeleted = true;
                DataService.SubmitDataChange(dbPlayer, DatabaseActionType.Update);

                NWScript.BootPC(user, "Your character has been deleted.");

                NWScript.DelayCommand(6.0f, () =>
                {
                    Administration.DeletePlayerCharacter(user, true);
                });

            }
        }

        public string ValidateArguments(NWPlayer user, params string[] args)
        {
            return "Currently disabled.";

            if (!user.IsPlayer)
                return "You can only delete a player character.";

            var cdKey = NWScript.GetPCPublicCDKey(user);
            var enteredCDKey = args.Length > 0 ? args[0] : string.Empty;

            if (cdKey != enteredCDKey)
            {
                return "Invalid CD key entered. Please enter the command as follows: \"/delete <CD Key>\". You can retrieve your CD key with the /CDKey chat command.";
            }
            
            return string.Empty;
        }

        public bool RequiresTarget => false;
    }
}