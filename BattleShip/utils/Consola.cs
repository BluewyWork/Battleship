using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.utils
{
    public class Consola
    {
        private static Consola instance;
        private StringWriter output;
        private TextBox textBox;

        private Consola(TextBox textBox)
        {
            this.textBox = textBox;
            output = new StringWriter();
            Console.SetOut(output);
        }

        public static Consola GetInstance(TextBox textBox) 
        {
            if (instance == null)
            {
                instance = new Consola(textBox);
            }
            return instance;
        }

        public void Write(string text)
        {
            this.textBox.AppendText(text);
        }

        public void WriteLine(string text)
        {
            this.textBox.AppendText(text + Environment.NewLine);
        }

        public void WriteLine()
        {
            WriteLine(string.Empty);
        }

        public void Clear()
        {
            this.textBox.Clear();
            output.GetStringBuilder().Clear();
        }

        public override string? ToString()
        {
            return output.ToString();
        }
    }
}
