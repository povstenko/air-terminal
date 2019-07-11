using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AirTerminal
{
	
	public partial class fMain : Form
	{
		List<Person> persons;
		public fMain()
		{
			InitializeComponent();
			persons = new List<Person>();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			fAdd f = new fAdd("Add New Client");
			if(f.ShowDialog() == DialogResult.OK)
			{
				persons.Add(new Person(f.tbxFName.Text, f.tbxSName.Text, f.tbxTName.Text, Convert.ToInt32(f.nudThings.Value), Convert.ToDouble(f.tbxWeight.Text), Convert.ToInt32(f.nudNumber.Value), f.dtpDate.Value, f.dtpTime.Value, Convert.ToDouble(f.tbxDuration.Text), f.tbxDestination.Text));
				lsbxPersons.DataSource = null;
				lsbxPersons.DataSource = persons;
			}
		}
		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (lsbxPersons.SelectedIndex != -1)
			{
				if(MessageBox.Show("Are you sure to remove selected client from list?", "Remove Selected Client", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					persons.RemoveAt(lsbxPersons.SelectedIndex);
					lsbxPersons.DataSource = null;
					lsbxPersons.DataSource = persons;
				}
			}
		}
		private void btnEdit_Click(object sender, EventArgs e)
		{
			if(lsbxPersons.SelectedIndex != -1)
			{
				fAdd f = new fAdd("Edit Selected Client");
				f.tbxFName.Text = persons[lsbxPersons.SelectedIndex].firstName;
				f.tbxSName.Text = persons[lsbxPersons.SelectedIndex].secondName;
				f.tbxTName.Text = persons[lsbxPersons.SelectedIndex].thirdName;
				f.nudThings.Value = Convert.ToDecimal(persons[lsbxPersons.SelectedIndex].baggageThings);
				f.tbxWeight.Text = persons[lsbxPersons.SelectedIndex].baggageWeight.ToString();
				f.nudNumber.Value = Convert.ToDecimal(persons[lsbxPersons.SelectedIndex].flightNumber);
				f.dtpDate.Value = persons[lsbxPersons.SelectedIndex].flightDate;
				f.dtpTime.Value = persons[lsbxPersons.SelectedIndex].flightTime;
				f.tbxDuration.Text = persons[lsbxPersons.SelectedIndex].flightDuration.ToString();
				f.tbxDestination.Text = persons[lsbxPersons.SelectedIndex].destination;

				if (f.ShowDialog() == DialogResult.OK)
				{
					persons.RemoveAt(lsbxPersons.SelectedIndex);
					persons.Insert(lsbxPersons.SelectedIndex, new Person(f.tbxFName.Text, f.tbxSName.Text, f.tbxTName.Text, Convert.ToInt32(f.nudThings.Value), Convert.ToDouble(f.tbxWeight.Text), Convert.ToInt32(f.nudNumber.Value), f.dtpDate.Value, f.dtpTime.Value, Convert.ToDouble(f.tbxDuration.Text), f.tbxDestination.Text));
					lsbxPersons.DataSource = null;
					lsbxPersons.DataSource = persons;
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
			SaveFileDialog dialog = new SaveFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				using (Stream fs = File.Create(dialog.FileName))
				{
					serializer.Serialize(fs, persons);
				}
			}
		}
		private void btnLoad_Click(object sender, EventArgs e)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
			OpenFileDialog dialog = new OpenFileDialog();
			persons = null;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				using (Stream fs = File.OpenRead(dialog.FileName))
				{
					persons = (List<Person>)serializer.Deserialize(fs);
					lsbxPersons.DataSource = null;
					lsbxPersons.DataSource = persons;
				}
			}
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void lsbxPersons_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(lsbxPersons.SelectedIndex != -1)
			{
				firstName.Text = persons[lsbxPersons.SelectedIndex].firstName;
				secondName.Text = persons[lsbxPersons.SelectedIndex].secondName;
				thirdName.Text = persons[lsbxPersons.SelectedIndex].thirdName;

				baggageThings.Text = persons[lsbxPersons.SelectedIndex].baggageThings.ToString();
				baggageWeight.Text = persons[lsbxPersons.SelectedIndex].baggageWeight.ToString();

				flightNumber.Text = persons[lsbxPersons.SelectedIndex].flightNumber.ToString();
				flightDate.Text = persons[lsbxPersons.SelectedIndex].flightDate.ToShortDateString();
				flightTime.Text = persons[lsbxPersons.SelectedIndex].flightTime.ToShortTimeString();
				flightDuration.Text = persons[lsbxPersons.SelectedIndex].flightDuration.ToString();
				destination.Text = persons[lsbxPersons.SelectedIndex].destination;
			}
		}

		private void lsbxPersons_MouseUp(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.Right);
			}
		}
	}
}
