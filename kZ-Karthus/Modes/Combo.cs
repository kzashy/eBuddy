using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

// Using the config like this makes your life easier, trust me
using Settings = kZKarthus.Config.Modes.Combo;
using SettingsPred = kZKarthus.Config.Modes.PredictionMenu;

namespace kZKarthus.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        private static HitChance PredQ()
        {
            var mode = SettingsPred.QPrediction;
            switch (mode)
            {
                case 0:
                    return HitChance.Low;
                case 1:
                    return HitChance.Medium;
                case 2:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }

        private static HitChance PredW()
        {
            var mode = SettingsPred.WPrediction;
            switch (mode)
            {
                case 0:
                    return HitChance.Low;
                case 1:
                    return HitChance.Medium;
                case 2:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }

        public override void Execute()
        {
            // TODO: Add combo logic here
            // See how I used the Settings.UseQ here, this is why I love my way of using
            // the menu in the Config class!
            if (Settings.UseQ && Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var Pred = Q.GetPrediction(Target);
                if (Target != null && Target.IsValid)
                {
                    if (Pred.HitChance >= PredQ())
                    {
                        Q.Cast(Pred.CastPosition);
                    }
                }
            }
            if (Settings.UseE && E.IsReady())
            {
                var Target = TargetSelector.GetTarget(E.Range+30, DamageType.Magical);
                if (Target != null && Target.IsValid)
                {
                    if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1) // 1 = off , 2 = on
                        E.Cast();
                }
                else
                {
                    if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2) // 1 = off , 2 = on
                        E.Cast();
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                var Target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                var Pred = W.GetPrediction(Target);
                if (Target != null && Target.IsValid)
                {
                    if (Pred.HitChance >= PredW())
                    {
                        W.Cast(Pred.CastPosition);
                    }
                }
            }
        }

    }
}
