using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPODESIGN
{
    class PHTextBox:System.Windows.Forms.TextBox
    {
        System.Drawing.Color DefaultColor;
        public String PaceHolderText
        {
            get;set;
        }
        public PHTextBox(String placeholdertext)
        {
            DefaultColor = this.ForeColor;
            this.GotFocus += (object sender, EventArgs e) =>
              {
                  this.Text = String.Empty;
                  this.ForeColor = DefaultColor;
              };
            this.LostFocus+= (object sender, EventArgs e) =>
            {
              
                this.ForeColor = DefaultColor;
            };

            if (!string.IsNullOrEmpty(placeholdertext))
            {
                this.ForeColor = System.Drawing.Color.Gray;
                PaceHolderText = placeholdertext;
                this.Text = placeholdertext;

            }
        }
    }
}
