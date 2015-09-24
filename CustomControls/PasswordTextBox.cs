using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class PasswordTextBox : TextBox
    {
        private StringBuilder password;

        public PasswordTextBox()
        {
            password = new StringBuilder();
        }

        public string Password
        {
            get
            {
                return password.ToString();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedCharacters(this, e.KeyCode);
                MaskPassword();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            int selectionStart = this.SelectionStart;
            int length = this.TextLength;
            int selectedChars = this.SelectionLength;
            
            Keys keyModifed = (Keys)e.KeyChar;

            //Only accept a-z, A-Z and @
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != '@')
            {
                //Need to handle special keys like DELETE and BACK
               if ((Keys.Back == keyModifed) || (Keys.Delete == keyModifed))
                {
                    DeleteSelectedCharacters(this, keyModifed);
                    
                }
                e.Handled = true;
            }
            else
            {                
                if (selectedChars > 0)
                {
                    //Need to remove the selected chars first
                    password.Remove(selectionStart, selectedChars);
                }
                password.Insert(this.SelectionStart, e.KeyChar.ToString());
            }

            //Hide password
            MaskPassword();
        }

        protected override void OnTextChanged(EventArgs e)
        {            
            base.OnTextChanged(e);
            MaskPassword();            
        }

        private void MaskPassword()
        {
            int index = this.SelectionStart;
            if (index >= 0)
            {
                //Show the text box with the masked symbol
                this.Text = new string('*', password.Length);
                this.SelectionStart = index;
            }            
        }

        private void DeleteSelectedCharacters(object sender, Keys key)
        {
            int selectionStart = this.SelectionStart;
            int length = this.TextLength;
            int selectedChars = this.SelectionLength;

            if (selectedChars == length)
            {
                ClearCharBufferPlusTextBox();
                return;
            }

            if (selectedChars > 0)
            {
                int i = selectionStart;
                this.Text = this.Text.Remove(selectionStart, selectedChars);
                password.Remove(selectionStart, selectedChars);
            }
            else
            {
                 // Handle the condition when the cursor is placed at the start or in the end or in between
                if (selectionStart == 0)
                {
                    
                    // Cursor is before the first character 
                    // Delete the character only when Del is pressed, No action when Back key is pressed
                    
                    if (key == Keys.Delete)
                    {
                        password.Remove(0, 1);
                    }
                }
                else if (selectionStart > 0 && selectionStart < length)
                {
                    
                    // Cursor position anywhere in between 
                    // Backspace and Delete have the same effect
                    
                    if (key == Keys.Back || key == Keys.Delete)
                    {
                        password.Remove(selectionStart, 1);
                    }
                }
                else if (selectionStart == length)
                {
                    
                    // Cursor at the end, after the last character 
                    // Delete the character only when Back key is pressed, No action when Delete key is pressed                    
                    if (key == Keys.Back)
                    {
                        password.Remove(selectionStart - 1, 1);
                    }
                }
            }

            //Reset SelectionStart
            this.Select((selectionStart > this.Text.Length ? this.Text.Length : selectionStart), 0);
        }
        
        private void ClearCharBufferPlusTextBox()
        {
            password = new StringBuilder();
            this.Clear();
        }
    }
}
