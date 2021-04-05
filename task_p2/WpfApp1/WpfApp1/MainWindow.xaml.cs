using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1 {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}
		Regex digitPattern = new Regex(@"D\d");
		/*
		 * Сделал 2 способа: в 1 мы обращаемся к событию изменения текста и делаем новое событие для изменения
		 * введенного символа на пробел. Более правильный способ, как по мне.
		 * Второй способ - просто влоб добавить к тексту пробел вместо введенной цифры, а потом передвинуть каретку
		 * на конец строчки. В wpf почему-то каретка съезжает при форматировании текста. 
		 */
		private void tbKeyPressed(object sender, KeyEventArgs e) {
			if (digitPattern.IsMatch(e.Key.ToString())) {
#if false
				e.Handled = true;
				var customEvent = new TextCompositionEventArgs(Keyboard.PrimaryDevice, new TextComposition(InputManager.Current, Keyboard.FocusedElement, " "));
				customEvent.RoutedEvent = TextInputEvent;
				InputManager.Current.ProcessInput(customEvent);
#endif
#if true
				e.Handled = true;
				textBox.Text += " ";
				textBox.CaretIndex = textBox.Text.Length;
#endif
			}

		}
	}
}
