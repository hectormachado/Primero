using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Primero
{
    public partial class Form1 : Form
    {
        string nombre;
        string dni;
        string email;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string json = File.ReadAllText("people.json");
            List<user> _users = JsonConvert.DeserializeObject<List<user>>(json);
            foreach (user _user in _users)
            {
                ListViewItem _list;

                bool dni_empty = _user.comprova_dni();
                if (!dni_empty)
                {
                    _list = new ListViewItem("*DNI INCORRECTO*");

                }
                else
                {
                    _list = new ListViewItem(_user.dni);
                }

                bool name_empty = _user.comprova_nom();
                if (!name_empty)
                {
                    _list.SubItems.Add("*NOMBRE VACIO*");
                }
                else
                {
                    _list.SubItems.Add(_user.name);
                }

                bool email_empty = _user.comprova_mail();
                if (!email_empty)
                {
                    _list.SubItems.Add("*EMAIL VACIO*");
                }
                else
                {
                    _list.SubItems.Add(_user.email);
                }

                listViewUser.Items.Add(_list);
            }

            sw.Stop();
            timer1.Text = sw.Elapsed.ToString("hh\\:mm\\:ss\\.fff");

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void toList()
        {
            ListViewItem _list = new ListViewItem(dni);
            _list.SubItems.Add(nombre);
            _list.SubItems.Add(email);

            listViewUserParalel.Items.Add(_list);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Mostrar(String resultat)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string json = File.ReadAllText("people.json");
            List<user> _users = JsonConvert.DeserializeObject<List<user>>(json);
            foreach (user _user in _users)
            {
                Parallel.Invoke(() =>
                {
                    if (!_user.comprova_dni())
                    {
                        dni = "*DNI INCORRECTO*";

                    }
                    else
                    {
                        dni = _user.dni;
                    }
                },
                () =>
                {
                    if (!_user.comprova_nom())
                    {
                        nombre = "*NOMBRE VACIO*";
                    }
                    else
                    {
                        nombre = _user.name;
                    }
                },
                () =>
                {
                    if (!_user.comprova_mail())
                    {
                        email = "*EMAIL VACIO*";
                    }
                    else
                    {
                        email = _user.email;
                    }
                }
                );
                toList();
            }

            sw.Stop();
            timer2.Text = sw.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
        }
    }
}