using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5WinForm
{
    public partial class Form1 : Form
    {

        private List<PersonData> DataList = new List<PersonData>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            ListRenew();
        }

        private void ListRenew()
        {
            lbData.DataSource = null;
            lbData.DataSource = DataList;
            lbData.DisplayMember = "FullData";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string surname = tbSurname.Text;
            DateTime birthdate = dtpBirth.Value;
            string gender = cbGender.Text;

            if (!string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(surname) &&
                !string.IsNullOrWhiteSpace(gender))
            {
                // Tworzenie nowego obiektu danych osobowych i dodanie go do listy
                PersonData person = new PersonData(name, surname, birthdate, gender);
                DataList.Add(person);

                ListRenew(); // Odświeżenie listy po dodaniu danych

                // Wyczyszczenie pól tekstowych
                tbName.Clear();
                tbSurname.Clear();
                cbGender.SelectedIndex = -1;
                dtpBirth.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (lbData.SelectedIndex != -1) // Sprawdzenie, czy jakiś rekord jest wybrany
            {
                DataList.RemoveAt(lbData.SelectedIndex); // Usunięcie wybranego rekordu z listy
                ListRenew(); // Odświeżenie listy po usunięciu danych

                // Wyczyszczenie pól tekstowych
                tbName.Clear();
                tbSurname.Clear();
                cbGender.SelectedIndex = -1;
                dtpBirth.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Wybierz rekord do usunięcia!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbData.SelectedIndex != -1) 
            {

                PersonData ChosenPerson = (PersonData)lbData.SelectedItem;
                tbName.Text = ChosenPerson.Name;
                tbSurname.Text = ChosenPerson.Surname;
                cbGender.Text = ChosenPerson.Gender;
                dtpBirth.Value = ChosenPerson.BirthDate;

            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lbData.SelectedIndex != -1) 
            {
                string name = tbName.Text;
                string surname = tbSurname.Text;
                DateTime birthdate = dtpBirth.Value;
                string gender = cbGender.Text;

                if (!string.IsNullOrWhiteSpace(name) &&
                    !string.IsNullOrWhiteSpace(surname) &&
                    !string.IsNullOrWhiteSpace(gender))
                {
                    
                    PersonData selectedPerson = DataList[lbData.SelectedIndex];

                    
                    selectedPerson.Name = name;
                    selectedPerson.Surname = surname;
                    selectedPerson.BirthDate = birthdate;
                    selectedPerson.Gender = gender;

                    ListRenew();

                    
                    tbName.Clear();
                    tbSurname.Clear();
                    cbGender.SelectedIndex = -1;
                    dtpBirth.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("Wypełnij wszystkie pola!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz rekord do aktualizacji!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class PersonData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

       
        public string FullData => Name+ " " + Surname + ", " + BirthDate.Date.ToString("d") + ", " + Gender;

        public PersonData(string name, string surname, DateTime birthdate, string gender)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthdate;
            Gender = gender;
        }
    }


}
