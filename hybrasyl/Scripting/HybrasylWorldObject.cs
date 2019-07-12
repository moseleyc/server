﻿/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2013 Justin Baugh (baughj@hybrasyl.com)
 * (C) 2015-2016 Project Hybrasyl (info@hybrasyl.com)
 *
 * For contributors and individual authors please refer to CONTRIBUTORS.MD.
 * 
 */


using System.Reflection;
using Hybrasyl.Objects;
using log4net;
using MoonSharp.Interpreter;

namespace Hybrasyl.Scripting
{
    [MoonSharpUserData]
    public class HybrasylWorldObject
    {
        internal WorldObject Obj { get; set; }

        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog ScriptingLogger = LogManager.GetLogger(Assembly.GetEntryAssembly(),"ScriptingLog");


        public string Name
        {
            get
            {
                if (Obj is ItemObject)
                    return (Obj as ItemObject).Name;
                else
                    return Name;
            }
        }
        public byte X => Obj.X;
        public byte Y => Obj.Y;

        public HybrasylWorldObject(WorldObject obj)
        {
            Obj = obj;
        }

        public void DisplayPursuits(dynamic invoker)
        {
            if (Obj is Merchant || Obj is Reactor)
            {
                var merchant = Obj as VisibleObject;
                if (invoker is HybrasylUser)
                {
                    var hybUser = (HybrasylUser)invoker;
                    merchant.DisplayPursuits(hybUser.User);
                }
            }

        }

        public void Destroy()
        {
            if (Obj is ItemObject || Obj is Gold)
            {
                Game.World.Remove(Obj);
            }
        }

        public void AddPursuit(HybrasylDialogSequence hybrasylSequence)
        {
            if (Obj is VisibleObject && !(Obj is User))
            {
                var vobj = Obj as VisibleObject;
                vobj.AddPursuit(hybrasylSequence.Sequence);
            }
        }


        public void RegisterSequence(HybrasylDialogSequence hybrasylSequence)
        {
            if (hybrasylSequence is null)
            {
                ScriptingLogger.Error("RegisterSequence called with null/nil value. Check Lua");
                return;       
            }
            if (Obj is VisibleObject && !(Obj is User))
            {
                var vobj = Obj as VisibleObject;
                vobj.RegisterDialogSequence(hybrasylSequence.Sequence);
            }
        }

        /// <summary>
        /// Calculate the Manhattan distance between ourselves and a target object.
        /// </summary>
        /// <param name="target">The target object</param>
        /// <returns></returns>
        public int Distance(HybrasylWorldObject target) => Obj.Distance(target.Obj);

        public void Say(string message)
        {
            if (Obj is VisibleObject)
            {
                var creature = Obj as VisibleObject;
                creature.Say(message);
            }
        }

    }
}
