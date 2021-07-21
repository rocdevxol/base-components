using System.Collections.ObjectModel;

namespace Models
{
	public class ReportComponents
	{
		/// <summary>
		/// Выходной перечень по компонентам, для закупки
		/// </summary>
		//public Dictionary<Components.Component, int> Report { get; set; }
		public ObservableCollection<Components.Component> Report { get; set; }

		/// <summary>
		/// Перечень компонентов из которых необходимо сделать выходной список
		/// </summary>
		private readonly ObservableCollection<Components.Component> ComponentsList;

		public ReportComponents()
		{
			ComponentsList = new ObservableCollection<Components.Component>();
			//Report = new Dictionary<Components.Component, int>();
			Report = new ObservableCollection<Components.Component>();
		}

		public ReportComponents(Boards.Board board)
		{
			ComponentsList = new ObservableCollection<Components.Component>();
			Report = new ObservableCollection<Components.Component>();
			AddComponents(board);
		}

		public ReportComponents(Components.ComponentList componentList)
		{
			ComponentsList = new ObservableCollection<Components.Component>();
			Report = new ObservableCollection<Components.Component>();
			AddComponents(componentList);
		}

		public ReportComponents(ObservableCollection<Components.Component> components)
		{
			ComponentsList = new ObservableCollection<Components.Component>();
			Report = new ObservableCollection<Components.Component>();
			AddComponents(components);
		}

		public void AddComponents(Boards.Board board)
		{
			ObservableCollection<Components.Component> temp;
			if (board.EnableToCalc == true)
			{
				temp = new ObservableCollection<Components.Component>();
				foreach (Components.Component component in board.GetComponentList().Components)
				{
					temp.Add((Components.Component)component.Clone());
				}

				foreach (Components.Component component in temp)
				{
					component.Count *= board.Count;
				}
			}
			else
			{
				temp = new ObservableCollection<Components.Component>();
			}

			AddLocalComponents(temp);
		}

		public void AddComponents(Components.ComponentList componentList)
		{
			ObservableCollection<Components.Component> temp = new ObservableCollection<Components.Component>(componentList.Components);
			AddLocalComponents(temp);
		}

		public void AddComponents(ObservableCollection<Components.Component> components)
		{
			ObservableCollection<Components.Component> temp = new ObservableCollection<Components.Component>(components);
			AddLocalComponents(temp);
		}

		private void AddLocalComponents(ObservableCollection<Components.Component> components)
		{
			foreach (Components.Component component in components)
			{
				ComponentsList.Add((Components.Component)component.Clone());
			}
		}

		public ObservableCollection<Components.Component> UpdateReport()
		{
			//Report = new Dictionary<Components.Component, int>();
			Report = new ObservableCollection<Components.Component>();
			foreach (Components.Component component in ComponentsList)
			{
				if (!component.Soldering)
				{
					continue;
				}

				bool change = false;
				int index = FindElement(component, ref change);
				if (index != -1)
				{
					if (change == false)
					{
						Report[index].UpdateRefDes(component.RefDes);
						Report[index].UpdateCount(component.Count);
					}
					else
					{
						Components.Component oldComponent = Report[index];
						Report.RemoveAt(index);
						Report.Insert(index, component);
						Report[index].UpdateCount(oldComponent.Count);
					}
				}
				else
				{
					Report.Add(component);
				}
			}
			return Report;
		}

		private int FindElement(Components.Component component, ref bool change)
		{
			int index = -1;
			change = false;

			foreach (Components.Component item in Report)
			{
				if (item.CompareTo(component) == 0)
				{
					index = Report.IndexOf(item);
					break;
				}
				else // Частичное совпадение или полное не соответствие
				{
					/*					if (item.Description == component.Description)
										{
					//						StringBuilder builder = new StringBuilder();
					//						builder.Append("Не соответствие компонентов")


											CustomControls.UserMessageBox messageBox = new CustomControls.UserMessageBox("Подготовка компонентов", "test", CustomControls.UserMessageBox.UserMessageButtons.Apply2Cancel);
											messageBox.ShowDialog();
											if (messageBox.DialogResult == CustomControls.UserMessageBox.UserMessageResult.Apply1)
											{
												index = Report.IndexOf(item);
												break;
											}
											else if (messageBox.DialogResult == CustomControls.UserMessageBox.UserMessageResult.Apply2)
											{
												index = Report.IndexOf(item);
												change = true;
												break;
											}
										}*/
				}
			}
			return index;
		}

		private Components.Component PrepareLocalComponent(Components.Component inComponent)
		{
			Components.Component outComponent = new Components.Component(string.Empty)
			{
				Description = inComponent.Description
			};
			outComponent.Names.Add(inComponent.Names[0]);

			return outComponent;
		}
	}
}
