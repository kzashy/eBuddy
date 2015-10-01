using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

// Using the config like this makes your life easier, trust me
using Settings = kZKarthus.Config.Modes.Harass;
using SettingsPred = kZKarthus.Config.Modes.PredictionMenu;
using SettingsCombo = kZKarthus.Config.Modes.Combo;

namespace kZKarthus.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
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
            // TODO: Add harass logic here
            // See how I used the Settings.UseQ and Settings.Mana here, this is why I love
            // my way of using the menu in the Config class!
            if (Settings.UseQ && Player.Instance.ManaPercent > Settings.QMana && Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                var Pred = Q.GetPrediction(Target);
                if (Target != null && Target.IsValid)
                {
                    if (Pred.HitChance == PredQ())
                    {
                        Q.Cast(Pred.CastPosition);
                    }
                }
            }

            if (Settings.UseE && Player.Instance.ManaPercent > Settings.EMana && E.IsReady())
            {
                var Target = TargetSelector.GetTarget(E.Range + 30, DamageType.Magical);
                if (Target != null && Target.IsValid)
                {
                    if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1) // 1 = off , 2 = on
                        E.Cast();
                }
                else
                {
                    if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2) // 1 = off , 2 = on
                        if (SettingsCombo.saveE)
                        {
                            E.Cast();
                        }
                }
            }
            else
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2) // 1 = off , 2 = on
                    if (SettingsCombo.saveE)
                    {
                        E.Cast();
                    }
            }
            if (Settings.UseW && Player.Instance.ManaPercent > Settings.WMana && W.IsReady())
            {
                var Target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                var Pred = W.GetPrediction(Target);
                if (Target != null && Target.IsValid)
                {
                    if (Pred.HitChance == PredW())
                    {
                        W.Cast(Pred.CastPosition);
                    }
                }
            }
        }
    }
}
