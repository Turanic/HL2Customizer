using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Globalization;
using System.Net;
using System.IO;

namespace HL2Customizer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Globals definition

        ClientSchemeManager csm;
        SourceSchemeManager ssm;
        CfgManager cfgm;
        HudAnimationsManager ham;
        HudLayoutManager hlm;
        WeaponScriptManager wsm;
        GameMenuManager gmm;
        BGsManager bgm;
        DSPManager dspm;
        LangManager lgm;
        BrandSaver brand;

        //BASIC CONFIGS
        #region basicconfigs
        String[] Colors = new String[] {
            		"Black","White", "Red", "Orange",
                    "Yellow", "DarkGreen", "Green",
                    "Lime", "Jade", "Cyan",
                    "Turquoise", "SkyBlue", "Blue",
                    "Navy", "Purple", "Magenta",
                    "Brown", "Pink", "Silver"	
        };

        Tuple<String, char>[] Crosshairs = new Tuple<String, char>[] {
            Tuple.Create("dot", 'b'),
            Tuple.Create("double dot", 'o'),
            Tuple.Create("tiny dot", 'i'),
            Tuple.Create("cross", 'd'),
            Tuple.Create("little cross", 'c'),
            Tuple.Create("X cross", 'g'),
            Tuple.Create("Empty cross", 'k'),
            Tuple.Create("cross + dot", 'r'),
            Tuple.Create("Aiming cross", 'j'),
            Tuple.Create("demi-cross", 'l'),
            Tuple.Create("demi-empty cross", 'm'),
            Tuple.Create("square", 'e'),
            Tuple.Create("box", 's'),
            Tuple.Create("cypher", 'n'),
            Tuple.Create("Xcypher", 't'),
            Tuple.Create("spikes", 'u'),
            Tuple.Create("spikes + dot", 'v'),
            Tuple.Create("vertical spikes", 'w'),
            Tuple.Create("vertical spikes + dot", 'x'),
            Tuple.Create("circle", 'h'),
            Tuple.Create("circle + dot", 'y'),
            Tuple.Create("brackets + dot", 'z'),
            Tuple.Create("bar", 'f'),
            Tuple.Create("double bar", 'q'),
            Tuple.Create("default", 'p'),
            Tuple.Create("void", ' '),
        };

        UserPaths Paths;
        #endregion

        //ADVANCED CONFIG
        #region advancedconfigs
        Tuple<string, string>[] Models = new Tuple<string, string>[] {
            Tuple.Create("rebel black male","models/humans/group03/male_05.mdl"),
            Tuple.Create("rebel white male","models/humans/group03/male_01.mdl"),
            Tuple.Create("rebel brown female","models/humans/group03/female_07.mdl"),
            Tuple.Create("rebel white female","models/humans/group03/female_01.mdl"),
            Tuple.Create("combine soldier","models/combine_soldier.mdl"),
            Tuple.Create("prison guard","models/combine_soldier_prisonguard.mdl"),
            Tuple.Create("metrocop","models/police.mdl"),
            Tuple.Create("supersoldier","models/combine_super_soldier.mdl")
        };
        Tuple<string, string>[] DefaultWeapons = new Tuple<string, string>[] {
            Tuple.Create("Gravity gun","weapon_physcannon"),
            Tuple.Create("Crowbar","weapon_crowbar"),
            Tuple.Create("Stunstick","weapon_stunstick"),
            Tuple.Create("Grenades","weapon_frag"),
            Tuple.Create("Smg","weapon_smg1"),
            Tuple.Create("Pistol","weapon_pistol")
        };
        Tuple<string, string>[] NonDefaultWeapons = new Tuple<string, string>[] {
            Tuple.Create("Gravity gun","weapon_physcannon"),
            Tuple.Create("Crowbar","weapon_crowbar"),
            Tuple.Create("Stunstick","weapon_stunstick"),
            Tuple.Create("Grenades","weapon_frag"),
            Tuple.Create("Smg","weapon_smg1"),
            Tuple.Create("Pistol","weapon_pistol"),
            Tuple.Create("Shotgun","weapon_shotgun"),
            Tuple.Create("Magnum","weapon_357"),
            Tuple.Create("Ar2","weapon_ar2"),
            Tuple.Create("Crossbow","weapon_crossbow"),
            Tuple.Create("RPG","weapon_rpg")

        };
        Tuple<string, string>[] Weapons;
        string[] Rates = new string[] {
            "Low rate",
            "Default rate",
            "Normal rate",
            "League rate",
            "Speed rate",
            "ZG/UG rate",
            "Cheater rate"
        };
        #endregion

        //MENU EDITOR
        #region menueditor
        MenuElement[] BasicLabels;
        MenuElement[] DemoLabels;
        MenuElement[] ConsoleLabels;
        MenuElement[] PerfLabels;
        MenuElement[] TeamLabels;
        MenuElement[] PmsLabels;
        MenuElement[] AdminLabels;

        MenuElement[][] LabelsListControler;
        System.Windows.Controls.CheckBox[] checkboxs;

        bool keeper; // needed for debug the loading

        Tuple<string, byte, byte, byte>[] MenuColors = new Tuple<string, byte, byte, byte>[] {
            Tuple.Create("White",  (byte)160, (byte)160 , (byte)160),
            Tuple.Create("Red",    (byte)160, (byte)20 ,   (byte)20),
            Tuple.Create("Orange", (byte)160, (byte)80 ,  (byte)0),
            Tuple.Create("Yellow", (byte)160, (byte)160 , (byte)0),
            Tuple.Create("Green",  (byte)20,   (byte)160 , (byte)20),
            Tuple.Create("Cyan",   (byte)0,   (byte)160 ,  (byte)160),
            Tuple.Create("Blue",   (byte)0,   (byte)50 ,   (byte)160),
            Tuple.Create("Purple", (byte)80,  (byte)0 ,   (byte)160),
            Tuple.Create("Grey",   (byte)60,  (byte)60 ,   (byte)60),
            Tuple.Create("Black",  (byte)10,  (byte)10 ,  (byte)10),
            };

        string BGMusicPath; //needed to move the bg music file to the right location when apply
        #endregion

        //FONT EDITOR
        #region fonteditor
        string[] Fonts = new string[]
        {
            "Arial",
            "Courier New",
            "Defused",
            //"Dodger", To big :/
            "DS-Digital",
            "Manteka",
            "RUSREturn to castle",
            "Russo One",
            "Turok",
            "Verdana",
        };
        string[] Icons = new string[]
        {
            "Quake style",
            "CAL style",
            "CU style",
        };
        #endregion

        //AUX EDITOR
        #region auxeditor
        string[] AuxLblPos = new string[] { "UP", "DOWN", "NONE" };
        #endregion

        //WPN EDITOR
        #region weaponeditor
        Tuple<string, char>[] WpnsTypes = new Tuple<string, char>[]
        {
            Tuple.Create("Gravity gun", 'm'),
            Tuple.Create("Crowbar", 'c'),
            Tuple.Create("Stunstick", 'n'),
            Tuple.Create("Grenade", 'k'),
            Tuple.Create("Mine", 'o'),
            Tuple.Create("Pistol", 'd'),
            Tuple.Create("SMG", 'a'),
            Tuple.Create("AR2", 'l'),
            Tuple.Create("Shotgun", 'b'),
            Tuple.Create("Magnum", 'e'),
            Tuple.Create("Crossbow", 'g'),
            Tuple.Create("RPG", 'i'),
        };

        #endregion

        #endregion

        public MainWindow()
        {
            System.Threading.Thread.Sleep(1500);
            // WPF Bug Workaround: while we have no WPF window open we can`t show MessageBox.
            Window WpfBugWindow = new Window()
            {
                AllowsTransparency = true,
                Background = System.Windows.Media.Brushes.Transparent,
                WindowStyle = WindowStyle.None,
                Top = 0,
                Left = 0,
                Width = 1,
                Height = 1,
                ShowInTaskbar = false
            };

            WpfBugWindow.Show();
            try
            {
                
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("http://turanic.com/HL2Customizer/lastVersion.txt");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();

                if (content.Substring(0, 3) != HL2Customizer.Resources.resfile.Version.Substring(0, 3))
                {
                    System.Windows.MessageBox.Show("HL2Customizer v." + content + " is available online!", "New version!", MessageBoxButton.OK);
                } 
            }
            catch
            {
                System.Windows.MessageBox.Show("The software can't access the main server, you may need it for some options :/", "No internet access", MessageBoxButton.OK);
            }
            SavedData save = null;
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\previous.hcd"))
            {
                save = Serializer.DeSerializeHudData("previous");
                if (save.Infos.HudVersion != HL2Customizer.Resources.resfile.Version)
                {
                    System.Windows.MessageBox.Show("Problem, Your last hud was created using version " + save.Infos.HudVersion +
                        " and is no longer compatible. The default hud will be loaded instead", "HUD obselete", MessageBoxButton.OK);
                    save = null;
                }
            }
            if (save == null)
            {
                save = new SavedData(
                new HudInformations("New"),
                new ClientSchemeManager(),
                new SourceSchemeManager(),
                new HudLayoutManager(),
                new HudAnimationsManager(),
                new WeaponScriptManager(),
                new GameMenuManager(),
                new BGsManager(),
                new CfgManager(),
                new DSPManager(),
                new LangManager(),
                new BrandSaver());
            }
            Initialize(save);
            WpfBugWindow.Close();
        }

        public MainWindow(SavedData Save)
        {
            Initialize(Save);
        }

        private void Initialize(SavedData save)
        {
            csm = save.Csm;
            ssm = save.Ssm;
            cfgm = save.Cfgm;
            ham = save.Ham;
            hlm = new HudLayoutManager(save.Hlm);
            wsm = new WeaponScriptManager(save.Wsm);
            gmm = save.Gmm;
            bgm = save.BGm;
            dspm = save.Dspm;
            lgm = save.Lgm;
            brand = save.Brand;

            InitializeComponent();
            #region Initilization

            //BASIC CONFIGS
            #region basicconfigs
            basicConfigs_title.Content = "HL2Customizer Alpha v." + HL2Customizer.Resources.resfile.Version;

            //Crosshair box load
            for (int i = 0; i < Crosshairs.Count(); i++)
            {
                Tuple<String, char> crosshair = Crosshairs[i];
                basicConfigs_CrosshairBox.Items.Add(crosshair.Item1);
               
                
                if (crosshair.Item2 == hlm.CrosshairType) basicConfigs_CrosshairBox.SelectedIndex = i;
            }

            //Colors boxs load
            for (int i = 0; i < Colors.Count(); i++)
            {
                string color = Colors[i];
                basicConfigs_mainColorBox.Items.Add(color);
                basicConfigs_secColorBox.Items.Add(color);
                basicConfigs_crossColorBox.Items.Add(color);
                basicConfigs_auxCrossColorBox.Items.Add(color);
                if (color == csm.MainColor) basicConfigs_mainColorBox.SelectedIndex = i;
                if (color == csm.SecondaryColor) basicConfigs_secColorBox.SelectedIndex = i;
                if (color == csm.CrossColor) basicConfigs_crossColorBox.SelectedIndex = i;
                if (color == csm.SecCrossColor) basicConfigs_auxCrossColorBox.SelectedIndex = i;
            }

            //Checkbox loads
            basicConfigs_dontChangeCrosshairRB.IsChecked = !hlm.AdvCrosshair;
            basicConfigs_outlinedRB.IsChecked = csm.OutlinedCrosshairs;
            basicConfigs_keepQuickInfosRB.IsChecked = cfgm.QuickInfos;

            switch (csm.CrosshairSize)
            {
                case 'S': basicConfigs_xhairSizeSlider.Value = 0; break;
                case 'M': basicConfigs_xhairSizeSlider.Value = 1; break;
                case 'L': basicConfigs_xhairSizeSlider.Value = 2; break;
                case 'G': basicConfigs_xhairSizeSlider.Value = 3; break;
                default: goto case 'S';
            }

            string path = PathSaver.LoadPath();
            if (path != null) basicConfigs_pathTextBox.Text = path;
            #endregion

            //ADVANCED CONFIGS
            #region advancedconfigs

            //Weapon list fill
            Weapons = NonDefaultWeapons;
            advancedconfigs_noDfltWeapCB.IsChecked = true;
            int idx = 0;
            while (idx < Weapons.Count())
            {
                if (Weapons[idx].Item2 == cfgm.StartWeapon) break;
                idx++;
            }
            if (idx < DefaultWeapons.Count())
            {
                Weapons = DefaultWeapons;
                advancedconfigs_noDfltWeapCB.IsChecked = false;
            }
            FillWeaponList();
            advancedconfigs_WeaponsBox.SelectedIndex = idx;

            //Model list fill
            foreach (Tuple<string, string> e in Models)
                advancedconfigs_ModelsBox.Items.Add(e.Item1);
            for (int i = 0; i < Models.Count(); i++)
                if (Models[i].Item2 == cfgm.Model) advancedconfigs_ModelsBox.SelectedIndex = i;

            // other options
            advancedconfigs_autoswitchRB.IsChecked = !cfgm.Autoswitch;
            advancedconfigs_consoleRB.IsChecked = cfgm.ConsoleAtstart;
            advancedconfigs_motdRB.IsChecked = cfgm.DisableMotd;
            advancedconfigs_consoleRB.IsChecked = cfgm.ConsoleAtstart;
            advancedconfigs_filterRB.IsChecked = cfgm.EnableConsoleFilter;
            advancedconfigs_sprayRB.IsChecked = cfgm.DisableSpray;
            switch (ham.RedScreenDegree)
            {
                case 200: advancedconfigs_redScreenScroller.Value = 0; break;
                case 100: advancedconfigs_redScreenScroller.Value = 1; break;
                case 40: advancedconfigs_redScreenScroller.Value = 2; break;
                case 0: advancedconfigs_redScreenScroller.Value = 3; break;
                default: advancedconfigs_redScreenScroller.Value = 0; break;
            }
            if(dspm.SoundVolume == 1.6f) advancedconfigs_bipLenghtScroller.Value = 0;
            else if (dspm.SoundVolume == 0.8f) advancedconfigs_bipLenghtScroller.Value = 1;
            else if (dspm.SoundVolume == 0.4f) advancedconfigs_bipLenghtScroller.Value = 2;
            else advancedconfigs_bipLenghtScroller.Value = 3;

            foreach (string e in Rates)
                advancedconfigs_RateBox.Items.Add(e);
            advancedconfigs_RateBox.SelectedIndex = 1;

            advancedconfigs_brandButton.IsEnabled = !brand.Enabled;
            advancedconfigs_delBrandButton.IsEnabled = brand.Enabled;
            #endregion

            //MENU EDITOR
            #region menueditor

            //menu label editor
            keeper = true;
            //Checkboxs loads
            checkboxs = new System.Windows.Controls.CheckBox[]
            {
                menueditor_basicsLabelsCB,
                menueditor_DemoLabelCB,
                menueditor_ConsoleLabelCB,
                menueditor_graphLabelCB,
                menueditor_jointeamLabelCB,
                menueditor_PMSLabelCB,
                menueditor_adminLabelCB
            };

            //Labels load
            int pwd = new Random().Next(0, 1000);
            BasicLabels = new MenuElement[] 
            {
                new MenuElement(1, "Quit", "quit", false),
                new MenuElement(2, "Options", "OpenOptionsDialog", false),
                new MenuElement(3, "Create local server", "OpenCreateMultiplayerGameDialog", false),
                new MenuElement(4, "Find Servers", "OpenServerBrowser", false),
                new MenuElement(5, "", "spacer", true),
                new MenuElement(6, "Disconnect", "Disconnect", true),
                new MenuElement(7, "Reconnect", "engine retry", true),
                new MenuElement(8, "Mute Players", "OpenPlayerListDialog", true),
                new MenuElement(9, "Resume", "ResumeGame", true),
            };
            ConsoleLabels = new MenuElement[] 
            {
                new MenuElement(10, "", "spacer", true),
                new MenuElement(11, "Show console", "engine showconsole", false),
            };
            DemoLabels = new MenuElement[] 
            {
                new MenuElement(15, "==========================", "", false),
                new MenuElement(16, "Show demo player", "engine demoui", false),
                new MenuElement(17, "Take Screenshot", "engine jpeg_quality 100; jpeg", true),
            };
            PerfLabels = new MenuElement[] 
            {
                new MenuElement(20, "==========================", "", true),
                new MenuElement(21, "Net graph", "engine toggle net_graph 0 1 2 3", true),
                new MenuElement(22, "Show FPS", "engine toggle cl_showfps 0 1", true),
                new MenuElement(23, "Show POS", "engine toggle cl_showpos 0 1", true),
            };
            TeamLabels = new MenuElement[] 
            {
                new MenuElement(25, "==========================", "", true),
                new MenuElement(26, "Join Rebels", "engine cl_playermodel models/humans/group03/male_01.mdl;jointeam 3", true),
                new MenuElement(27, "Join Combines", "engine cl_playermodel models/combine_soldier.mdl;jointeam 2", true),
                new MenuElement(28, "Spectate", "engine jointeam 1", true),
            };
            PmsLabels = new MenuElement[] 
            {
                new MenuElement(30, "==========================", "", true),
                new MenuElement(31, "Start 1v1 match", "engine start 1v1", true),
                new MenuElement(32, "Start 2v2 match", "engine start 2v2", true),
                new MenuElement(33, "Start quick match", "engine start shorty", true),
            };
            AdminLabels = new MenuElement[] 
            {
                new MenuElement(35, "==========================", "", true),
                new MenuElement(36, "Admin panel", "engine sm_admin;admin", true),
                new MenuElement(37, "Enable Teamplay", "engine sm_rcon mp_tea mplay 1;rcon mp_teamplay 1", true),
                new MenuElement(38, "Disable Teamplay", "engine sm_rcon mp_teamplay 0;rcon mp_teamplay 0", true),
                new MenuElement(39, "Close server", "engine sm_rcon sv_password " + pwd +";rcon sv_password " + pwd +"; say the server password is now " + pwd, true),
                new MenuElement(40, "Open server", "engine sm_rcon sv_password \\\"\\\";rcon sv_password \\\"\\\"; say The server is now open", true),
                new MenuElement(41, "Show Steam IDs", "engine showconsole;clear;status", true),
            };
            LabelsListControler = new MenuElement[][]
            {
                BasicLabels,
                DemoLabels,
                ConsoleLabels,
                PerfLabels,
                TeamLabels,
                PmsLabels,
                AdminLabels
            };

            //Check boxes
            for (int i = 0; i < gmm.Options.Count(); i++)
                if (gmm.Options[i]) checkboxs[i].IsChecked = true; // make the refresh list by the same way --> bug --> solved by keeper

            menueditor_menuElementsList.SelectedIndex = 0;
            keeper = false;
            if (gmm.MenuLabels.Count(s => s != null) == 0) ChangeContent(BasicLabels, false);
            RefreshList(); // I know it can be made twice, but fuck this...

            //menu color editor
            //Color boxs load
            foreach (Tuple<string, byte, byte, byte> c in MenuColors)
            {
                menueditor_txtBox1.Items.Add(c.Item1);
                menueditor_txtBox2.Items.Add(c.Item1);
            }
            foreach (Tuple<string, byte, byte, byte> c in MenuColors)
                menueditor_bgBox.Items.Add(c.Item1);

            for (int i = 0; i < menueditor_txtBox1.Items.Count; i++)
                if (Convert.ToString(menueditor_txtBox1.Items[i]) == ssm.TxtColor1.Item1) menueditor_txtBox1.SelectedIndex = i;
            for (int i = 0; i < menueditor_txtBox2.Items.Count; i++)
                if (Convert.ToString(menueditor_txtBox2.Items[i]) == ssm.TxtColor2.Item1) menueditor_txtBox2.SelectedIndex = i;
            for (int i = 0; i < menueditor_bgBox.Items.Count; i++)
                if (Convert.ToString(menueditor_bgBox.Items[i]) == ssm.BgColor.Item1) menueditor_bgBox.SelectedIndex = i;

            // Get list of locals backgrounds
            bgm.FillLocalBGs();
            foreach (string file in bgm.Locals2dBGs)
               menueditor_2dbgBox.Items.Add(file);
            foreach (string file in bgm.LocalsMapBGs)
                menueditor_3dbgBox.Items.Add(file);

            menueditor_2dbgBox.SelectedIndex = 0;
            menueditor_3dbgBox.SelectedIndex = 0;
            for (int i = 0; i < menueditor_2dbgBox.Items.Count; i++)
                if (Convert.ToString(menueditor_2dbgBox.Items[i]) == bgm.LastBGName) menueditor_2dbgBox.SelectedIndex = i;
            for (int i = 0; i < menueditor_3dbgBox.Items.Count; i++)
                if (Convert.ToString(menueditor_3dbgBox.Items[i]) == bgm.LastBGName) menueditor_3dbgBox.SelectedIndex = i;

            menueditor_2dbgRB.IsChecked = !bgm.MapBG;
            menueditor_3dbgRB.IsChecked = bgm.MapBG;
            menueditor_smokeEffectCB.IsChecked = bgm.SmokeEffects;
            menueditor_CBMusic.IsChecked = cfgm.BGMusic;

            BGMusicPath = "none"; // if the user did not change music
            #endregion

            //FONT EDITOR
            #region fonteditor
            foreach (string style in Icons)
            {
                fonteditor_healthIconBox.Items.Add(style);
                fonteditor_armorIconBox.Items.Add(style);
            }
            foreach (string font in Fonts)
            {
                fonteditor_TxtFontBox.Items.Add(font);
                fonteditor_NbrFontBox.Items.Add(font);
                fonteditor_TitleFontBox.Items.Add(font);
                fonteditor_ChatFontBox.Items.Add(font);
            }
            fonteditor_TxtFontBox.SelectedIndex = 0;
            fonteditor_NbrFontBox.SelectedIndex = 0;
            fonteditor_TitleFontBox.SelectedIndex = 0;
            fonteditor_ChatFontBox.SelectedIndex = 0;
            for (int i = 0; i < fonteditor_TxtFontBox.Items.Count; i++)
                if (Convert.ToString(fonteditor_TxtFontBox.Items[i]) == csm.TxtFont) fonteditor_TxtFontBox.SelectedIndex = i;
            for (int i = 0; i < fonteditor_NbrFontBox.Items.Count; i++)
                if (Convert.ToString(fonteditor_NbrFontBox.Items[i]) == csm.NbrFont) fonteditor_NbrFontBox.SelectedIndex = i;
            for (int i = 0; i < fonteditor_TitleFontBox.Items.Count; i++)
                if (Convert.ToString(fonteditor_TitleFontBox.Items[i]) == ssm.TitleFont) fonteditor_TitleFontBox.SelectedIndex = i;
            for (int i = 0; i < fonteditor_ChatFontBox.Items.Count; i++)
                if (Convert.ToString(fonteditor_ChatFontBox.Items[i]) == csm.ChatFont) fonteditor_ChatFontBox.SelectedIndex = i;
            fonteditor_DeletePannelsCB.IsChecked = !csm.KeepPannelBG;

            fonteditor_healthIconBox.SelectedIndex = lgm.NoHealthIcon -1;
            fonteditor_armorIconBox.SelectedIndex = lgm.NoAmorIcon - 1;
            #endregion

            //FILE SYSTEM
            #region file

            file_SaveCreatorName.Content = "Creator : " + Environment.UserName;

            string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer";
            File.WriteAllBytes(SavePath + @"\\Default.hcd", HL2Customizer.Resources.resfile.Default);
            File.WriteAllBytes(SavePath + @"\\Example.hcd", HL2Customizer.Resources.resfile.Example);
            RefreshBoxes();

            #endregion

            //AUX. POWER EDITOR
            #region auxeditor
            auxeditor_barWidthSlider.Value = Convert.ToInt32(hlm.AuxPowerConfig[0]);
            auxeditor_barHeightSlider.Value = Convert.ToInt32(hlm.AuxPowerConfig[1]);
            auxeditor_tileWidthSlider.Value = Convert.ToInt32(hlm.AuxPowerConfig[2]);
            auxeditor_gapSlider.Value = Convert.ToInt32(hlm.AuxPowerConfig[3]);

            foreach (string i in AuxLblPos)
                auxeditor_auxlabelpos.Items.Add(i);
            for (int i = 0; i < AuxLblPos.Count(); i++)
                if (AuxLblPos[i] == hlm.AuxPowerLabelPos) auxeditor_auxlabelpos.SelectedIndex = i;

            #endregion

            //WPN EDITOR
            #region weaponeditor

            foreach (Tuple<string, char> crosshair in Crosshairs)
                weaponeditor_CrosshairBox.Items.Add(crosshair.Item1);

            foreach (Tuple<string, char> Wpn in WpnsTypes)
                weaponeditor_wpnType.Items.Add(Wpn.Item1);
            weaponeditor_wpnType.SelectedIndex = 0;

            #endregion

            #endregion
        }

        #region Methodes

        #region apply
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool goodPath;
            try
            {
                goodPath = DefinePaths(basicConfigs_pathTextBox.Text);
            }
            catch (Exception exc)
            {
                System.Windows.MessageBox.Show("Major error, the software can't access your HL2DM folder for unknown reasons :/. Plz report the bug ! \n" + exc.Message, "Error", MessageBoxButton.OK);
                return;
            }

            if (!goodPath)
            {
                System.Windows.MessageBox.Show("The folder you've chosen is not valid... plz browse another one", "Error", MessageBoxButton.OK);
            }
            else if ((bool)basicConfigs_deleteRB.IsChecked)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure your wanna delete all your custom hud?", "Really ?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DeleteDirectory(Paths.HudPath);
                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("Sry dude, there is a bug and I can't delete it by myself. Maybe you should do it yourself?"
                            + " Go check in your custom folder to delete the \"_\" directory", "Error", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                SetProperties();
                try
                {

                    cfgm.SetAutoexec(ref Paths);
                    gmm.WriteFile(ref Paths);
                    cfgm.ConfigAutoExec();
                    csm.WriteFile(ref Paths);
                    csm.WriteChatScheme(ref Paths);
                    csm.AddFonts(ref Paths);
                    ham.WriteFile(ref Paths);
                    wsm.WriteFiles(ref Paths);
                    ssm.WriteFile(ref Paths);
                    dspm.WriteFile(ref Paths);
                    lgm.WriteFiles(ref Paths);

                    #region bg download and application


                    //FIRST, let's apply the choosen mp3 file for bg music
                    if (cfgm.BGMusic && BGMusicPath != "none")
                    {
                        try
                        {
                            File.Copy(BGMusicPath, Paths.SoundsPath + @"startup_music.mp3", true);
                        }
                        catch
                        {
                            System.Windows.MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("I wasn't able to move the mp3 file from your "+
                                "location for the bg music :(. Maybe you should try another time, or test another file","ERROR", System.Windows.MessageBoxButton.OK);
                        }
                    }
                    File.WriteAllBytes(Paths.BackgroundsPath + @"background01.zip", HL2Customizer.Resources.resfile.background01);
                    Extracter.Extract(Paths.BackgroundsPath, "background01.zip");

                    File.WriteAllBytes(Paths.BackgroundsPath + @"defaultBG.zip", HL2Customizer.Resources.resfile.defaultBG);
                    Extracter.Extract(Paths.BackgroundsPath, "defaultBG.zip");

                    File.WriteAllBytes(Paths.MapsPath + @"defaultMap.zip", HL2Customizer.Resources.resfile.defaultMap);
                    Extracter.Extract(Paths.MapsPath, "defaultMap.zip");

                    try
                    {
                        if (!bgm.MapBG) bgm.DownloadBG(menueditor_2dbgBox.Text, ref Paths);
                        else bgm.DownloadMapBG(menueditor_3dbgBox.Text, ref Paths);
                    }
                    catch (WebException exc)
                    {
                        System.Windows.MessageBox.Show("It was not possible to download the selected background, the default background" +
                                "will be loaded instead. More infos : " + exc.Message, "Error", MessageBoxButton.OK);
                        menueditor_2dbgRB.IsChecked = true;
                        menueditor_2dbgBox.SelectedIndex = 0;
                    }

                    if (bgm.MapBG) bgm.Apply2dBackground(ref Paths, menueditor_3dbgBox.Text);
                    else bgm.Apply2dBackground(ref Paths, menueditor_2dbgBox.Text);
                    #endregion

                    #region hudanim elements creation
                    Tuple<string, string>[] s;
                    //ADDING CUSTOM CROSSHAIR
                    if (hlm.AdvCrosshair)
                    {
                        s = new Tuple<string, string>[]{
                        Tuple.Create("controlName","Label"),
                        Tuple.Create("fieldName","Xhair"),
                        Tuple.Create("visible","1"),
                        Tuple.Create("enabled","1"),
                        Tuple.Create("font","XHAIR"),
                        Tuple.Create("labelText",hlm.CrosshairType.ToString()),
                        Tuple.Create("textAlignment","center"),
                        Tuple.Create("xpos","c-320"),
                        Tuple.Create("ypos","0"),
                        Tuple.Create("wide","640"),
                        Tuple.Create("tall","480"),
                        Tuple.Create("fgcolor_override",basicConfigs_crossColorBox.Text),
                        };
                        hlm.AddElement("XHAIR", s);
                    }

                    //ADD AUX. POWER BAR
                    int BarInsetX, BarInsetY, text_xpos, text_ypos;
                    switch (hlm.AuxPowerLabelPos)
                    {
                        case "UP": BarInsetX = 8; BarInsetY = 15; text_xpos = 8; text_ypos = 4; break;
                        case "DOWN": BarInsetX = 8; BarInsetY = 5;
                            text_xpos = 8;
                            text_ypos = Convert.ToInt32(hlm.AuxPowerConfig[1]) + 7; break;
                        case "NONE": BarInsetX = 8;
                            BarInsetY = 10 + Convert.ToInt32(hlm.AuxPowerConfig[1]) / 2 - Convert.ToInt32(hlm.AuxPowerConfig[1]) / 2;
                            text_xpos = -10; text_ypos = -10; break;
                        default: goto case "UP";
                    }
                    s = new Tuple<string, string>[]{
                    Tuple.Create("fieldName","HudSuitPower"),
                    Tuple.Create("visible","1"),
                    Tuple.Create("enabled","1"),
                    Tuple.Create("xpos","16"),
                    Tuple.Create("ypos","396"),
                    Tuple.Create("wide",(15 + Convert.ToInt32(hlm.AuxPowerConfig[0])).ToString()),
                    Tuple.Create("tall",(20 + Convert.ToInt32(hlm.AuxPowerConfig[1])).ToString()),
                    Tuple.Create("AuxPowerDisabledAlpha","70"),
                    Tuple.Create("BarInsetX", BarInsetX.ToString()), 
                    Tuple.Create("BarInsetY", BarInsetY.ToString()),
                    Tuple.Create("BarWidth", hlm.AuxPowerConfig[0]),
                    Tuple.Create("BarHeight", hlm.AuxPowerConfig[1]),
                    Tuple.Create("BarChunkWidth", hlm.AuxPowerConfig[2]),
                    Tuple.Create("BarChunkGap", hlm.AuxPowerConfig[3]),
                    Tuple.Create("text_xpos", text_xpos.ToString()),
                    Tuple.Create("text_ypos", text_ypos.ToString()),
                    Tuple.Create("text2_xpos", "-10"),
                    Tuple.Create("text2_ypos", "-10"),
                    Tuple.Create("PaintBackgroundType","2"),
                    };
                    hlm.AddElement("HudSuitPower", s);

                    //ADDING BRAND
                    s = new Tuple<string, string>[]{
                    Tuple.Create("controlName","Panel"),
                    Tuple.Create("fieldName","BrandPanel"),
                    Tuple.Create("visible","1"),
                    Tuple.Create("enabled","1"),
                    Tuple.Create("xpos","4"),
                    Tuple.Create("ypos","32"),
                    Tuple.Create("wide","160"),
                    Tuple.Create("tall","18"),
                    };
                    hlm.AddElement("BrandPanel", s);
                    s = new Tuple<string, string>[]{
                    Tuple.Create("controlName","Label"),
                    Tuple.Create("fieldName","Brand"),
                    Tuple.Create("visible","1"),
                    Tuple.Create("enabled","1"),
                    Tuple.Create("labelText","HUD made with Hl2Customizer v." + HL2Customizer.Resources.resfile.Version),
                    Tuple.Create("textAlignment","center"),
                    Tuple.Create("xpos","4"),
                    Tuple.Create("ypos","32"),
                    Tuple.Create("wide","160"),
                    Tuple.Create("tall","18"),
                    Tuple.Create("font","SmallVerdana"),
                    Tuple.Create("fgcolor_override", basicConfigs_mainColorBox.Text),
                    };
                    hlm.AddElement("Brand", s);

                    //SPECIAL BRAND
                    if (brand.Enabled)
                        hlm.AddElement("SpecialBrand", brand.GetBrand(basicConfigs_mainColorBox.Text));

                    hlm.WriteFile(ref Paths);

                    #endregion

                    HudInformations infos = new HudInformations("previous");
                    SavedData save = new SavedData(infos, csm, ssm, hlm, ham, wsm, gmm, bgm, cfgm, dspm, lgm, brand);
                    Serializer.SerializeHudData(save);
                    System.Windows.MessageBox.Show("Done ! Your hud is ready ;D. You can now start Half-life 2 Deathmatch.", "Success !!", MessageBoxButton.OK);

                }
                catch (Exception exc)
                {
                    System.Windows.MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK);
                };
            }
        }
        #endregion

        #region setproperties
        private void SetProperties()
        {
            cfgm.SetProperties(!(bool)advancedconfigs_autoswitchRB.IsChecked, (bool)advancedconfigs_motdRB.IsChecked,
                (bool)advancedconfigs_consoleRB.IsChecked, (bool)advancedconfigs_sprayRB.IsChecked, (bool)advancedconfigs_filterRB.IsChecked,
                Models[advancedconfigs_ModelsBox.SelectedIndex].Item2, Weapons[advancedconfigs_WeaponsBox.SelectedIndex].Item2);
            float interpTmp = float.Parse(advancedconfigs_interpLabel.Content.ToString(), CultureInfo.InvariantCulture);
            cfgm.SetRate(Convert.ToInt32(advancedconfigs_rateLabel.Content), Convert.ToInt32(advancedconfigs_updateLabel.Content), Convert.ToInt32(advancedconfigs_cmdLabel.Content), interpTmp);
            cfgm.DontModifyRates = (bool)advancedconfigs_dontTuchThisRB.IsChecked;
            cfgm.MapBG = (bool)menueditor_3dbgRB.IsChecked;
            cfgm.BGMusic = (bool)menueditor_CBMusic.IsChecked;
            cfgm.MapBGname = menueditor_3dbgBox.Text ;
            int redAmount;
            switch ((int)advancedconfigs_redScreenScroller.Value)
            {
                case 0: redAmount = 200; break;
                case 1: redAmount = 100; break;
                case 2: redAmount = 40; break;
                case 3: redAmount = 0; break;
                default: goto case 0;
            }
            ham.SetProperties(redAmount);

            switch ((int)advancedconfigs_bipLenghtScroller.Value)
            {
                case 0: dspm.SoundVolume = 1.6f; break;
                case 1: dspm.SoundVolume = 0.8f; break;
                case 2: dspm.SoundVolume = 0.4f; break;
                case 3: dspm.SoundVolume = 0.0f; break;
                default: goto case 0;
            }

            csm.SetProperties(basicConfigs_mainColorBox.Text, basicConfigs_secColorBox.Text,
            basicConfigs_crossColorBox.Text, basicConfigs_auxCrossColorBox.Text,
            menueditor_txtBox1.Text, (bool)basicConfigs_outlinedRB.IsChecked);
            csm.TxtFont = fonteditor_TxtFontBox.Text;
            csm.NbrFont = fonteditor_NbrFontBox.Text;
            ssm.TitleFont = fonteditor_TitleFontBox.Text;
            csm.ChatFont = fonteditor_ChatFontBox.Text;

            switch ((int)basicConfigs_xhairSizeSlider.Value)
            {
                case 0: csm.CrosshairSize = 'S'; break;
                case 1: csm.CrosshairSize = 'M'; break;
                case 2: csm.CrosshairSize = 'L'; break;
                case 3: csm.CrosshairSize = 'G'; break;
                default: goto case 0;
            }

            csm.KeepPannelBG = !(bool)fonteditor_DeletePannelsCB.IsChecked;
            csm.KeepXhair = (bool)basicConfigs_dontChangeCrosshairRB.IsChecked;
            wsm.KeepXhair = (bool)basicConfigs_dontChangeCrosshairRB.IsChecked;
            ssm.SetProperties(MenuColors[menueditor_txtBox1.SelectedIndex],
                MenuColors[menueditor_txtBox2.SelectedIndex],
                MenuColors[menueditor_bgBox.SelectedIndex]);
            cfgm.SetQuickInfos((bool)basicConfigs_keepQuickInfosRB.IsChecked);

            hlm.Initiate();
            hlm.CrosshairType = Crosshairs[basicConfigs_CrosshairBox.SelectedIndex].Item2;
            hlm.AdvCrosshair = !(bool)basicConfigs_dontChangeCrosshairRB.IsChecked;
            hlm.AuxPowerConfig[0] = auxeditor_barWidthLabel.Content.ToString();
            hlm.AuxPowerConfig[1] = auxeditor_barHeightLabel.Content.ToString();
            hlm.AuxPowerConfig[2] = auxeditor_tileWidthLabel.Content.ToString();
            hlm.AuxPowerConfig[3] = auxeditor_gapLabel.Content.ToString();
            hlm.AuxPowerLabelPos = auxeditor_auxlabelpos.Text;

            lgm.NoHealthIcon = fonteditor_healthIconBox.SelectedIndex + 1;
            lgm.NoAmorIcon = fonteditor_armorIconBox.SelectedIndex + 1;

            bgm.MapBG = (bool)menueditor_3dbgRB.IsChecked;
            bgm.SmokeEffects = ((bool)menueditor_2dbgRB.IsChecked && (bool)menueditor_smokeEffectCB.IsChecked);
        }

        private bool DefinePaths(string FileName)
        {
            //Define and create folders
            Paths.MainPath = FileName;
            FileName = FileName.ToLower();
            if (!Directory.Exists(FileName + @"/hl2mp")
                || !Directory.Exists(FileName + @"/hl2")
                || !FileName.Contains("steam")) return false;

            Paths.CustomPath = Paths.MainPath + @"/hl2mp/custom/";
            Paths.HudPath = Paths.CustomPath + @"_/";

            string[] directories = Directory.GetDirectories(Paths.CustomPath);
            if (directories.Count() != 0)
            {
                if (!Directory.Exists(Paths.CustomPath + "backup")) Directory.CreateDirectory(Paths.CustomPath + "backup");
                foreach (string dir in directories)
                {
                    string dirName = dir.Split('/').Last();
                    if (dirName != "_" && dirName != "backup")
                    {
                        try
                        {
                            Directory.Move(dir, Paths.CustomPath + "backup/" + dirName);
                            System.Windows.MessageBox.Show("Some modifications where already present in the custom folder. To preven bugs, the folder \""
                                + dirName + "\" will be moved to a backup folder in custom", "INFO", MessageBoxButton.OK);
                        }
                        catch
                        {
                            System.Windows.MessageBox.Show("The folder \"" + dir + "\" may cause further problems ... would be better to move it at another location",
                                "INFO", MessageBoxButton.OK);
                        }
                    }
                }
            }

            if (!Directory.Exists(Paths.HudPath)) Directory.CreateDirectory(Paths.HudPath);
            Paths.CfgPath = Paths.HudPath + @"cfg/";
            if (!Directory.Exists(Paths.CfgPath)) Directory.CreateDirectory(Paths.CfgPath);
            Paths.MaterialsPath = Paths.HudPath + @"materials/";
            if (!Directory.Exists(Paths.MaterialsPath)) Directory.CreateDirectory(Paths.MaterialsPath);
            Paths.BackgroundsPath = Paths.MaterialsPath + @"Console/";
            if (!Directory.Exists(Paths.BackgroundsPath)) Directory.CreateDirectory(Paths.BackgroundsPath);
            Paths.ResPath = Paths.HudPath + @"resource/";
            if (!Directory.Exists(Paths.ResPath)) Directory.CreateDirectory(Paths.ResPath);
            Paths.FontsPath = Paths.ResPath + @"fonts/";
            if (!Directory.Exists(Paths.FontsPath)) Directory.CreateDirectory(Paths.FontsPath);
            Paths.UIPath = Paths.ResPath + @"UI/";
            if (!Directory.Exists(Paths.UIPath)) Directory.CreateDirectory(Paths.UIPath);
            Paths.ScriptsPath = Paths.HudPath + @"scripts/";
            if (!Directory.Exists(Paths.ScriptsPath)) Directory.CreateDirectory(Paths.ScriptsPath);
            Paths.MapsPath = Paths.HudPath + @"maps/";
            if (!Directory.Exists(Paths.MapsPath)) Directory.CreateDirectory(Paths.MapsPath);
            Paths.SoundsPath = Paths.HudPath + @"sound/hl2c/";
            if (!Directory.Exists(Paths.HudPath + @"sound/")) Directory.CreateDirectory(Paths.HudPath + @"sound/");
            if (!Directory.Exists(Paths.SoundsPath)) Directory.CreateDirectory(Paths.SoundsPath);

            //SPECIALE SCOREBOARD
            if (!File.Exists(Paths.UIPath + @"ScoreBoard.res"))
            {
                File.WriteAllBytes(Paths.UIPath + @"ScoreBoard.res", HL2Customizer.Resources.resfile.ScoreBoard);
            }
            //

            return true;
        }
        #endregion

        //BASIC CONFIGS
        #region basicconfigs

        private void browsePathButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if ("OK" == dialog.ShowDialog().ToString())
                {
                    basicConfigs_pathTextBox.Text = dialog.SelectedPath;
                    PathSaver.SavePath(dialog.SelectedPath);
                }
                else
                    basicConfigs_pathTextBox.Text = "The path is not valid";
            }
        }

        private void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        private void creditsButton_Click(object sender, RoutedEventArgs e)
        {
            CreditsWin win = new CreditsWin();
            win.ShowDialog();
        }

        private void dontChangeCrosshairRB_Checked(object sender, RoutedEventArgs e)
        {
            basicConfigs_CrosshairBox.IsEnabled = false;
            basicConfigs_outlinedRB.IsEnabled = false;
            basicConfigs_xhairSizeSlider.IsEnabled = false;
            //Peaf Did What You See Here
            ChangeColor("Black");
            Brush brush = new SolidColorBrush(Color.FromArgb(hexaColor[0], hexaColor[1], hexaColor[2], hexaColor[3]));
            basicConfigs_previewCrosshair.Foreground = brush;
            //End Of Peaf Did What You See Here
        }

        private void dontChangeCrosshairRB_Unchecked(object sender, RoutedEventArgs e)
        {
            
            basicConfigs_CrosshairBox.IsEnabled = true;
            basicConfigs_outlinedRB.IsEnabled = true;
            basicConfigs_xhairSizeSlider.IsEnabled = true;
            //Peaf Did What You See Here
            ChangeColor(basicConfigs_crossColorBox.Items[basicConfigs_crossColorBox.SelectedIndex].ToString());
            Brush brush = new SolidColorBrush(Color.FromArgb(hexaColor[0], hexaColor[1], hexaColor[2], hexaColor[3]));
            basicConfigs_previewCrosshair.Foreground = brush;
            //End Of Peaf Did What You See Here
        }

        private void basicConfigs_xhairSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch ((int)basicConfigs_xhairSizeSlider.Value)
            {
                case 0: basicConfigs_crosshairSizeLabel.Content = "Small"; break;
                case 1: basicConfigs_crosshairSizeLabel.Content = "Medium"; break;
                case 2: basicConfigs_crosshairSizeLabel.Content = "Large"; break;
                case 3: basicConfigs_crosshairSizeLabel.Content = "Giant"; break;
                default: goto case 0;
            }
            int csize;
            switch ((int)basicConfigs_xhairSizeSlider.Value)
            {
                case 0: csize = 40; break;
                case 1: csize = 64;  break;
                case 2: csize = 80; break;
                case 3: csize = 104;break;
                default: goto case 0;
            }
            basicConfigs_previewCrosshair.FontSize = csize;
            weaponeditor_previewMainCrosshair.FontSize = csize;
            weaponeditor_previewSecCrosshair.FontSize = csize;
        }
        //Peaf Did What You See Here
        byte[] hexaColor = new byte[] { 196, 0, 0, 0 }; //faudra peut être le changer d'endroit lui
        private void ChangeColor(string s)
        {

            switch (s)
            {
                case "Black": hexaColor = new byte[] { 196, 0, 0, 0 };
                    break;
                case "Brown": hexaColor = new byte[] { 255, 139, 69, 19 };
                    break;
                case "Red": hexaColor = new byte[] { 255, 255, 0, 0 };
                    break;
                case "Orange": hexaColor = new byte[] { 255, 255, 176, 0 };
                    break;
                case "Yellow": hexaColor = new byte[] { 255, 255, 255, 0 };
                    break;
                case "DarkGreen": hexaColor = new byte[] { 255, 0, 80, 0 };
                    break;
                case "Green": hexaColor = new byte[] { 255, 0, 128, 0 };
                    break;
                case "Lime": hexaColor = new byte[] { 255, 0, 255, 0 };
                    break;
                case "Cyan": hexaColor = new byte[] { 255, 0, 255, 255 };
                    break;
                case "Jade": hexaColor = new byte[] { 255, 64, 255, 160 };
                    break;
                case "Turquoise": hexaColor = new byte[] { 255, 38, 196, 236 };
                    break;
                case "SkyBlue": hexaColor = new byte[] { 255, 128, 160, 224 };
                    break;
                case "Blue": hexaColor = new byte[] { 255, 0, 0, 255 };
                    break;
                case "Navy": hexaColor = new byte[] { 255, 16, 16, 102 };
                    break;
                case "Purple": hexaColor = new byte[] { 255, 128, 0, 128 };
                    break;
                case "Magenta": hexaColor = new byte[] { 255, 255, 0, 255 };
                    break;
                case "White": hexaColor = new byte[] { 255, 255, 255, 255 };
                    break;
                case "Pink": hexaColor = new byte[] { 255, 255, 105, 180 };
                    break;
                case "Silver": hexaColor = new byte[] { 255, 204, 204, 204 };
                    break;
               

            } 

        }
        private void basicConfigs_crossColorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (basicConfigs_crossColorBox.SelectedIndex >= 0)
            {
                ChangeColor(basicConfigs_crossColorBox.Items[basicConfigs_crossColorBox.SelectedIndex].ToString());
                Brush brush = new SolidColorBrush(Color.FromArgb(hexaColor[0], hexaColor[1], hexaColor[2], hexaColor[3]));
                basicConfigs_previewCrosshair.Foreground = brush;
                weaponeditor_previewMainCrosshair.Foreground = brush;
            }
        }

        private void basicConfigs_auxCrossColorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeColor(basicConfigs_auxCrossColorBox.Items[basicConfigs_auxCrossColorBox.SelectedIndex].ToString());
            Brush brush = new SolidColorBrush(Color.FromArgb(hexaColor[0], hexaColor[1], hexaColor[2], hexaColor[3]));
            weaponeditor_previewSecCrosshair.Foreground = brush;
        }

        //End Of Peaf Did What You See Here

        private void CrosshairBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            basicConfigs_previewCrosshair.Content = Crosshairs[basicConfigs_CrosshairBox.SelectedIndex].Item2.ToString();
            weaponeditor_previewMainCrosshair.Content = Crosshairs[basicConfigs_CrosshairBox.SelectedIndex].Item2.ToString();
        }
        private void basicConfigs_help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.turanic.com/HL2Customizer/tutorials.php");
        }
        private void basicConfigs_donationButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=DYPXG9S2UDBTN");
        }
        #endregion

        //ADVANCED CONFIGS
        #region advancedconfigs

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateRateBox(object sender, SelectionChangedEventArgs e)
        {
            switch ((string)advancedconfigs_RateBox.SelectedItem)
            {
                case "Low rate": advancedconfigs_rateLabel.Content = "10000"; advancedconfigs_updateLabel.Content = "10"; advancedconfigs_cmdLabel.Content = "10"; advancedconfigs_interpLabel.Content = "0.1"; break;
                case "Default rate": advancedconfigs_rateLabel.Content = "30000"; advancedconfigs_updateLabel.Content = "20"; advancedconfigs_cmdLabel.Content = "30"; advancedconfigs_interpLabel.Content = "0.1"; break;
                case "Limited rate": advancedconfigs_rateLabel.Content = "66000"; advancedconfigs_updateLabel.Content = "33"; advancedconfigs_cmdLabel.Content = "33"; advancedconfigs_interpLabel.Content = "0.1"; break;
                case "Normal rate": advancedconfigs_rateLabel.Content = "80000"; advancedconfigs_updateLabel.Content = "66"; advancedconfigs_cmdLabel.Content = "66"; advancedconfigs_interpLabel.Content = "0.1"; break;
                case "Speed rate": advancedconfigs_rateLabel.Content = "100000"; advancedconfigs_updateLabel.Content = "100"; advancedconfigs_cmdLabel.Content = "100"; advancedconfigs_interpLabel.Content = "0.0152"; break;
                case "League rate": advancedconfigs_rateLabel.Content = "128000"; advancedconfigs_updateLabel.Content = "66"; advancedconfigs_cmdLabel.Content = "66"; advancedconfigs_interpLabel.Content = "0.0152"; break;
                case "ZG/UG rate": advancedconfigs_rateLabel.Content = "100000"; advancedconfigs_updateLabel.Content = "66"; advancedconfigs_cmdLabel.Content = "66"; advancedconfigs_interpLabel.Content = "0.1"; break;
                case "Cheater rate": advancedconfigs_rateLabel.Content = "500000"; advancedconfigs_updateLabel.Content = "5000"; advancedconfigs_cmdLabel.Content = "66"; advancedconfigs_interpLabel.Content = "0.0"; break;
                default: goto case "Default rate";
            }
        }

        private void redScreenScroller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch ((int)advancedconfigs_redScreenScroller.Value)
            {
                case 0: advancedconfigs_redAmountLabel.Content = "FULL"; break;
                case 1: advancedconfigs_redAmountLabel.Content = "HALF"; break;
                case 2: advancedconfigs_redAmountLabel.Content = "LOW"; break;
                case 3: advancedconfigs_redAmountLabel.Content = "NUL"; break;
                default: goto case 0;
            }
        }

        private void bipLenghtScroller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch ((int)advancedconfigs_bipLenghtScroller.Value)
            {
                case 0: advancedconfigs_bipLenghtLabel.Content = "LONG"; break;
                case 1: advancedconfigs_bipLenghtLabel.Content = "HALF"; break;
                case 2: advancedconfigs_bipLenghtLabel.Content = "SHORT"; break;
                case 3: advancedconfigs_bipLenghtLabel.Content = "NONE"; break;
                default: goto case 0;
            }
        }

        private void FillWeaponList()
        {
            advancedconfigs_WeaponsBox.Items.Clear();
            foreach (Tuple<string, string> e in Weapons)
                advancedconfigs_WeaponsBox.Items.Add(e.Item1);
            advancedconfigs_WeaponsBox.SelectedIndex = 0;
        }
        private void noDfltWeapCB_Checked(object sender, RoutedEventArgs e)
        {
            Weapons = NonDefaultWeapons;
            FillWeaponList();
        }
        private void noDfltWeapCB_Unchecked(object sender, RoutedEventArgs e)
        {
            Weapons = DefaultWeapons;
            FillWeaponList();
        }
        private void advancedconfigs_brandButton_Click(object sender, RoutedEventArgs e)
        {
            if (brand.ApplyBrand(advancedconfigs_brandTextBox.Text, basicConfigs_mainColorBox.Text))
            {
                System.Windows.MessageBox.Show("A brand has now been added", "Success", MessageBoxButton.OK);
                advancedconfigs_brandButton.IsEnabled = false;
                advancedconfigs_delBrandButton.IsEnabled = true;
            }
            else
                System.Windows.MessageBox.Show("This brand does not exist", "Error", MessageBoxButton.OK);
        }
        private void advancedconfigs_delBrandButton_Click(object sender, RoutedEventArgs e)
        {
            advancedconfigs_brandButton.IsEnabled = true;
            advancedconfigs_delBrandButton.IsEnabled = false;
        }

        #endregion

        //MENU EDITOR
        #region menueditor

        
        private void RefreshList()
        {
            menueditor_menuElementsList.Items.Clear();
            for (int i = 49; i > 0; i--)
            {
                MenuElement e = gmm.MenuLabels[i];
                if (e != null)
                    menueditor_menuElementsList.Items.Add(e.Label);
            }
        }

        private void ChangeContent(MenuElement[] array, bool delete)
        {
            if (!keeper)
            {
                foreach (MenuElement elmt in array)
                    if (delete)
                        gmm.MenuLabels[elmt.Id] = null;
                    else
                        gmm.MenuLabels[elmt.Id] = elmt;
                RefreshList();
            }
        }

        private void basicsLabelsCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(BasicLabels, false);
            gmm.Options[0] = true;
        }

        private void basicsLabelsCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(BasicLabels, true);
            gmm.Options[0] = false;
        }

        private void DemoLabelCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(DemoLabels, false);
            gmm.Options[1] = true;
        }

        private void DemoLabelCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(DemoLabels, true);
            gmm.Options[1] = false;
        }

        private void ConsoleLabelCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(ConsoleLabels, false);
            gmm.Options[2] = true;
        }

        private void ConsoleLabelCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(ConsoleLabels, true);
            gmm.Options[2] = false;
        }

        private void graphLabelCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(PerfLabels, false);
            gmm.Options[3] = true;
        }

        private void graphLabelCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(PerfLabels, true);
            gmm.Options[3] = false;
        }

        private void jointeamLabelCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(TeamLabels, false);
            gmm.Options[4] = true;
        }

        private void jointeamLabelCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(TeamLabels, true);
            gmm.Options[4] = false;
        }

        private void PMSLabelCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(PmsLabels, false);
            gmm.Options[5] = true;
        }

        private void PMSLabelCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(PmsLabels, true);
            gmm.Options[5] = false;
        }

        private void adminLabelCB_Checked(object sender, RoutedEventArgs e)
        {
            ChangeContent(AdminLabels, false);
            gmm.Options[6] = true;
        }

        private void adminLabelCB_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeContent(AdminLabels, true);
            gmm.Options[6] = false;
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            int indexTodelete = menueditor_menuElementsList.SelectedIndex;
            if (indexTodelete < 0) return;
            for (int i = 49; i >= 0; i--)
            {
                if (gmm.MenuLabels[i] != null) indexTodelete--;
                if (indexTodelete < 0)
                {
                    gmm.MenuLabels[i] = null;
                    RefreshList();
                    break;
                }
            }
        }

        private void menueditor_dlCB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("http://turanic.com/HL2Customizer/dlable_content/2dbg/bgs_list.txt");
                StreamReader reader = new StreamReader(stream);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!bgm.Locals2dBGs.Contains(line))
                        menueditor_2dbgBox.Items.Add(line);
                }
                stream = client.OpenRead("http://turanic.com/HL2Customizer/dlable_content/3dbg/bgs_list.txt");
                reader = new StreamReader(stream);

                while ((line = reader.ReadLine()) != null)
                {
                    if (!bgm.LocalsMapBGs.Contains(line))
                        menueditor_3dbgBox.Items.Add(line);
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("The software was not able to access the main server. The online content is not available", "Can't access server", MessageBoxButton.OK);
                menueditor_dlCB.IsChecked = false;
            }
        }

        private void menueditor_dlCB_Unchecked(object sender, RoutedEventArgs e)
        {
            menueditor_2dbgBox.Items.Clear();
            foreach (string file in bgm.Locals2dBGs)
                menueditor_2dbgBox.Items.Add(file);
        }

        private void menueditor_2dbgRB_Checked(object sender, RoutedEventArgs e)
        {
            menueditor_2dbgBox.IsEnabled = true;
            menueditor_smokeEffectCB.IsEnabled = true;
            menueditor_3dbgBox.IsEnabled = false;
        }

        private void menueditor_3dbgRB_Checked(object sender, RoutedEventArgs e)
        {
            menueditor_2dbgBox.IsEnabled = false;
            menueditor_smokeEffectCB.IsEnabled = false;
            menueditor_3dbgBox.IsEnabled = true;
        }

        private void menueditor_CBMusic_Checked(object sender, RoutedEventArgs e)
        {
            menueditor_browseMusic.IsEnabled = true;
        }

        private void menueditor_CBMusic_Unchecked(object sender, RoutedEventArgs e)
        {
            menueditor_browseMusic.IsEnabled = false;
        }

        private void menueditor_browseMusic_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Music Files (*.mp3)|*.mp3";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name
                if (result == true)
                {
                    // Open document 
                    BGMusicPath = dlg.FileName;
                }

        }

        #endregion

        //FILE SYSTEM
        #region file

        private void RefreshBoxes()
        {
            file_LoadHudBox.Items.Clear();
            file_ExportHudBox.Items.Clear();

            DirectoryInfo saves = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer");
            foreach (var file in saves.GetFiles())
            {
                if (!file.Name.Contains("previous") && file.Extension == ".hcd")
                {
                    file_LoadHudBox.Items.Add(file.Name.Replace(".hcd", ""));
                    file_ExportHudBox.Items.Add(file.Name.Replace(".hcd", ""));
                }
            }
            file_LoadHudBox.SelectedIndex = 0;
            file_ExportHudBox.SelectedIndex = 0;
        }

        private void file_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetProperties();
            HudInformations infos = new HudInformations(file_SaveNameBox.Text);
            SavedData save = new SavedData(infos, csm, ssm, hlm, ham, wsm, gmm, bgm, cfgm, dspm, lgm, brand);
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + infos.Name + ".hcd"))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("You already saved a hud with this name... do you want to erease the previous one?", "Save already exist", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;
            }
            Serializer.SerializeHudData(save);
            System.Windows.MessageBox.Show("Save completed", "Success", MessageBoxButton.OK);

            RefreshBoxes();
        }
        private void file_LoadHudButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + file_LoadHudBox.Text + ".hcd"))
            {
                System.Windows.MessageBox.Show("The hud save does not exist anymore", "Error", MessageBoxButton.OK);
                return;
            }
            SavedData save;
            try
            {
                save = Serializer.DeSerializeHudData(file_LoadHudBox.Text);
                MessageBoxResult result = System.Windows.MessageBox.Show("This hud named " + save.Infos.Name + " has been made by " + save.Infos.Creator + " the " + save.Infos.Date
                    + "\n Are you sure you wanna load it ?", "Hud informations", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (save.Infos.HudVersion != HL2Customizer.Resources.resfile.Version)
                    {
                        System.Windows.MessageBox.Show("The hud use the version " + save.Infos.HudVersion + ". It can't be loaded with this version of HL2Customizer", "Obselete Hud", MessageBoxButton.OK);
                    }
                    else
                    {
                        MainWindow win = new MainWindow(save);
                        win.Show();
                        this.Close();
                    }
                }
            }
            catch { System.Windows.MessageBox.Show("Sorry, the save file is corrupted", "Error", MessageBoxButton.OK); }

        }
        private void file_DeleteHudButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + file_LoadHudBox.Text + ".hcd"))
            {
                System.Windows.MessageBox.Show("The hud save does not exist anymore", "Error", MessageBoxButton.OK);
                return;
            }
            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you wanna delete this file?", "Hud informations", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + file_LoadHudBox.Text + ".hcd");
                    System.Windows.MessageBox.Show("The hud as been deleted", "Success", MessageBoxButton.OK);
                    RefreshBoxes();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("I don't have the needed rights to delete this file", "Error", MessageBoxButton.OK);
            }
        }

        private void file_ExportBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if ("OK" == dialog.ShowDialog().ToString())
                    file_ExportPathBox.Text = dialog.SelectedPath;
                else
                    file_ExportPathBox.Text = "The path is not valid";
            }
        }

        private void file_ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string destination = file_ExportPathBox.Text;
            if (Directory.Exists(destination))
            {
                string filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + file_ExportHudBox.Text + ".hcd";
                destination += @"/" + file_ExportHudBox.Text + ".hcd";
                try
                {
                    File.Copy(filename, destination, true);
                    System.Windows.MessageBox.Show("The save has been exported to the destination.", "Success", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid path, please select another destination", "Error", MessageBoxButton.OK);
            }
        }

        private void file_ImportBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Custom Hud Files|*.hcd";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;

            if ("OK" == dialog.ShowDialog().ToString() && dialog.CheckPathExists)
            {
                file_ImportPathBox.Text = dialog.FileName;
            }
            else
            {
                file_ImportPathBox.Text = "Invalid path";
            }
        }

        private void file_ImportButton_Click(object sender, RoutedEventArgs e)
        {
            string file = file_ImportPathBox.Text;
            if (File.Exists(file))
            {
                string destination = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\HL2Customizer\\" + file_ImportFileNameBox.Text + ".hcd";
                try
                {
                    File.Copy(file, destination, true);
                    System.Windows.MessageBox.Show("The file has been imported succefully.", "Success", MessageBoxButton.OK);
                    RefreshBoxes();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid path, please select another file", "Error", MessageBoxButton.OK);
            }
        }

        #endregion

        //AUX. POWER EDITOR
        #region auxeditor
        private void barHeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            auxeditor_barHeightLabel.Content = auxeditor_barHeightSlider.Value;
            auxeditor_rbCustom.IsChecked = true;
        }

        private void tileWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            auxeditor_tileWidthLabel.Content = auxeditor_tileWidthSlider.Value;
            DetermineNbrOfChunks();
            auxeditor_rbCustom.IsChecked = true;
        }

        private void gapSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            auxeditor_gapLabel.Content = auxeditor_gapSlider.Value;
            DetermineNbrOfChunks();
            auxeditor_rbCustom.IsChecked = true;
        }

        private void auxeditor_barWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            auxeditor_barWidthLabel.Content = auxeditor_barWidthSlider.Value;
            DetermineNbrOfChunks();
            auxeditor_rbCustom.IsChecked = true;
        }
        
        private void DetermineNbrOfChunks()
        {
            auxeditor_tileNbrLabel.Content = ((int)(auxeditor_barWidthSlider.Value / (Convert.ToInt32(auxeditor_tileWidthLabel.Content) + Convert.ToInt32(auxeditor_gapLabel.Content)))).ToString();
        }
        private void auxeditor_rbDefault_Checked(object sender, RoutedEventArgs e)
        {
            auxeditor_barHeightSlider.Value = 4;
            auxeditor_barWidthSlider.Value = 92;
            auxeditor_tileWidthSlider.Value = 6;
            auxeditor_gapSlider.Value = 3;
            auxeditor_rbDefault.IsChecked = true;
        }

        private void auxeditor_rbFull_Checked(object sender, RoutedEventArgs e)
        {
            auxeditor_barHeightSlider.Value = 4;
            auxeditor_barWidthSlider.Value = 100;
            auxeditor_tileWidthSlider.Value = 1;
            auxeditor_gapSlider.Value = 0;
            auxeditor_rbFull.IsChecked = true;
        }

        private void auxeditor_rbCombine_Checked(object sender, RoutedEventArgs e)
        {
            auxeditor_barHeightSlider.Value = 9;
            auxeditor_barWidthSlider.Value = 70;
            auxeditor_tileWidthSlider.Value = 1;
            auxeditor_gapSlider.Value = 1;
            auxeditor_rbCombine.IsChecked = true;
        }

        private void auxeditor_rbMinimal_Checked(object sender, RoutedEventArgs e)
        {
            auxeditor_barHeightSlider.Value = 3;
            auxeditor_barWidthSlider.Value = 90;
            auxeditor_tileWidthSlider.Value = 9;
            auxeditor_gapSlider.Value = 2;
            auxeditor_rbMinimal.IsChecked = true;
        }

        #endregion

        //WPN EDITOR
        #region weaponeditor

        private void weaponeditor_wpnType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            weaponeditor_wpnIcon.Content = WpnsTypes[weaponeditor_wpnType.SelectedIndex].Item2;
            loadConfig();
        }

        private void weaponeditor_loadOldConfig_Click(object sender, RoutedEventArgs e)
        {
            loadConfig();
        }

        private void loadConfig()
        {
            weaponeditor_wpnNameBox.Text = wsm.Weapons[weaponeditor_wpnType.SelectedIndex].Name;

            for (int i = 0; i < Crosshairs.Count(); i++)
                if (Crosshairs[i].Item2 == wsm.Weapons[weaponeditor_wpnType.SelectedIndex].Crosshair) weaponeditor_CrosshairBox.SelectedIndex = i;

            //weaponeditor_outlinedRB.IsChecked = csm.OutlinedAdditionnalCrosshairs[weaponeditor_wpnType.SelectedIndex];
        }

        private void weaponeditor_saveNewConfig_Click(object sender, RoutedEventArgs e)
        {
            wsm.Weapons[weaponeditor_wpnType.SelectedIndex].Name = weaponeditor_wpnNameBox.Text;
            //csm.OutlinedAdditionnalCrosshairs[weaponeditor_wpnType.SelectedIndex] = (bool)weaponeditor_outlinedRB.IsChecked;
            wsm.Weapons[weaponeditor_wpnType.SelectedIndex].Crosshair = Crosshairs[weaponeditor_CrosshairBox.SelectedIndex].Item2;
        }

        private void weaponeditor_CrosshairBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            weaponeditor_previewSecCrosshair.Content = Crosshairs[weaponeditor_CrosshairBox.SelectedIndex].Item2.ToString();
        }

        #endregion


        #endregion

    }
}
