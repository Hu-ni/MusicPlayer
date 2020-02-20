using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer
{
    public class MusicException : Exception
    {
        public override string Message => "에러가 발생했습니다.";
        public MusicException(string message)
        {
            MessageBox.Show(message, "에러");
        }

        public void ShowMessage()
        {
            MessageBox.Show(Message, "에러");
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "에러");
        }
    }
}
