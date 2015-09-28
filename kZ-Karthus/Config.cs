using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace kZKarthus
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "kZ-Karthus";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("WkZ-Karthus");
            Menu.AddSeparator();
            Menu.AddLabel("Version : 1.0.1.94");
            Menu.AddLabel("Author : Kzashy");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                // Initialize the menu
                // Initialize all modes
                // Combo
                Menu = Config.Menu.AddSubMenu("Combo");
                Combo.Initialize();

                // Harass
                Menu = Config.Menu.AddSubMenu("Harass");
                Harass.Initialize();

                // Harass
                Menu = Config.Menu.AddSubMenu("LaneClear");
                LaneClear.Initialize();

                // Draw
                Menu = Config.Menu.AddSubMenu("Drawing");
                DrawingMenu.Initialize();

                //Gap Close
                Menu = Config.Menu.AddSubMenu("GapClose");
                GapCloseMenu.Initialize();

                //Gap Close
                Menu = Config.Menu.AddSubMenu("Prediction");
                PredictionMenu.Initialize();

            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useAA;
                private static readonly CheckBox _useAC;
                private static readonly CheckBox _useUltKS;
                private static readonly CheckBox _saveE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool useAA
                {
                    get { return _useAA.CurrentValue; }
                }
                public static bool useAC
                {
                    get { return _useAC.CurrentValue; }
                }
                public static bool useUltKS
                {
                    get { return _useUltKS.CurrentValue; }
                }
                public static bool saveE
                {
                    get { return _saveE.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo Options");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    Menu.AddSeparator();

                    Menu.AddGroupLabel("AA");
                    _useAA = Menu.Add("useAA", new CheckBox("Use AA on Combo", false));

                    Menu.AddSeparator();
                    Menu.AddGroupLabel("AutoCombo");
                    _useAC = Menu.Add("useAC", new CheckBox("Death AutoCombo", true));

                    Menu.AddSeparator();
                    Menu.AddGroupLabel("Misc");
                    _useUltKS = Menu.Add("useUltKS", new CheckBox("Use Ultimate KS", true));
                    _saveE = Menu.Add("ESave", new CheckBox("Auto switch E to save MP (Turn off if you have problems with E use.)", true));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _harassManaQ;
                private static readonly Slider _harassManaW;
                private static readonly Slider _harassManaE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static int QMana
                {
                    get { return _harassManaQ.CurrentValue; }
                }
                public static int WMana
                {
                    get { return _harassManaW.CurrentValue; }
                }
                public static int EMana
                {
                    get { return _harassManaE.CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass Options");
                    _useQ = Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("harassUseW", new CheckBox("Use W", false));
                    _useE = Menu.Add("harassUseE", new CheckBox("Use E"));
                    Menu.AddSeparator();

                    Menu.AddGroupLabel("Mana");
                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    _harassManaQ = Menu.Add("harassManaQ", new Slider("Q Maximum mana usage in percent ({0}%)", 60));
                    _harassManaW = Menu.Add("harassManaW", new Slider("W Maximum mana usage in percent ({0}%)", 70));
                    _harassManaE = Menu.Add("harassManaE", new Slider("E Maximum mana usage in percent ({0}%)", 70));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useQOnlyK;
                private static readonly CheckBox _useE;
                private static readonly Slider _laneclearManaQ;
                private static readonly Slider _laneclearManaE;
                private static readonly Slider _laneclearEMinMinions;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseQOnlyK
                {
                    get { return _useQOnlyK.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static int QMana
                {
                    get { return _laneclearManaQ.CurrentValue; }
                }
                public static int EMana
                {
                    get { return _laneclearManaE.CurrentValue; }
                }
                public static int EMinMinions
                {
                    get { return _laneclearEMinMinions.CurrentValue; }
                }

                static LaneClear()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("LaneClear Options");
                    _useQ = Menu.Add("laneclearUseQ", new CheckBox("Use Q"));
                    _useQOnlyK = Menu.Add("laneclearUseQOnlyK", new CheckBox("Try use Q only LastHit"));
                    _useE = Menu.Add("laneclearUseE", new CheckBox("Use E"));
                    Menu.AddSeparator();

                    Menu.AddGroupLabel("Mana");
                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    _laneclearManaQ = Menu.Add("laneclearManaQ", new Slider("Q Maximum mana usage in percent ({0}%)", 60));
                    _laneclearManaE = Menu.Add("laneclearManaE", new Slider("E Maximum mana usage in percent ({0}%)", 60));
                    Menu.AddSeparator();

                    Menu.AddGroupLabel("Minions");
                    _laneclearEMinMinions = Menu.Add("EMinMinions", new Slider("Minimum count of minions ({0}) to use E", 3, 1, 10));
                }

                public static void Initialize()
                {
                }
            }

            public static class DrawingMenu
            {
                private static readonly CheckBox _DrawQ;
                private static readonly CheckBox _DrawW;
                private static readonly CheckBox _DrawE;
                private static readonly CheckBox _DrawMyRange;
                private static readonly CheckBox _DrawEnemyRange;
                private static readonly CheckBox _DrawRalert;
                private static readonly CheckBox _DrawLH;
                private static readonly CheckBox _ChampInfo;
                private static readonly Slider _posX;
                private static readonly Slider _posY;

                public static bool DrawQ
                {
                    get { return _DrawQ.CurrentValue; }
                }
                public static bool DrawW
                {
                    get { return _DrawW.CurrentValue; }
                }
                public static bool DrawE
                {
                    get { return _DrawE.CurrentValue; }
                }
                public static bool DrawMyRange
                {
                    get { return _DrawMyRange.CurrentValue; }
                }
                public static bool DrawEnemyRange
                {
                    get { return _DrawEnemyRange.CurrentValue; }
                }
                public static bool DrawRalert
                {
                    get { return _DrawRalert.CurrentValue; }
                }
                public static bool DrawLH
                {
                    get { return _DrawLH.CurrentValue; }
                }
                public static bool ChampInfo
                {
                    get { return _ChampInfo.CurrentValue; }
                }
                public static float PosX
                {
                    get { return _posX.CurrentValue; }
                }
                public static float PosY
                {
                    get { return _posY.CurrentValue; }
                }

                static DrawingMenu()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Drawing Options");
                    _DrawQ = Menu.Add("DrawQ", new CheckBox("Draw Q Range"));
                    _DrawW = Menu.Add("DrawW", new CheckBox("Draw W Range"));
                    _DrawE = Menu.Add("DrawE", new CheckBox("Draw E Range", false));
                    Menu.AddSeparator();

                    Menu.AddGroupLabel("Ultimate R Notification");
                    _DrawRalert = Menu.Add("DrawRalert", new CheckBox("Draw R Notification when Ult can kill a Enemy"));
                    Menu.AddSeparator();

                    Menu.AddGroupLabel("Misc");
                    _DrawLH = Menu.Add("drawLastHit", new CheckBox("Draw LastHit Circle"));
                    _DrawMyRange = Menu.Add("DrawMyRange", new CheckBox("Draw My Range"));
                    _DrawEnemyRange = Menu.Add("DrawEnemyRange", new CheckBox("Draw Enemy Range"));
                    _ChampInfo = Menu.Add("ChampInfo", new CheckBox("Show Champion Info."));
                    _posX = Menu.Add("posX", new Slider("Champion Info - Pos X",10,0,100));
                    _posY = Menu.Add("posY", new Slider("Show Champion - Pos Y",10,0,100));


                }

                public static void Initialize()
                {
                }
            }

            public static class GapCloseMenu
            {
                private static readonly CheckBox _UseWonGap;

                public static bool UseWonGap
                {
                    get { return _UseWonGap.CurrentValue; }
                }

                static GapCloseMenu()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("GapClose Options");
                    _UseWonGap = Menu.Add("UseWonGap", new CheckBox("Use W on GapClose"));
                }

                public static void Initialize()
                {
                }
            }

            public static class PredictionMenu
            {
                private static readonly Slider _QPrediction;
                private static readonly Slider _WPrediction;

                public static int QPrediction
                {
                    get { return _QPrediction.CurrentValue; }
                }
                public static int WPrediction
                {
                    get { return _WPrediction.CurrentValue; }
                }

                static PredictionMenu()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Prediction Options");
                    Menu.AddSeparator();
                    Menu.AddGroupLabel("Q HitChante :");
                    _QPrediction = Menu.Add("QPrediction", new Slider("Q HitChance : ({0})", 2, 0, 2));
                    var qMode = new[] { "Low (Fast Casting)", "Medium", "High (Slow Casting)" };
                    _QPrediction.DisplayName = qMode[_QPrediction.CurrentValue];

                    _QPrediction.OnValueChange +=
                        delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                        {
                            sender.DisplayName = qMode[changeArgs.NewValue];
                        };


                    Menu.AddSeparator();
                    Menu.AddGroupLabel("W HitChante :");
                    _WPrediction = Menu.Add("WPrediction", new Slider("W HitChance : ({0})", 2, 0, 2));
                    var wMode = new[] { "Low (Fast Casting)", "Medium", "High (Slow Casting)" };
                    _WPrediction.DisplayName = wMode[_WPrediction.CurrentValue];

                    _WPrediction.OnValueChange +=
                        delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                        {
                            sender.DisplayName = wMode[changeArgs.NewValue];
                        };
                }

                public static void Initialize()
                {
                }
            }

        }
    }
}
