using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kZKarthus.Modes;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

using Settings = kZKarthus.Config.Modes.Combo;
using SettingsPred = kZKarthus.Config.Modes.PredictionMenu;

namespace kZKarthus
{
    public static class ModeManager
    {
        private static List<ModeBase> Modes { get; set; }

        static ModeManager()
        {
            // Initialize properties
            Modes = new List<ModeBase>();

            // Load all modes manually since we are in a sandbox which does not allow reflection
            // Order matter here! You would want something like PermaActive being called first
            Modes.AddRange(new ModeBase[]
            {
                new PermaActive(),
                new Combo(),
                new Harass(),
                new LaneClear(),
                new JungleClear(),
                new LastHit(),
                new Flee()
            });

            // Listen to events we need
            Game.OnTick += OnTick;
        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        public static float GetTargetHealth(AIHeroClient playerInfo)
        {
            if (playerInfo.IsVisible)
                return playerInfo.Health;

            var predictedhealth = playerInfo.Health + playerInfo.HPRegenRate * (SpellManager.R.CastDelay / 1000);

            return predictedhealth > playerInfo.MaxHealth ? playerInfo.MaxHealth : predictedhealth;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            var DMG = 0f;

            if (SpellManager.R.Level == 1)
            {
                DMG = 250f + (0.60f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.R.Level == 2)
            {
                DMG = 400f + (0.60f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.R.Level == 3)
            {
                DMG = 550f + (0.60f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }

            return DMG;
        }

        public static void AutoCast()
        {
            if (Settings.useAC)
            {
                if (Player.Instance.IsDead || Player.Instance.IsZombie)
                {
                    if (Settings.UseQ && SpellManager.Q.IsReady())
                    {
                        var Target = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Magical);
                        var Pred = SpellManager.Q.GetPrediction(Target);
                        if (Target != null && Target.IsValid)
                        {
                            if (Pred.HitChance >= PredQ())
                            {
                                SpellManager.Q.Cast(Pred.CastPosition);
                            }
                        }
                    }
                    if (Settings.UseW && SpellManager.W.IsReady())
                    {
                        var Target = TargetSelector.GetTarget(SpellManager.W.Range, DamageType.Magical);
                        var Pred = SpellManager.W.GetPrediction(Target);
                        if (Target != null && Target.IsValid)
                        {
                            if (Pred.HitChance >= PredW())
                            {
                                SpellManager.W.Cast(Pred.CastPosition);
                            }
                        }
                    }
                }
            }

            if (Settings.useUltKS)
            {
                var EnemiesTxt = "";

                //Show Enemies
                var enemies = HeroManager.Enemies.Where(a => a.IsEnemy && a.IsValid);
                Vector2 WTS = Drawing.WorldToScreen(Player.Instance.Position);

                foreach (var enemy in enemies)
                {
                    if ((GetTargetHealth(enemy) - RDamage(enemy)) <= 0)
                    {
                        if (!enemy.IsDead)
                        {
                            EnemiesTxt = enemy.BaseSkinName + " | ";

                        }
                    }
                }

                if (EnemiesTxt != "")
                {
                    if (SpellManager.R.IsLearned && SpellManager.R.IsReady())
                    {
                        var Target = TargetSelector.GetTarget(SpellManager.R.Range, DamageType.Magical);
                        var Pred = SpellManager.R.GetPrediction(Target);
                        if (Target != null && Target.IsValid)
                        {
                            SpellManager.R.Cast(Pred.CastPosition);
                        }
                    }
                }

            }
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

        public static void SafeMana()
        {
            if (Settings.saveE)
            {
                if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) || !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                {
                    if (!SpellManager.E.IsReady() || Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState != 2) // 1 = off , 2 = on
                        return;

                    var Range = SpellManager.E.Range + 100;
                    var Etarget = TargetSelector.GetTarget(Range, DamageType.Magical);

                    if (SpellManager.E.IsReady() && Etarget == null)
                    {
                        SpellManager.E.Cast();
                    }
                }
            }
        }

        private static void OnTick(EventArgs args)
        {
            try
                
            {
                AutoCast();
                // Execute all modes
                Modes.ForEach(mode =>
                {
                    AutoCast();
                    // Precheck if the mode should be executed
                    if (mode.ShouldBeExecuted())
                    {
                        // Execute the mode
                        ModeManager.AutoCast();
                        mode.Execute();
                        //Program.UltKS();
                    }
                    else SafeMana();

                });
                                
            }catch (Exception e)
                {
                    // Please enable the debug window to see and solve the exceptions that might occur!
                    //Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, e);
                }
        }
    }
}
