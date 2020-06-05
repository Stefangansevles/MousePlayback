using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using MaterialSkin.Controls;
using WindowsInput;
using WindowsInput.Native;
using static MousePlayback.Action;

namespace MousePlayback
{
    public partial class Form1 : MaterialForm
    {
        private IKeyboardMouseEvents m_GlobalHook;
        List<Action> actions = new List<Action>();

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        //Mouse DWORD actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;        
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;

        bool isRecording;
        bool isPlaying;

        Thread playbackThread;

        //Store the currentAction in an object so it can be read elsewhere
        Action currentAction;

        InputSimulator inputSim;

        KeysConverter kc;
        Keys StartStopRecording;
        Keys PlaybackRecording;
        
        Random random;
        public Form1()
        {
            InitializeComponent();
            this.Opacity = 0;
            inputSim = new InputSimulator();
            
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
            m_GlobalHook.KeyUp += M_GlobalHook_KeyUp;

            lblStatus.Location = new Point(lblStatus.Location.X, lblStatus.Location.Y + 40);
            pbStatus.Location = new Point(pbStatus.Location.X, pbStatus.Location.Y + 40);

            btnExport.Location = new Point(btnExport.Location.X, pbStatus.Location.Y-5);
            btnImport.Location = new Point(btnImport.Location.X, pbStatus.Location.Y-5);

            tbLog.Size = new Size(tbLog.Size.Width, tbLog.Size.Height + 30);
            
            Directory.CreateDirectory(Path.GetDirectoryName(Database.databaseFile));
            AppDomain.CurrentDomain.SetData("DataDirectory", Database.databaseFile);
            Database.CreateDatabaseIfNotExist();

            kc = new KeysConverter();
            StartStopRecording = (Keys)kc.ConvertFromString(Database.HotKeyStartStopRecording);
            PlaybackRecording = (Keys)kc.ConvertFromString(Database.HotKeyPlaybackRecording);
            SetControlValues();

            random = new Random();
            tbLog.AppendText("  == Initialized MousePlayBack V" + IO.ApplicationVersion + " ==\r\n");
            tmrFadeIn.Start();
        }       

        private void SetControlValues()
        {
            cbRepeatForever.Checked = Database.RepeatForever;
            cbRandomizeInput.Checked = Database.RandomizeInput;
            cbStartStopRecording.Text = Database.HotKeyStartStopRecording;
            cbPlaybackRecording.Text = Database.HotKeyPlaybackRecording;
            numRepeatTimes.Value = Database.RepeatTimes;
            numRandomSleep.Value = Database.RndSleepTime.Value;
            numPixels.Value = Database.RndPixels.Value;
        }

        /// <summary>
        /// Subscribe to all Global Mouse Key events so they will begin firing
        /// </summary>
        public void Subscribe()
        {
            actions.Clear();            
            
            //Subscribe events
            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt += GlobalHookMouseUpExt;            
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
            m_GlobalHook.MouseMove += GlobalMouseMove;
            m_GlobalHook.MouseWheelExt += M_GlobalHook_MouseWheelExt;            
        }       

        /// <summary>
        /// Unsubscribe from all Global Mouse Key events so they will stop firing
        /// </summary>
        public void Unsubscribe()
        {
            isRecording = false;

            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.MouseUpExt -= GlobalHookMouseUpExt;            
            m_GlobalHook.MouseMove -= GlobalMouseMove;
            m_GlobalHook.MouseWheelExt -= M_GlobalHook_MouseWheelExt;                        
        }


        private void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyData == StartStopRecording && !isRecording)
            {
                btnRecord_Click(null, null);
                e.Handled = true;
            }
            else if (e.KeyData == StartStopRecording && isRecording)
            {
                btnStop_Click(null, null);
                e.Handled = true;
            }
            if (e.KeyData.ToString().Contains(PlaybackRecording.ToString()))
            {                
                if (isPlaying)
                    btnStop_Click(null, null);
                else if(!isRecording)
                    btnPlayback_Click(null, null);                

                e.Handled = true;
            }

