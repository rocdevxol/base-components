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
using System.Windows.Shapes;

namespace CustomControls
{
	/// <summary>
	/// Логика взаимодействия для UserMessageBox.xaml
	/// </summary>
	public partial class UserMessageBox : Window
	{
		public enum UserMessageButtons
		{
			/// <summary>
			/// ОК
			/// </summary>
			Ok,
			/// <summary>
			/// OK, Отмена
			/// </summary>
			OkCancel,
			/// <summary>
			/// Да, Нет
			/// </summary>
			YesNo,
			/// <summary>
			/// Да, Нет, Отмена
			/// </summary>
			YesNoCancel,
			/// <summary>
			/// Применить, Отмена
			/// </summary>
			ApplyCancel,
			/// <summary>
			/// Применить 1й вариант, Применить 2ой вариант, Отмена
			/// </summary>
			Apply2Cancel
		}

		public enum UserMessageResult
		{
			/// <summary>
			/// Окно закрыто без выбора
			/// </summary>
			Empty,
			/// <summary>
			/// ОК
			/// </summary>
			Ok,
			/// <summary>
			/// Да
			/// </summary>
			Yes,
			/// <summary>
			/// Нет
			/// </summary>
			No,
			/// <summary>
			/// Применить
			/// </summary>
			Apply,
			/// <summary>
			/// Выбор 1-го элемента
			/// </summary>
			Apply1,
			/// <summary>
			/// Выбор 2-го элемента
			/// </summary>
			Apply2,
			/// <summary>
			/// Отмена
			/// </summary>
			Cancel
		}

		private string content;
		public new string Content
		{
			get => content;
			set
			{
				if (value != content)
				{
					content = value;
					textBlockContent.Text = value;
				}
			}
		}

		private readonly UserMessageButtons Buttons;

		public new UserMessageResult DialogResult;

		public UserMessageBox(string title, string content, UserMessageButtons buttons)
		{
			InitializeComponent();
			Title = title;
			Content = content;
			Buttons = buttons;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DialogResult = UserMessageResult.Empty;
			switch (Buttons)
			{
				case UserMessageButtons.Ok:
					buttonOkYesApply1.Visibility = Visibility.Visible;
					buttonNoApply2.Visibility = Visibility.Collapsed;
					buttonCancel.Visibility = Visibility.Collapsed;

					buttonOkYesApply1.Content = "OK";
					buttonNoApply2.Content = string.Empty;
					buttonCancel.Content = string.Empty;
					break;
				case UserMessageButtons.OkCancel:
					buttonOkYesApply1.Visibility = Visibility.Visible;
					buttonNoApply2.Visibility = Visibility.Collapsed;
					buttonCancel.Visibility = Visibility.Visible;

					buttonOkYesApply1.Content = "OK";
					buttonNoApply2.Content = string.Empty;
					buttonCancel.Content = "Отмена";
					break;
				case UserMessageButtons.YesNo:
					buttonOkYesApply1.Visibility = Visibility.Visible;
					buttonNoApply2.Visibility = Visibility.Visible;
					buttonCancel.Visibility = Visibility.Collapsed;

					buttonOkYesApply1.Content = "Да";
					buttonNoApply2.Content = "Нет";
					buttonCancel.Content = string.Empty;
					break;
				case UserMessageButtons.YesNoCancel:
					buttonOkYesApply1.Visibility = Visibility.Visible;
					buttonNoApply2.Visibility = Visibility.Visible;
					buttonCancel.Visibility = Visibility.Visible;

					buttonOkYesApply1.Content = "Да";
					buttonNoApply2.Content = "Нет";
					buttonCancel.Content = "Отмена";
					break;
				case UserMessageButtons.ApplyCancel:
					buttonOkYesApply1.Visibility = Visibility.Visible;
					buttonNoApply2.Visibility = Visibility.Collapsed;
					buttonCancel.Visibility = Visibility.Visible;

					buttonOkYesApply1.Content = "Применить";
					buttonNoApply2.Content = string.Empty;
					buttonCancel.Content = "Отмена";
					break;
				case UserMessageButtons.Apply2Cancel:
					buttonOkYesApply1.Visibility = Visibility.Visible;
					buttonNoApply2.Visibility = Visibility.Visible;
					buttonCancel.Visibility = Visibility.Visible;

					buttonOkYesApply1.Content = "Применить 1";
					buttonNoApply2.Content = "Применить 2";
					buttonCancel.Content = "Отмена";
					break;
			}
		}

		private void ButtonOkYesApply1_Click(object sender, RoutedEventArgs e)
		{
			switch (Buttons)
			{
				case UserMessageButtons.Ok:
					DialogResult = UserMessageResult.Ok;
					break;
				case UserMessageButtons.OkCancel:
					DialogResult = UserMessageResult.Ok;
					break;
				case UserMessageButtons.YesNo:
					DialogResult = UserMessageResult.Yes;
					break;
				case UserMessageButtons.YesNoCancel:
					DialogResult = UserMessageResult.Yes;
					break;
				case UserMessageButtons.ApplyCancel:
					DialogResult = UserMessageResult.Apply;
					break;
				case UserMessageButtons.Apply2Cancel:
					DialogResult = UserMessageResult.Apply1;
					break;
			}
			Close();
		}

		private void ButtonNoApply2_Click(object sender, RoutedEventArgs e)
		{
			switch (Buttons)
			{
				case UserMessageButtons.Ok:
					DialogResult = UserMessageResult.Empty;
					return;
				case UserMessageButtons.OkCancel:
					DialogResult = UserMessageResult.Empty;
					return;
				case UserMessageButtons.YesNo:
					DialogResult = UserMessageResult.No;
					break;
				case UserMessageButtons.YesNoCancel:
					DialogResult = UserMessageResult.No;
					break;
				case UserMessageButtons.ApplyCancel:
					DialogResult = UserMessageResult.Empty;
					return;
				case UserMessageButtons.Apply2Cancel:
					DialogResult = UserMessageResult.Apply2;
					break;
			}
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			switch (Buttons)
			{
				case UserMessageButtons.Ok:
					DialogResult = UserMessageResult.Empty;
					return;
				case UserMessageButtons.OkCancel:
					DialogResult = UserMessageResult.Cancel;
					break;
				case UserMessageButtons.YesNo:
					DialogResult = UserMessageResult.Empty;
					return;
				case UserMessageButtons.YesNoCancel:
					DialogResult = UserMessageResult.Cancel;
					break;
				case UserMessageButtons.ApplyCancel:
					DialogResult = UserMessageResult.Cancel;
					break;
				case UserMessageButtons.Apply2Cancel:
					DialogResult = UserMessageResult.Cancel;
					break;
			}
			this.Close();
		}
	}
}
