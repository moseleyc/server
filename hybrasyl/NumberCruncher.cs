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
 * (C) 2020 ERISCO, LLC 
 *
 * For contributors and individual authors please refer to CONTRIBUTORS.MD.
 * 
 */
 
using Hybrasyl.Objects;
using System;
using System.Linq;

namespace Hybrasyl
{

    // A simple class to hold damage output along with flags / element, which we use elsewhere (specifically statuses)
    public class DamageOutput
    {
        public double Amount { get; set; }
        public Xml.DamageType Type { get; set; }
        public Xml.DamageFlags Flags { get; set; }
        public Xml.Element Element { get; set; }
    }
    /// <summary>
    /// This class is used to do a variety of numerical calculations, in order to consolidate those into
    /// one place. Specifically, healing and damage, are handled here.
    /// </summary>
    ///
    static class NumberCruncher
    {

        // This is dumb, but it's a consequence of how xsd2code works
        private static double _evalSimple(dynamic simple)
        {
            if (simple is Xml.SimpleQuantity)
            {
                // Simple damage can either be expressed as a fixed value <Simple>50</Simple> or a min/max <Simple Min="50" Max="100"/>
                if (!string.IsNullOrEmpty(simple.Value)) return Convert.ToInt32(simple.Value);
                var rand = new Random();
                return rand.Next(Convert.ToInt32(simple.Min), Convert.ToInt32(simple.Max));
            }
            throw new InvalidOperationException("Invalid type passed to _evalSimple");
        }

        private static double _evalFormula(string formula, Xml.Castable castable, Creature target, Creature source)
        {
            try
            {
                return new FormulaParser(source, castable, target).Eval(formula);
            }
            catch (Exception e)
            {
                Game.ReportException(e);
                GameLog.Error($"NumberCruncher formula error: castable {castable.Name}, target {target.Name}, source {source?.Name ?? "no source"}: {formula}, error: {e}");
                return 0;
            }
        }

        /// <summary>
        /// Calculate the damage for a castable.
        /// </summary>
        /// <param name="castable">The castable to use for the calculation</param>
        /// <param name="target">The target of the castable (i.e. the spell/skill target)</param>
        /// <param name="source">The source of the castable (i.e. the caster)</param>
        /// <returns></returns>
        public static DamageOutput CalculateDamage(Xml.Castable castable, Creature target, Creature source = null)
        {
            var rand = new Random();
            // Defaults
            double dmg = 1;
            var type = castable.Effects?.Damage?.Type ?? Xml.DamageType.Magical;

            if (castable.Effects?.Damage == null) return new DamageOutput() { Amount = dmg, Type = type, Flags = Xml.DamageFlags.None, Element = castable.Element }; 

            if (castable.Effects.Damage.IsSimple)
            {
                var simple = castable.Effects.Damage.Simple;
                dmg = _evalSimple(simple);
            }
            else
            {
                var formula = castable.Effects.Damage.Formula;
                dmg = _evalFormula(formula, castable, target, source);
            }
            return new DamageOutput() { Amount = dmg * target.Stats.DamageModifier,
                Type = type, Flags = castable.Effects.Damage.Flags,
                Element = castable.Element };
        }

        /// <summary>
        /// Calculate the healing for a castable.
        /// </summary>
        /// <param name="castable">The castable to use for the calculation</param>
        /// <param name="target">The target of the castable (i.e. the spell/skill target)</param>
        /// <param name="source">The source of the castable (i.e. the caster), optional parameter</param>
        /// <returns></returns>
        public static double CalculateHeal(Xml.Castable castable, Creature target, Creature source = null)
        {
            var rand = new Random();
            double heal = 0;
            if (castable.Effects?.Heal == null) return heal;

            if (castable.Effects.Heal.IsSimple)
                heal = _evalSimple(castable.Effects.Heal.Simple) * target.Stats.HealModifier;
            else
                heal = _evalFormula(castable.Effects.Heal.Formula, castable, target, source);
            return heal * target.Stats.HealModifier;
        }


        /// <summary>
        /// Calculate the damage for a status tick.
        /// </summary>
        /// <param name="castable">Castable responsible for the status</param>
        /// <param name="effect">ModifierEffect structure for the status</param>
        /// <param name="target">Target for the damage (e.g. the player or creature with the status)</param>
        /// <param name="source">Original source of the status</param>
        /// <param name="statusName">The name of the status</param>
        /// <returns></returns>
        public static DamageOutput CalculateDamage(Xml.Castable castable, Xml.ModifierEffect effect, Creature target, Creature source, string statusName)
        {
            // Defaults
            double dmg = 0;
            var type = effect.Damage?.Type ?? Xml.DamageType.Magical;

            if (effect?.Damage == null) return new DamageOutput() { Amount = dmg, Type = type, Flags = Xml.DamageFlags.None, Element = castable.Element };

            var statusAdd = castable?.Effects?.Statuses?.Add?.Where(e => e.Value == statusName)?.ToList();
            var intensity = statusAdd != null ? statusAdd[0].Intensity : 1;

            if (effect.Damage.IsSimple)
                dmg = _evalSimple(effect.Damage.Simple);
            else
                dmg = _evalFormula(effect.Damage.Formula, castable, target, source);

            return new DamageOutput() { Amount = (dmg*intensity*target.Stats.DamageModifier), Type = type, Flags = effect.Damage.Flags, Element = castable.Element };
        }

        /// <summary>
        /// Calculate the healing for a status tick.
        /// </summary>
        /// <param name="castable">Castable responsible for the status</param>
        /// <param name="effect">ModifierEffect structure for the status</param>
        /// <param name="target">Target for the healing (e.g. the player or creature with the status)</param>
        /// <param name="source">Original source of the status</param>
        /// <param name="statusName">The name of the status</param>
        /// <returns></returns>
        public static double CalculateHeal(Xml.Castable castable, Xml.ModifierEffect effect, Creature target, Creature source, string statusName)
        {
            // Defaults
            double heal = 0;

            if (effect?.Heal == null) return heal;

            var statusAdd = castable?.Effects?.Statuses?.Add?.Where(e => e.Value == statusName)?.ToList();
            var intensity = statusAdd != null ? statusAdd[0].Intensity : 1;

            if (effect.Heal.IsSimple)
                heal = _evalSimple(effect.Heal.Simple);
            else
                heal = _evalFormula(effect.Damage.Formula, castable, target, source);

            return heal * intensity * target.Stats.HealModifier;

        }

    }
}