            if (isRecording && !isPlaying && (e.KeyData != StartStopRecording && e.KeyData != PlaybackRecording))
            {
                //ctrl/shift/alt are modifiers, not actions. This messes with dragging while holding these down.
                //In the future, maybe make it so that ctrl/shift/alt can be actions, if X/Y coördinates are null so the mouse wont move?
                if (e.KeyCode == Keys.LMenu || e.KeyCode == Keys.RMenu || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey
                    || e.KeyCode == Keys.Menu || e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
                    return;
                               
                actions.Add(new Action(actions.Count, null,null, 0, DateTime.Now, ActionType.KEY_DOWN, (VirtualKeyCode)e.KeyCode, GetWinModifiers()));                
            }
        }
        

        private void M_GlobalHook_KeyUp(object sender, KeyEventArgs e)
        {
            //if (isRecording && !isPlaying && (e.KeyData != Keys.F1 && e.KeyData != Keys.F2))
                //actions.Add(new Action(Cursor.Position.X, Cursor.Position.Y, DateTime.Now, Action.ActionType.KEY_UP, (VirtualKeyCode)e.KeyCode));
        }

        private void M_GlobalHook_MouseWheelExt(object sender, MouseEventExtArgs e)
        {                                    
            if (!isPlaying)
                actions.Add(new Action(actions.Count, e.X, e.Y, e.Delta, DateTime.Now, ActionType.WHEEL, VirtualKeyCode.NONAME, GetWinModifiers()));
        }


