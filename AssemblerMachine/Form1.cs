using AssemblerMachine.DataAccess;
using AssemblerMachine.Model;
using AssemblerMachine.Services;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblerMachine
{
	public partial class Form1 : Form
	{
		private readonly IAssembleService _assembleService;
		private readonly ICarService _carService;

		public Form1(IAssembleService assembleService, ICarService carService)
		{
			InitializeComponent();
			InitializeUI();
			_assembleService = assembleService;
			_carService = carService;
		}

		// Initializes the UI components (labels, textboxes, comboboxes, buttons, DataGridView, and ProgressBar)
		private void InitializeUI()
		{
			#region Design
			// Set form properties
			this.Text = "Assembler Driver";
			this.Size = new Size(950, 600);
			this.BackColor = Color.White;

			// Title label
			Label lblTitle = new Label
			{
				Text = "Assembler Driver",
				Font = new Font("Segoe UI", 16, FontStyle.Bold),
				Location = new Point(20, 20),
				AutoSize = true
			};
			this.Controls.Add(lblTitle);

			// Arrays for labels and textboxes
			string[] labels = { "Wheel Numbers", "Door Numbers", "Glass Numbers", "Motor Numbers" };
			TextBox[] textBoxes = new TextBox[4];

			// Create labels and textboxes for number inputs
			for (int i = 0; i < labels.Length; i++)
			{
				Label lbl = new Label
				{
					Text = labels[i],
					Location = new Point(20, 70 + i * 40),
					Size = new Size(120, 25)
				};
				this.Controls.Add(lbl);

				textBoxes[i] = new TextBox
				{
					Name = $"txt{labels[i].Replace(" ", "")}",
					Location = new Point(150, 70 + i * 40),
					Size = new Size(180, 25)
				};
				this.Controls.Add(textBoxes[i]);
			}

			// Arrays for combobox labels and comboboxes
			string[] comboLabels = { "Wheel Types", "Door Types", "Glass Types", "Motor Power" };
			ComboBox[] comboBoxes = new ComboBox[4];

			// Create labels and comboboxes for type selections
			for (int i = 0; i < comboLabels.Length; i++)
			{
				Label lbl = new Label
				{
					Text = comboLabels[i],
					Location = new Point(380, 70 + i * 40),
					Size = new Size(120, 25)
				};
				this.Controls.Add(lbl);

				comboBoxes[i] = new ComboBox
				{
					Name = $"cmb{comboLabels[i].Replace(" ", "")}",
					Location = new Point(510, 70 + i * 40),
					Size = new Size(180, 25),
					DropDownStyle = ComboBoxStyle.DropDownList
				};

				// Set specific items for each ComboBox based on the category
				switch (comboLabels[i])
				{
					case "Wheel Types":
						comboBoxes[i].Items.AddRange(new string[] { "BigWheel", "SmallWheel", "NormalWheel", "ThinWheel" });
						break;
					case "Door Types":
						comboBoxes[i].Items.AddRange(new string[] { "Steel", "Titanium", "Aluminum", "Plastic" });
						break;
					case "Glass Types":
						comboBoxes[i].Items.AddRange(new string[] { "Bullet Proof", "Thin", "Thick", "5 Layers" });
						break;
					case "Motor Power":
						comboBoxes[i].Items.AddRange(new string[] { "10 Horse", "50 Horse", "100 Horse" });
						break;
				}
				this.Controls.Add(comboBoxes[i]);
			}

			// Toggle visibility of ComboBoxes based on numbers entered
			TextBox txtDoorNumbers = this.Controls["txtDoorNumbers"] as TextBox;
			ComboBox cmbDoorTypes = this.Controls["cmbDoorTypes"] as ComboBox;
			if (txtDoorNumbers != null && cmbDoorTypes != null)
			{
				txtDoorNumbers.TextChanged += (s, e) =>
				{
					if (int.TryParse(txtDoorNumbers.Text, out int num) && num <= 0)
						cmbDoorTypes.Visible = false;
					else
						cmbDoorTypes.Visible = true;
				};
			}

			TextBox txtGlassNumbers = this.Controls["txtGlassNumbers"] as TextBox;
			ComboBox cmbGlassTypes = this.Controls["cmbGlassTypes"] as ComboBox;
			if (txtGlassNumbers != null && cmbGlassTypes != null)
			{
				txtGlassNumbers.TextChanged += (s, e) =>
				{
					if (int.TryParse(txtGlassNumbers.Text, out int num) && num <= 0)
						cmbGlassTypes.Visible = false;
					else
						cmbGlassTypes.Visible = true;
				};
			}

			TextBox txtWheelNumbers = this.Controls["txtWheelNumbers"] as TextBox;
			ComboBox cmbWheelTypes = this.Controls["cmbWheelTypes"] as ComboBox;
			if (txtWheelNumbers != null && cmbWheelTypes != null)
			{
				txtWheelNumbers.TextChanged += (s, e) =>
				{
					if (int.TryParse(txtWheelNumbers.Text, out int num) && num <= 0)
						cmbWheelTypes.Visible = false;
					else
						cmbWheelTypes.Visible = true;
				};
			}

			TextBox txtMotorNumbers = this.Controls["txtMotorNumbers"] as TextBox;
			ComboBox cmbMotorPower = this.Controls["cmbMotorPower"] as ComboBox;
			if (txtMotorNumbers != null && cmbMotorPower != null)
			{
				txtMotorNumbers.TextChanged += (s, e) =>
				{
					if (int.TryParse(txtMotorNumbers.Text, out int num) && num <= 0)
						cmbMotorPower.Visible = false;
					else
						cmbMotorPower.Visible = true;
				};
			}

			// Assemble button
			Button btnAssemble = new Button
			{
				Text = "Assemble",
				Name = "btnAssemble",
				Location = new Point(230, 250),
				Size = new Size(130, 40),
				BackColor = Color.IndianRed,
				ForeColor = Color.White,
				Font = new Font("Segoe UI", 10, FontStyle.Bold)
			};
			btnAssemble.Click += BtnAssemble_Click;
			this.Controls.Add(btnAssemble);

			// Sold Vehicles button
			Button btnSold = new Button
			{
				Text = "Sold Vehicles",
				Name = "btnSoldVehicles",
				Location = new Point(380, 250),
				Size = new Size(150, 40),
				BackColor = Color.IndianRed,
				ForeColor = Color.White,
				Font = new Font("Segoe UI", 10, FontStyle.Bold)
			};
			btnSold.Click += BtnSold_Click;
			this.Controls.Add(btnSold);

			// Sold Vehicles label
			Label lblSold = new Label
			{
				Text = "Sold Vehicles",
				Location = new Point(20, 310),
				Font = new Font("Segoe UI", 14, FontStyle.Bold),
				AutoSize = true
			};
			this.Controls.Add(lblSold);

			// DataGridView for sold vehicles
			DataGridView dgv = new DataGridView
			{
				Name = "dataGridSoldVehicles",
				Location = new Point(20, 350),
				Size = new Size(890, 180),
				ReadOnly = true,
				AllowUserToAddRows = false,
				AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
			};
			this.Controls.Add(dgv);

			// ProgressBar for assembly process
			ProgressBar progressBar = new ProgressBar
			{
				Name = "progressAssembly",
				Location = new Point(230, 300),
				Size = new Size(300, 20),
				Style = ProgressBarStyle.Marquee,
				Visible = false
			};
			this.Controls.Add(progressBar);
			#endregion
		}

		// Handles the Assemble button click event
		private async void BtnAssemble_Click(object sender, EventArgs e)
		{
			// Retrieve all input fields (TextBoxes and ComboBoxes)
			TextBox txtWheelNumber = this.Controls["txtWheelNumbers"] as TextBox;
			ComboBox cmbWheelType = this.Controls["cmbWheelTypes"] as ComboBox;
			TextBox txtDoorNumber = this.Controls["txtDoorNumbers"] as TextBox;
			ComboBox cmbDoorType = this.Controls["cmbDoorTypes"] as ComboBox;
			TextBox txtGlassNumber = this.Controls["txtGlassNumbers"] as TextBox;
			ComboBox cmbGlassType = this.Controls["cmbGlassTypes"] as ComboBox;
			TextBox txtMotorNumber = this.Controls["txtMotorNumbers"] as TextBox;
			ComboBox cmbMotorPower = this.Controls["cmbMotorPower"] as ComboBox;
			ProgressBar progressBar = this.Controls["progressAssembly"] as ProgressBar;
			Button btnAssemble = this.Controls["btnAssemble"] as Button;

			// Array to store fields for validation (TextBox, ComboBox, and field name)
			var numberFields = new (TextBox TextBox, ComboBox ComboBox, string FieldName)[]
			{
				(txtWheelNumber, cmbWheelType, "Wheel"),
				(txtDoorNumber, cmbDoorType, "Door"),
				(txtGlassNumber, cmbGlassType, "Glass"),
				(txtMotorNumber, cmbMotorPower, "Motor")
			};

			#region Validate
			bool isValid = true;
			string errorMessage = "";
			int wheelNumber = 0, motorNumber = 0;

			foreach (var field in numberFields)
			{
				if (string.IsNullOrWhiteSpace(field.TextBox.Text))
				{
					isValid = false;
					errorMessage = $"Please enter a number (or 0) for {field.FieldName} Numbers.";
					break;
				}

				if (!int.TryParse(field.TextBox.Text, out int number) || number < 0 || number > 1000000)
				{
					isValid = false;
					errorMessage = $"{field.FieldName} Numbers must be (Integer) at least 1 and at most 1000000";
					break;
				}

				if (field.FieldName == "Wheel")
					wheelNumber = number;
				if (field.FieldName == "Motor")
					motorNumber = number;

				if (number > 0 && field.ComboBox.SelectedIndex == -1)
				{
					isValid = false;
					errorMessage = $"Please select a type for {field.FieldName} since the number is greater than 0.";
					break;
				}
			}

			if (isValid)
			{
				if (wheelNumber == 0)
				{
					isValid = false;
					errorMessage = "Wheel Numbers must be greater than 0. At least one wheel is required.";
				}
				else if (motorNumber == 0)
				{
					isValid = false;
					errorMessage = "Motor Numbers must be greater than 0. At least one motor is required.";
				}
			}

			if (!isValid)
			{
				MessageBox.Show(errorMessage, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			#endregion

			// إنشاء السيارة باستخدام الخدمة (يمكنك هنا تعديل طريقة الإنشاء وفقاً للتصميم لديك)
			var car = _carService.CreateCar(
				cmbWheelType?.SelectedItem?.ToString(),
				int.Parse(txtWheelNumber.Text),
				cmbDoorType?.SelectedItem?.ToString(),
				int.Parse(txtDoorNumber.Text),
				cmbGlassType?.SelectedItem?.ToString(),
				int.Parse(txtGlassNumber.Text),
				cmbMotorPower.SelectedItem.ToString(),
				int.Parse(txtMotorNumber.Text)
			);

			// تعطيل النموذج بالكامل أثناء عملية التجميع
			this.Enabled = false;
			progressBar.Visible = true;
			btnAssemble.Text = "Assembling...";

			try
			{
				// تنفيذ عملية التجميع بشكل غير متزامن
				var res = await Task.Run(() => _assembleService.AssembleCar(car));

				if (res)
					MessageBox.Show("Vehicle assembled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("Vehicle assembly failed. No available components.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred during assembly:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				// إعادة تفعيل النموذج وإخفاء ProgressBar
				this.Enabled = true;
				progressBar.Visible = false;
				btnAssemble.Text = "Assemble";
			}
		}

		// Handles the Sold Vehicles button click event
		private void BtnSold_Click(object sender, EventArgs e)
		{
			DataGridView dgv = this.Controls["dataGridSoldVehicles"] as DataGridView;

			DataTable table = new DataTable();
			table.Columns.Add("Car Number");
			table.Columns.Add("Motor Number");
			table.Columns.Add("Motor Power");
			table.Columns.Add("Glasses Number");
			table.Columns.Add("Glasses Type");
			table.Columns.Add("Doors Number");
			table.Columns.Add("Doors Type");
			table.Columns.Add("Wheels Number");
			table.Columns.Add("Wheels Type");

			var list = _carService.GetAllCars();
			dgv.DataSource = list;
		}
	}
}
