﻿using SWLOR.Game.Server.Core.NWNX;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;




namespace SWLOR.Game.Server.Service
{
    public static class LocalVariableService
    {
        public static void CopyVariables(NWObject oSource, NWObject oCopy)
        {
            int variableCount = Object.GetLocalVariableCount(oSource);
            for (int variableIndex = 0; variableIndex < variableCount - 1; variableIndex++)
            {
                Object.LocalVariable stCurVar = Object.GetLocalVariable(oSource, variableIndex);

                switch (stCurVar.Type)
                {
                    case LocalVariableType.Int:
                        oCopy.SetLocalInt(stCurVar.Key, oSource.GetLocalInt(stCurVar.Key));
                        break;
                    case LocalVariableType.Float:
                        oCopy.SetLocalFloat(stCurVar.Key, oSource.GetLocalFloat(stCurVar.Key));
                        break;
                    case LocalVariableType.String:
                        oCopy.SetLocalString(stCurVar.Key, oSource.GetLocalString(stCurVar.Key));
                        break;
                    case LocalVariableType.Object:
                        oCopy.SetLocalObject(stCurVar.Key, oSource.GetLocalObject(stCurVar.Key));
                        break;
                    case LocalVariableType.Location:
                        oCopy.SetLocalLocation(stCurVar.Key, oSource.GetLocalLocation(stCurVar.Key));
                        break;
                }
            }
        }
    }
}