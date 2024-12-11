using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace guess_or_shutdown
{
    public partial class form1 : Form
    {

        private int generatedNumber;
        private int roundNumber;

        public form1()
        {
            InitializeComponent();
            roundNumber = 1;
            GenerateRandomNumber();
        }

        private void form1_Load(object sender, EventArgs e)
        {

            string agreement = "     By continuing, you agree that I breathemonoxide (co / kayla)\n" +
                      "am not responsible for any damage caused while using this program.";   // strings for msgbox
            string title = "guess or shutdown";

            DialogResult choice = MessageBox.Show(
                agreement,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,               //who is even reading this anyway
                MessageBoxDefaultButton.Button2
            );

            switch (choice)
            {
                case DialogResult.Yes:

                    MessageBox.Show("Be sure to close any unsaved work!", "guess or shutdown", MessageBoxButtons.OK, MessageBoxIcon.Information);    // if you choose yes well you get to play
                    MessageBox.Show("Round 1: Enter a number between 1 and 2");
                    break;

                case DialogResult.No:

                    MessageBox.Show("You must accept to play the game!", "guess or shutdown", MessageBoxButtons.OK, MessageBoxIcon.Error);     // and if you choose no you don't
                    Application.Exit();
                    break;

                default:
                    Application.Exit();   // and if you try to cheat the system...
                    break;
            }


        }



        private void GenerateRandomNumber()
        {
            Random random = new Random();
            int maxNumber = GetMaxNumberForRound();
            generatedNumber = random.Next(1, maxNumber + 1); 
        }

        private int GetMaxNumberForRound()
        {
            switch (roundNumber)
            {
                case 1:
                    return 2;
                case 2:
                    return 5;
                case 3:
                    return 10;
                case 4:
                    return 15;
                case 5:
                    return 20;
                case 6:
                    return 30;
                case 7:
                    return 50;
                case 8:
                    return 75;
                case 9:
                    return 85;
                case 10:
                    return 100; // are you a god or something!?

                default:
                    return 2; 
            }
        }

        private string RoundInfo()
        {
            int maxNumber = GetMaxNumberForRound();
            return $"Round {roundNumber}: Enter a number between 1 and {maxNumber}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int userInput))
            {
                if (userInput != generatedNumber)
                {
                    Shutdown();   //run the script   
                }
                else
                {
                    MessageBox.Show("You guessed correctly! Well done!", "guess or shutdown", MessageBoxButtons.OK, MessageBoxIcon.Information);

          
                    roundNumber++;
                    if (roundNumber > 10) roundNumber = 10; 
                }

                GenerateRandomNumber();

                MessageBox.Show(RoundInfo(), "guess or shutdown", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a valid number.", "guess or shutdown", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Shutdown()
        {
            try
            {
                string scriptPath = Path.Combine(Path.GetTempPath(), "shutdown.bat");
                string scriptContent = "shutdown /s /t 1";
                File.WriteAllText(scriptPath, scriptContent);
                Process.Start(new ProcessStartInfo
                {
                    FileName = scriptPath,
                    UseShellExecute = true,  
                    CreateNoWindow = true    
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to execute shutdown: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}





    