        private void GlobalMouseMove(object sender, MouseEventArgs e)
        {
            if(!isPlaying)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_MOVE,VirtualKeyCode.NONAME, GetWinModifiers()));            
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            //textBox1.Text += "Key press! ["+ e.KeyChar + "]\r\n";            
        }

        /// <summary>
        /// Get's the modifiers without a KeysEvent
        /// </summary>
        /// <returns></returns>
        private KeyModifiers GetWinModifiers()
        {
            KeyModifiers mods = new KeyModifiers();
            if (ModifierKeys != Keys.None)
            {                                          
                //While doing this Mouse down, a key has also been held! for example, shift
                mods.ShiftKeyDown = ModifierKeys.ToString().ToLower().Contains(Keys.Shift.ToString().ToLower());
                mods.CtrlKeyDown = ModifierKeys.ToString().ToLower().Contains(Keys.Control.ToString().ToLower());
                mods.AltKeyDown = ModifierKeys.ToString().ToLower().Contains(Keys.Alt.ToString().ToLower());                
            }
            return mods;
        }
        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            if (isPlaying)
                return;


            if (e.Button == MouseButtons.Left)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_LEFT_DOWN, VirtualKeyCode.NONAME, GetWinModifiers()));

            if (e.Button == MouseButtons.Middle)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_MIDDLE_DOWN, VirtualKeyCode.NONAME, GetWinModifiers())); 

            if (e.Button == MouseButtons.Right)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_RIGHT_DOWN, VirtualKeyCode.NONAME, GetWinModifiers()));            
        }

        private void GlobalHookMouseUpExt(object sender, MouseEventExtArgs e)
        {                        
            if (isPlaying)
                return;            

            if (e.Button == MouseButtons.Left)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_LEFT_UP, VirtualKeyCode.NONAME, GetWinModifiers()));

            if (e.Button == MouseButtons.Middle)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_MIDDLE_UP, VirtualKeyCode.NONAME, GetWinModifiers())); 

            if (e.Button == MouseButtons.Right)
                actions.Add(new Action(actions.Count, e.X, e.Y, 0, DateTime.Now, ActionType.MOUSE_RIGHT_UP, VirtualKeyCode.NONAME, GetWinModifiers()));
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {            
            if (isPlaying || isRecording)
                return;

            tbLog.AppendText("Starting recording.\r\n");

            isRecording = true;
            Subscribe();
            lblStatus.Text = "Recording";
            pbStatus.BackgroundImage = Properties.Resources.blueRecord;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tbLog.AppendText("Stopping recording/playback.\r\n");

            ReleaseKeyboardModifiers();

            materialProgressBar1.Invoke((MethodInvoker)(() =>
            {
                materialProgressBar1.Value = 0;
            }));

            isPlaying = false;
            Unsubscribe();

            if(playbackThread != null)
                playbackThread.Abort();
          
            lblStatus.Text = "Inactive";
            pbStatus.BackgroundImage = Properties.Resources.blueZzz;
        }

        private void btnPlayback_Click(object sender, EventArgs e)
        {
            if (isPlaying || actions == null || actions.Count == 0)
                return;
            
            tbLog.AppendText("Playing back.\r\n");

            tmrProgress.Start();
            lblStatus.Text = "Playing";
            pbStatus.BackgroundImage = Properties.Resources.bluePlay;
            playbackThread = new Thread(() =>
            {
                isPlaying = true;

                int repeatCount = (int)numRepeatTimes.Value;
                if (repeatCount == 0) repeatCount = 1;


                while (isPlaying) //Repeat endlessly until stop has been pressed / hotkey'd or until it has repeated x times (not infinite)
                    Playback();

                isPlaying = false;

                materialProgressBar1.Invoke((MethodInvoker)(() =>
                {
                    materialProgressBar1.Value = 0;
                }));

            });

            playbackThread.Start();
        }

        private void Playback()
        {
            int randomSleepTime = 0;
            int i = 0;
            int repeatCount = 0;
            TimeSpan totalTime = (actions[actions.Count - 1].TimeStamp - actions[0].TimeStamp);
            
            while (true)
            {
                //If counter exceeds actionCount, restart or stop
                if (i >= actions.Count - 1)
                {
                    i = 0;
                    repeatCount++;
                   
                    if (!cbRepeatForever.Checked && repeatCount >= numRepeatTimes.Value)
                    {
                        SetTextboxText("Stopping playback after " + numRepeatTimes.Value + " playbacks.\r\n");                        

                        ReleaseKeyboardModifiers();

                        isPlaying = false;
                        break;
                    }
                    
                    if (cbRandomizeInput.Checked)
                    {
                        if (random.Next(0, 4) > 2.5)
                        {
                            randomSleepTime = random.Next((int)numRandomSleep.Value);

                            SetTextboxText("[Random] Sleeping for " + randomSleepTime + " ms\r\n");

                            Thread.Sleep(randomSleepTime);
                        }
                    }

                    SetTextboxText("Playing back [" + repeatCount + "].\r\n");
                }

                if (isPlaying)               
                    PlayAction(actions[i], i); //"Play" the current action                

                i++;
            }
        }
        
        /// <summary>
        /// Set the textbox text from within a thread
        /// </summary>
        /// <param name="text"></param>
        private void SetTextboxText(string text)
        {
            tbLog.Invoke((MethodInvoker)(() =>
            {
                tbLog.AppendText(text);
            }));
        }

        /// <summary>
        /// "Plays" an action, moves the mouse, presses a button, etc
        /// </summary>
        /// <param name="a"></param>
        private void PlayAction(Action a, int actionCount)
        {
            currentAction = a;
            HoldDownKeys(a);

            //Move mouse
            #region move mouse
            if (cbRandomizeInput.Checked && a.X.HasValue)
            {
                //move the mouse differently a little bit
                if (random.NextDouble() >= 0.5)
                {
                    if (random.NextDouble() >= 0.5)
                        SetCursorPos((a.X.Value + random.Next(0, (int)numPixels.Value)), a.Y.Value);

                    else
                        SetCursorPos(a.X.Value, a.Y.Value + random.Next(0, (int)numPixels.Value));
                }
                else
                    SetCursorPos(a.X.Value, a.Y.Value);

            }
            else if(a.X.HasValue)//No random, just repeat mouse movements
            {
                SetCursorPos(a.X.Value, a.Y.Value);
            }
            #endregion

            //Scroll if it is an scroll action
            if (a.ScrollAmount != 0)
                inputSim.Mouse.VerticalScroll(a.ScrollAmount / 120);

            //Do a mouse click (including wheel button)
            #region Mouse click
            if (a.IsMouseClick) 
            {
                if (a.Type == ActionType.MOUSE_LEFT_DOWN)
                    mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)a.X, (uint)a.Y, 0, 0);
                if (a.Type == ActionType.MOUSE_LEFT_UP)
                    mouse_event(MOUSEEVENTF_LEFTUP, (uint)a.X, (uint)a.Y, 0, 0);


                if (a.Type == ActionType.MOUSE_MIDDLE_DOWN)
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, (uint)a.X, (uint)a.Y, 0, 0);
                if (a.Type == ActionType.MOUSE_MIDDLE_UP)
                    mouse_event(MOUSEEVENTF_MIDDLEUP, (uint)a.X, (uint)a.Y, 0, 0);


                if (a.Type == ActionType.MOUSE_RIGHT_DOWN)
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, (uint)a.X, (uint)a.Y, 0, 0);
                if (a.Type == ActionType.MOUSE_RIGHT_UP)
                    mouse_event(MOUSEEVENTF_RIGHTUP, (uint)a.X, (uint)a.Y, 0, 0);
            }
            #endregion

            //The action is a keyboard key input. Simulate it
            #region Key simulation
            if (a.IsKey) 
            {
                if (a.Modifiers.AltKeyDown || a.Modifiers.CtrlKeyDown || a.Modifiers.ShiftKeyDown)
                {
                    List<VirtualKeyCode> keyCodes = new List<VirtualKeyCode>();

                    if (a.Modifiers.AltKeyDown)
                        keyCodes.Add(VirtualKeyCode.MENU);

                    if (a.Modifiers.CtrlKeyDown)
                        keyCodes.Add(VirtualKeyCode.CONTROL);

                    if (a.Modifiers.ShiftKeyDown)
                        keyCodes.Add(VirtualKeyCode.SHIFT);

                    inputSim.Keyboard.ModifiedKeyStroke(keyCodes.ToArray(), a.Key);
                }
                else
                    inputSim.Keyboard.KeyPress(a.Key);
            }
            #endregion

            //Sleep (the recorded time) between actions
            #region sleep between actions
            if (actionCount < actions.Count - 1)
            {
                //Get the time difference between action x and action x+1 , the user might have waited 1.5 seconds before moving the mouse again, or 4 miliseconds
                TimeSpan timeToSleep = actions[actionCount + 1].TimeStamp - actions[actionCount].TimeStamp;

                if (cbRandomizeInput.Checked)
                {
                    //Sleep a tiny (random) bit between movements
                    if (random.NextDouble() >= 0.5)
                        Thread.Sleep(((int)timeToSleep.TotalMilliseconds) + random.Next(5));
                    else
                        Thread.Sleep(((int)timeToSleep.TotalMilliseconds));
                }
                else
                    Thread.Sleep((int)timeToSleep.TotalMilliseconds);
            }
            #endregion
        }


        private void HoldDownKeys(Action a)
        {
            //Hold down/up keys.             
            
            //ALT
            if (a.Modifiers.AltKeyDown && (Control.ModifierKeys & Keys.Alt) != Keys.Alt)            
                inputSim.Keyboard.KeyDown(VirtualKeyCode.MENU);            
            else if (!a.Modifiers.AltKeyDown && (Control.ModifierKeys & Keys.Alt) == Keys.Alt) //Don't send the KeyUp if the key isn't down            
                inputSim.Keyboard.KeyUp(VirtualKeyCode.MENU);           

            //SHIFT
            if (a.Modifiers.ShiftKeyDown && (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
                inputSim.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
            else if (!a.Modifiers.ShiftKeyDown && (Control.ModifierKeys & Keys.Shift) == Keys.Shift) //Don't send the KeyUp if the key isn't down
                inputSim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);

            //CONTROL
            if (a.Modifiers.CtrlKeyDown && (Control.ModifierKeys & Keys.Control) != Keys.Control)
                inputSim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            else if (!a.Modifiers.CtrlKeyDown && (Control.ModifierKeys & Keys.Control) == Keys.Control) //Don't send the KeyUp if the key isn't down
                inputSim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);            
        }
        private void cbRepeatForever_CheckedChanged(object sender, EventArgs e)
        {
            Database.RepeatForever = cbRepeatForever.Checked;

            if (tbLog.Text.Contains("Initialized MousePlayBack"))
                tbLog.AppendText("Repeating playbacks forever " + (cbRepeatForever.Checked ? "enabled.\r\n" : "disabled.\r\n"));
        }

        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            if (currentAction == null)
                return;

            if (!isPlaying)
            {
                lblStatus.Text = "Inactive";
                pbStatus.BackgroundImage = Properties.Resources.blueZzz;
                tmrProgress.Stop();
            }

            TimeSpan t = currentAction.TimeStamp - actions[0].TimeStamp;
            TimeSpan totalTime = (actions[actions.Count - 1].TimeStamp - actions[0].TimeStamp);

            if (totalTime.TotalMilliseconds <= 0)
                return;

            decimal currentActionMilis = (decimal)t.TotalMilliseconds;
            decimal totalMilis = (decimal)totalTime.TotalMilliseconds;            
            decimal percentage = (currentActionMilis / totalMilis) * 100m;

            materialProgressBar1.Value = (int)percentage;
        }

        private void materialMultiLineTextBox1_TextChanged(object sender, EventArgs e)
        {                        
            tbLog.SelectionStart = tbLog.Text.Length;
            tbLog.ScrollToCaret();
        }

        private void cbRandomizeInput_CheckedChanged(object sender, EventArgs e)
        {
            Database.RandomizeInput = cbRandomizeInput.Checked;

            if(tbLog.Text.Contains("Initialized MousePlayBack"))
                tbLog.AppendText("Randomizing input now " + (cbRandomizeInput.Checked ? "enabled.\r\n" : "disabled.\r\n"));
        }

      

        private void cbStartStopRecording_SelectedIndexChanged(object sender, EventArgs e)
        {                     
            Database.HotKeyStartStopRecording = cbStartStopRecording.SelectedItem.ToString();
            StartStopRecording = (Keys)kc.ConvertFromString(Database.HotKeyStartStopRecording);
        }

        private void cbPlaybackRecording_SelectedIndexChanged(object sender, EventArgs e)
        {                        
            Database.HotKeyPlaybackRecording = cbPlaybackRecording.SelectedItem.ToString();
            PlaybackRecording = (Keys)kc.ConvertFromString(Database.HotKeyPlaybackRecording);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(actions.Count > 0)
            {
                string path = FSManager.Folders.GetSelectedFolderPath();

                if (!string.IsNullOrWhiteSpace(path))
                {                    
                    if(IO.Serialize(actions, path + "\\Playback.dat"))
                        tbLog.AppendText("Export Success. File: " + path + "\\Playback.dat\r\n");
                    else
                        tbLog.AppendText("Export failed   [SerializationException]\r\n");
                }
                else
                    tbLog.AppendText("Export failed. No path selected\r\n");
            }
            else
                tbLog.AppendText("Nothing to export.\r\n");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string path = FSManager.Files.GetSelectedFileWithPath("MousePlayback backup files", "*.dat");

            if(!string.IsNullOrWhiteSpace(path))
            {
                List<Action> tempActions = IO.Deserialize(path);

                if (tempActions != null && tempActions.Count > 0)
                {
                    tempActions = tempActions.OrderBy(a => a.TimeStamp).ToList();
                    actions = tempActions;
                }
                else
                {
                    tbLog.AppendText("Could not import this file. \r\n");
                    return;
                }

                tempActions = null;

                tbLog.AppendText("Import successfull. You can now press Playback or shortcut key " + Database.HotKeyPlaybackRecording + "\r\n");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseKeyboardModifiers();
        }

        private void ReleaseKeyboardModifiers()
        {
            //release all modifier keys 
            inputSim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            inputSim.Keyboard.KeyUp(VirtualKeyCode.MENU);
            inputSim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
            //Incase there was still a mouse middle down event active, release it
            mouse_event(MOUSEEVENTF_MIDDLEUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
        }

        private void tmrFadeIn_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.15;
            if (this.Opacity >= 1)
                tmrFadeIn.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            materialTabControl1.Select();
        }

        private void numRepeatTimes_ValueChanged(object sender, EventArgs e)
        {
            //Don't update the db everytime the value is changed, for spam clicking
            new Thread(() =>
            {
                int currVal = (int)numRepeatTimes.Value;

                Thread.Sleep(1000);

                //Still same value?
                if (currVal == (int)numRepeatTimes.Value)
                    Database.RepeatTimes = (int)numRepeatTimes.Value;

            }).Start();
        }

        private void numRandomSleep_ValueChanged(object sender, EventArgs e)
        {
            //Don't update the db everytime the value is changed, for spam clicking
            new Thread(() =>
            {
                int currVal = (int)numRandomSleep.Value;

                Thread.Sleep(1000);

                //Still same value?
                if (currVal == (int)numRandomSleep.Value)
                    Database.RndSleepTime = (int)numRandomSleep.Value;

            }).Start();
        }

        private void numPixels_ValueChanged(object sender, EventArgs e)
        {
            //Don't update the db everytime the value is changed, for spam clicking
            new Thread(() =>
            {
                int currVal = (int)numPixels.Value;

                Thread.Sleep(1000);

                //Still same value?
                if (currVal == (int)numPixels.Value)
                    Database.RndPixels = (int)numPixels.Value;

            }).Start();
        }
    }
}
