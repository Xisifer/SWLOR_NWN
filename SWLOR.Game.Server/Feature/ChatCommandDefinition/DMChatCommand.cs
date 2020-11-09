﻿using System;
using System.Collections.Generic;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.Service.ChatCommandService;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Feature.ChatCommandDefinition
{
    public class DMChatCommand: IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("copyitem")
                .Description("Copies the targeted item.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    if (GetObjectType(target) != ObjectType.Item)
                    {
                        SendMessageToPC(user, "You can only copy items with this command.");
                        return;
                    }

                    CopyItem(target, user, true);
                    SendMessageToPC(user, "Item copied successfully.");
                });

            builder.Create("day")
                .Description("Sets the world time to 8 AM.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    SetTime(8, 0, 0, 0);
                });

            builder.Create("night")
                .Description("Sets the world time to 8 PM.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    SetTime(20, 0, 0, 0);
                });

            builder.Create("getplot")
                .Description("Gets whether an object is marked plot.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    SendMessageToPC(user, GetPlotFlag(target) ? "Target is marked plot." : "Target is NOT marked plot.");
                })
                .RequiresTarget();

            builder.Create("kill")
                .Description("Kills your target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    var amount = GetMaxHitPoints(target) + 11;
                    var damage = EffectDamage(amount);
                    ApplyEffectToObject(DurationType.Instant, damage, target);
                })
                .RequiresTarget();

            builder.Create("name")
                .Description("Renames your target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) => args.Length <= 0 ? "Please enter a name. Example: /name My Creature" : string.Empty)
                .Action((user, target, location, args) =>
                {
                    if (GetIsPC(target) || GetIsDM(target))
                    {
                        SendMessageToPC(user, "PCs cannot be targeted with this command.");
                        return;
                    }

                    var name = string.Empty;
                    foreach (var arg in args)
                    {
                        name += " " + arg;
                    }

                    SetName(target, name);
                })
                .RequiresTarget();

            builder.Create("rez")
                .Description("Revives you, heals you to full, and restores all FP/STM.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Action((user, target, location, args) =>
                {
                    if (GetIsDead(user))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectResurrection(), user);
                    }

                    ApplyEffectToObject(DurationType.Instant, EffectHeal(999), user);
                });

            builder.Create("spawngold")
                .Description("Spawns gold of a specific quantity on your character. Example: /spawngold 33")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return ColorToken.Red("Please specify a quantity. Example: /spawngold 34");
                    }
                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var quantity = 1;

                    if (args.Length >= 1)
                    {
                        if (!int.TryParse(args[0], out quantity))
                        {
                            return;
                        }
                    }

                    GiveGoldToCreature(user, quantity);
                });

            builder.Create("tpwp")
                .Description("Teleports you to a waypoint with a specified tag.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "You must specify a waypoint tag. Example: /tpwp MY_WAYPOINT_TAG";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var tag = args[0];
                    var wp = GetWaypointByTag(tag);

                    if (!GetIsObjectValid(wp))
                    {
                        SendMessageToPC(user, "Invalid waypoint tag. Did you enter the right tag?");
                        return;
                    }

                    AssignCommand(user, () => ActionJumpToLocation(GetLocation(wp)));
                });

            GetLocalVariable(builder);
            SetLocalVariable(builder);
            SetPortrait(builder);
            SpawnItem(builder);


            return builder.Build();
        }

        private static void GetLocalVariable(ChatCommandBuilder builder)
        {
            builder.Create("getlocalfloat")
                .Description("Gets a local float on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /GetLocalFloat Variable_Name. Example: /GetLocalFloat MY_VARIABLE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = NWScript.GetLocalFloat(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });

            builder.Create("getlocalint")
                .Description("Gets a local integer on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /GetLocalInt Variable_Name. Example: /GetLocalInt MY_VARIABLE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalInt(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });

            builder.Create("getlocalstring")
                .Description("Gets a local string on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /GetLocalString Variable_Name. Example: /GetLocalString MY_VARIABLE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalString(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });
        }

        private static void SetLocalVariable(ChatCommandBuilder builder)
        {
            builder.Create("setlocalfloat")
                .Description("Sets a local float on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 2)
                    {
                        return "Missing arguments. Format should be: /SetLocalFloat Variable_Name <VALUE>. Example: /SetLocalFloat MY_VARIABLE 6.9";
                    }

                    if (!float.TryParse(args[1], out var value))
                    {
                        return "Invalid value entered. Please try again.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = args[0];
                    var value = float.Parse(args[1]);

                    SetLocalFloat(target, variableName, value);

                    SendMessageToPC(user, "Local float set: " + variableName + " = " + value);
                });


            builder.Create("setlocalint")
                .Description("Sets a local int on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 2)
                    {
                        return "Missing arguments. Format should be: /SetLocalInt Variable_Name <VALUE>. Example: /SetLocalInt MY_VARIABLE 69";
                    }

                    if (!int.TryParse(args[1], out var value))
                    {
                        return "Invalid value entered. Please try again.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = args[0];
                    var value = Convert.ToInt32(args[1]);

                    SetLocalInt(target, variableName, value);

                    SendMessageToPC(user, "Local integer set: " + variableName + " = " + value);
                });

            builder.Create("setlocalstring")
                .Description("Sets a local string on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /SetLocalString Variable_Name <VALUE>. Example: /SetLocalString MY_VARIABLE My Text";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = string.Empty;

                    for (var x = 1; x < args.Length; x++)
                    {
                        value += " " + args[x];
                    }

                    value = value.Trim();

                    SetLocalString(target, variableName, value);

                    SendMessageToPC(user, "Local string set: " + variableName + " = " + value);
                });
        }

        private static void SetPortrait(ChatCommandBuilder builder)
        {
            builder.Create("setportrait")
                .Description("Sets portrait of the target player using the string specified. (Remember to add po_ to the portrait)")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return "Please enter the name of the portrait and try again. Example: /SetPortrait po_myportrait";
                    }

                    if (args[0].Length > 16)
                    {
                        return "The portrait you entered is too long. Portrait names should be between 1 and 16 characters.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target) || GetObjectType(target) != ObjectType.Creature)
                    {
                        SendMessageToPC(user, "Only creatures may be targeted with this command.");
                        return;
                    }

                    SetPortraitResRef(target, args[0]);
                    FloatingTextStringOnCreature("Your portrait has been changed.", target, false);
                });
        }

        private static void SpawnItem(ChatCommandBuilder builder)
        {
            builder.Create("spawnitem")
                .Description("Spawns an item of a specific quantity on your character. Example: /spawnitem my_item 3")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return ColorToken.Red("Please specify a resref and optionally a quantity. Example: /spawnitem my_resref 20");
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var resref = args[0];
                    var quantity = 1;

                    if (args.Length > 1)
                    {
                        if (!int.TryParse(args[1], out quantity))
                        {
                            return;
                        }
                    }

                    var item = CreateItemOnObject(resref, user, quantity);

                    if (!GetIsObjectValid(item))
                    {
                        SendMessageToPC(user, ColorToken.Red("Item not found! Did you enter the correct ResRef?"));
                        return;
                    }

                    SetIdentified(item, true);
                });
        }
    }
}