using System;
using System.Linq;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

using Settings = kZKarthus.Config.Modes.LaneClear;
using SettingsCombo = kZKarthus.Config.Modes.Combo;

namespace kZKarthus.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        float QDamage(Obj_AI_Base target)
        {
            var DMG = 0f;

            if (SpellManager.Q.Level == 1)
            {
                DMG = 75f + (0.55f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 2)
            {
                DMG = 110f + (0.55f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 3)
            {
                DMG = 140f + (0.55f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 4)
            {
                DMG = 180f + (0.55f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 5)
            {
                DMG = 220f + (0.55f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }

            return DMG;
        }

        public override void Execute()
        {
            // TODO: Add laneclear logic here
            if (Settings.UseQ && Player.Instance.ManaPercent >= Settings.QMana && !Settings.UseQOnlyK)
            {
                var allMinionsQ = ObjectManager.Get<Obj_AI_Base>().Where(t => Q.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy).OrderBy(t => t.Health);
                //var rangedMinionsR = ObjectManager.Get<Obj_AI_Base>().Where(t => R.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy && t.IsRanged).OrderBy(t => t.Health);
                if (allMinionsQ == null)
                {
                    return;
                }

                var QLocation = Prediction.Position.PredictCircularMissileAoe(allMinionsQ.ToArray(), Q.Range, Q.Radius, Q.CastDelay, Q.Speed, Player.Instance.Position);//R.GetPrediction(allMinionsR.FirstOrDefault());
                //var r2Location = Prediction.Position.PredictCircularMissileAoe(rangedMinionsR.ToArray(), R.Range, R.Radius, R.CastDelay, R.Speed, PlayerInstance.Position);//R.GetPrediction(rangedMinionsR.FirstOrDefault());
                var useQonMinion = Settings.UseQ;

                if (useQonMinion)
                {
                    if (QLocation == null)
                    {
                        return;
                    }
                    else
                    {
                        if (useQonMinion)
                        {
                            foreach (var pred in QLocation)
                            {
                                if (pred.CollisionObjects.Count() > 0 && pred.CollisionObjects.Count() <= 1)
                                {
                                    if (Q.IsReady() && Q.IsInRange(pred.CastPosition))
                                    {
                                        Q.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var pred in QLocation)
                            {
                                if (pred.CollisionObjects.Count() == 2)
                                {
                                    if (Q.IsReady() && Q.IsInRange(pred.CastPosition))
                                    {
                                        Q.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //LASTHIT


            if (Settings.UseQ && Player.Instance.ManaPercent >= Settings.QMana && Settings.UseQOnlyK)
            {
                var allMinionsQLast = ObjectManager.Get<Obj_AI_Base>().Where(t => Q.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy && (t.Health <= QDamage(t))).OrderBy(t => t.Health);

                if (allMinionsQLast == null)
                {
                    return;
                }

                var QLocation = Prediction.Position.PredictCircularMissileAoe(allMinionsQLast.ToArray(), Q.Range, Q.Radius, Q.CastDelay, Q.Speed, Player.Instance.Position);//R.GetPrediction(allMinionsR.FirstOrDefault());
                //var r2Location = Prediction.Position.PredictCircularMissileAoe(rangedMinionsR.ToArray(), R.Range, R.Radius, R.CastDelay, R.Speed, PlayerInstance.Position);//R.GetPrediction(rangedMinionsR.FirstOrDefault());
                var useQonMinion = Settings.UseQ;

                if (useQonMinion)
                {
                    if (QLocation == null)
                    {
                        return;
                    }
                    else
                    {
                        if (useQonMinion)
                        {
                            foreach (var pred in QLocation)
                            {
                                if (pred.CollisionObjects.Count() > 0 && pred.CollisionObjects.Count() <= 3)
                                {
                                    if (Q.IsReady() && Q.IsInRange(pred.CastPosition))
                                    {
                                        Q.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var pred in QLocation)
                            {
                                if (pred.CollisionObjects.Count() == 2)
                                {
                                    if (Q.IsReady() && Q.IsInRange(pred.CastPosition))
                                    {
                                        Q.Cast(pred.CastPosition);
                                    }
                                }
                            }
                        }
                    }
                }

            
            }


            //-------

            if (Settings.UseE)
            {
                var allMinionsE = ObjectManager.Get<Obj_AI_Base>().Where(t => E.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy).OrderBy(t => t.Health);
                //var rangedMinionsR = ObjectManager.Get<Obj_AI_Base>().Where(t => R.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy && t.IsRanged).OrderBy(t => t.Health);
                if (allMinionsE == null)
                {
                    return;
                }

                if (allMinionsE.Count() >= Settings.EMinMinions)
                {
                    if (Settings.UseE && Player.Instance.ManaPercent >= Settings.EMana)
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

            }


        }
    }
}
